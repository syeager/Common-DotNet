using System.Diagnostics;
using LittleByte.Common.Logging;
using Microsoft.Extensions.Hosting;
using LogLevel = LittleByte.Common.Logging.LogLevel;

namespace LittleByte.WorkerQueue;

public sealed class WorkerQueueService : BackgroundService
{
    private readonly IWorkItemQueue queue;
    private readonly ILog log;
    private readonly Dictionary<Guid, int> workItemAttempts = new();

    public WorkerQueueService(IWorkItemQueue queue)
    {
        this.queue = queue;
        log = this.NewLogger();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        log.Info("Starting worker queue service");
        while (!stoppingToken.IsCancellationRequested)
        {
            // TODO: Multi-thread
            //const int parallelWorkCount = 3; // TODO move to appsettings.
            //var itemsToGrab = parallelWorkCount > queue.Count ? queue.Count : parallelWorkCount;

            var workItem = await GetNextItem(stoppingToken);
            log.Push(workItem);

            var result = await DoWork(workItem, stoppingToken);
            log.Push(result);

            var attemptCount = workItemAttempts.GetValueOrDefault(workItem.Id);
            await RetryIfNeeded(stoppingToken, result, attemptCount, workItem);

            var logLevel = result.Status is WorkResult.Statuses.Failed or WorkResult.Statuses.TimedOut
                ? LogLevel.Warn
                : LogLevel.Debug;
            log.Write(logLevel, "Work item completed");
        }

        log.Info("Stopping worker queue service");
    }

    private async Task RetryIfNeeded(CancellationToken stoppingToken, WorkResult result, int attemptCount, WorkItem workItem)
    {
        if (result.Status != WorkResult.Statuses.Successful)
        {
            const int max = 3; // TODO move to appsettings.
            if (attemptCount >= max)
            {
                workItem.Cancel();
                workItemAttempts.Remove(workItem.Id);
            }
            else
            {
                var newWorkItem = workItem.Clone();
                await QueueItemForRetryAsync(newWorkItem, attemptCount, stoppingToken);
            }
        }
    }

    private static async Task<WorkResult> DoWork(WorkItem workItem, CancellationToken stoppingToken)
    {
        var stopwatch = Stopwatch.StartNew();
        WorkResult result;
        Task? delayTask = null;

        try
        {
            var workTokenSource = new CancellationTokenSource();
            var workTask = workItem.DoWorkAsync(workTokenSource.Token);
            const int workTtlMs = 10000; // TODO move to appsettings.
            delayTask = Task.Delay(workTtlMs, stoppingToken);
            var completedTask = await Task.WhenAny(workTask, delayTask);
            stopwatch.Stop();

            if (completedTask == workTask)
            {
                result = workTask.Result;
            }
            else
            {
                await workTokenSource.CancelAsync();
                result = stoppingToken.IsCancellationRequested
                    ? WorkResult.Cancelled(stopwatch.ElapsedMilliseconds, "Cancelled by service.")
                    : WorkResult.TimedOut(stopwatch.ElapsedMilliseconds);
            }
        }
        catch (Exception exception)
        {
            result = WorkResult.Failed(stopwatch.ElapsedMilliseconds, exception);
        }
        finally
        {
            stopwatch.Stop();
            delayTask?.Dispose();
        }

        return result;
    }

    private async Task<WorkItem> GetNextItem(CancellationToken stoppingToken)
    {
        var workItem = await queue.PopAsync(stoppingToken).AsTask();

        if (stoppingToken.IsCancellationRequested)
        {
            var attempts = workItemAttempts.GetValueOrDefault(workItem.Id);
            await QueueItemForRetryAsync(workItem, attempts, stoppingToken);
            stoppingToken.ThrowIfCancellationRequested();
        }

        return workItem;
    }

    private async Task QueueItemForRetryAsync(WorkItem workItem, int attemptCount, CancellationToken stoppingToken)
    {
        log.Debug("Pushing work item back to queue after cancellation");
        workItemAttempts[workItem.Id] = ++attemptCount;
        await queue.PushAsync(workItem, stoppingToken);
    }
}