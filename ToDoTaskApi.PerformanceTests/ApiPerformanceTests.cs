using System.Text;
using System.Text.Json;
using NBench;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Tests;

namespace ToDoTaskApi.PerformanceTests
{
    public class ApiPerformanceTests
    {
        // HTTP client used to send requests to the running API instance
        private HttpClient _client = default!;
        // Counter provided by NBench to measure capacity
        private Counter _apiCounter = default!;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };
            _apiCounter = context.GetCounter("ApiCounter");
        }

        [PerfBenchmark(
            Description = "Stress test dla POST /todotasks",
            NumberOfIterations = 3,                                                                     // Run the benchmark 3 times
            TestMode = TestMode.Test,
            RunTimeMilliseconds = 10000)]                                                               // Run each iteration for 10 seconds
        [CounterThroughputAssertion("ApiCounter", MustBe.GreaterThan, 50.0d)]                           // Assert: must handle >50 requests/sec
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 2)]   // Assert: no more than 2 Gen2 GCs
        public void CreateTaskBenchmark(BenchmarkContext context)
        {
            // Prepare the request payload
            var request = new CreateToDoTaskRequest(
                "Stress test",
                "Check API performance",
                DateTime.UtcNow.AddDays(1));

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the API synchronously
            var response = _client.PostAsync("/todotasks", content).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                _apiCounter.Increment();
            }
        }
    }
}
