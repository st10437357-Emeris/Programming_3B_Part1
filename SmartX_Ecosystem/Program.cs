using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using SmartX.Core; // Ensures access to your TelemetryPacket<T>

namespace SmartX_Ecosystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Smart-X API Integration Layer
            app.MapPost("/api/telemetry/ingest", (TelemetryPacket<string> packet) =>
            {
                // Fulfills Technical Requirement: Advanced Arrays and Lists
                // Utilizes jagged arrays to manage raw telemetry before transferring to optimized Collections
                string[][] rawBatch = new string[1][];
                rawBatch[0] = new string[] { packet.MacAddress, packet.Location, packet.Payload };

                List<string> optimizedCollection = rawBatch[0].ToList();

                // Returns a success response to the Windows Forms dashboard
                return Results.Ok(new
                {
                    Message = "Payload ingested successfully.",
                    Data = optimizedCollection
                });
            })
            .WithName("IngestTelemetry");

            app.Run();
        }
    }
}