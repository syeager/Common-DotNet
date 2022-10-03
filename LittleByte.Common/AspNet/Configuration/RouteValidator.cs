using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LittleByte.Common.AspNet.Configuration
{
    // TODO: Move to analyzer.
    public static class RouteValidator
    {
        private const string DefaultRouteRegex = @"^*$";

        public static IApplicationBuilder ValidateRoutes(
            this IApplicationBuilder app,
            string routeRegex = DefaultRouteRegex)
        {
            var actionProvider = app.ApplicationServices.GetService<IActionDescriptorCollectionProvider>()!;
            var regex = new Regex(routeRegex);

            var invalidRouteNames = GetInvalidRouteNames(actionProvider, regex);

            if(invalidRouteNames.Length > 0)
            {
                Fail(invalidRouteNames);
            }

            return app;
        }

        private static ImmutableArray<ActionDescriptor> GetInvalidRouteNames(
            IActionDescriptorCollectionProvider actionProvider,
            Regex regex)
        {
            var invalidRouteNames = actionProvider.ActionDescriptors.Items
                .Where(x => x.AttributeRouteInfo?.Template != null && !regex.IsMatch(x.AttributeRouteInfo.Template))
                .ToImmutableArray();
            return invalidRouteNames;
        }

        private static void Fail(ImmutableArray<ActionDescriptor> invalidRoutes)
        {
            const string separator = "\n- ";
            var invalideRouteNames =
                string.Join(separator, invalidRoutes.Select(rn => rn.AttributeRouteInfo!.Template!));
            var message = $"Invalid route names: {invalidRoutes.Length}{separator}{invalideRouteNames}";
            throw new Exception(message);
        }
    }
}
