using System.Net;
using static NUnit.Framework.Assert;

namespace LittleByte.AspNet.Test;

public static class ApiAssert
{
    public static void IsSuccess(ApiResponse response, HttpStatusCode expectedCode = HttpStatusCode.OK)
    {
        Multiple(() =>
        {
            That(response.IsError, Is.False);
            That(response.StatusCode, Is.EqualTo((int)expectedCode));
        });
    }

    public static void IsSuccess<T>(ApiResponse<T> response, HttpStatusCode expectedCode = HttpStatusCode.OK)
        where T : class
    {
        Multiple(() =>
        {
            That(response.IsError, Is.False);
            That(response.Obj, Is.Not.Null);
            That(response.StatusCode, Is.EqualTo((int)expectedCode));
        });
    }

    public static void IsFailure(ApiResponse response, HttpStatusCode expectedCode = HttpStatusCode.BadRequest)
    {
        Multiple(() =>
        {
            That(response.IsError, Is.True);
            That(response.StatusCode, Is.EqualTo((int)expectedCode));
        });
    }
}