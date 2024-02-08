using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OpenTelemtryIntegration
{

    public class OpenTelemetryBuilder
    {
        const string serviceName = "web-goat";
        public static void AddOpenTelemetry(IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName)).WithTracing(builder => builder.AddAspNetCoreInstrumentation());
        }
    }
}