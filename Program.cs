using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace testperformancelogging
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<LoggerBenchmark>();

        }
    }
    [BenchmarkDotNet.Attributes.MemoryDiagnoser]
    [BenchmarkDotNet.Attributes.MinColumn]
    [BenchmarkDotNet.Attributes.MaxColumn]
    [BenchmarkDotNet.Attributes.RankColumn]
    public class LoggerBenchmark
    {
        private static readonly Action<ILogger, string, string, int, Exception> _fastLogMessageWithParameters;
        private static readonly Action<ILogger, Exception> _fastLogMessage;
        private readonly ServiceProvider service;
        private readonly ILogger<LoggerBenchmark> logger;

        const string Val1 = "This is a sample Parameter string";
        const string Val2 = " the length of these values should not matter for boxing";
        const int Val3 = 96;
        static LoggerBenchmark()
        {
            _fastLogMessageWithParameters = LoggerMessage.Define<string, string, int>(LogLevel.Critical, new EventId(1, "BenchmarkEvent"), "My simple test string {val1} {val2} {val3}");
            _fastLogMessage = LoggerMessage.Define(LogLevel.Critical, new EventId(1, "BenchmarkEvent"), "My simple test string without parameters");
        }
        public LoggerBenchmark()
        {
            var services = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddDebug();
            });
            this.service = services.BuildServiceProvider();
            logger = this.service.GetRequiredService<ILogger<LoggerBenchmark>>();
        }

        [BenchmarkDotNet.Attributes.Benchmark(Baseline = true)]
        public object NormalLoggerCallWithParameter()
        {
            logger.LogCritical(null, "My simple test string {val1} {val2} {val3}", Val1, Val2, Val3);
            return logger;
        }
        [BenchmarkDotNet.Attributes.Benchmark]
        public object NormalLoggerCall()
        {
            logger.LogCritical(null, "My simple test string {val1} {val2} {val3}", Val1, Val2, Val3);
            return logger;
        }
        [BenchmarkDotNet.Attributes.Benchmark]
        public object FastLoggerWithParameters()
        {
            _fastLogMessageWithParameters(logger, Val1, Val2, Val3, null);
            return logger;
        }
        [BenchmarkDotNet.Attributes.Benchmark]
        public object FastLogger()
        {
            _fastLogMessage(logger, null);
            return logger;
        }
    }
}
