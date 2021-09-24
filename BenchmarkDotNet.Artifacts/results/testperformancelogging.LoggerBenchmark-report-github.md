``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1237 (21H1/May2021Update)
AMD Ryzen 5 2500U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.401
  [Host]     : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT
  DefaultJob : .NET 5.0.10 (5.0.1021.41214), X64 RyuJIT


```
|                        Method |      Mean |    Error |   StdDev |       Min |       Max | Iterations | Ratio | RatioSD | Rank |  Gen 0 | Allocated |
|------------------------------ |----------:|---------:|---------:|----------:|----------:|-----------:|------:|--------:|-----:|-------:|----------:|
| NormalLoggerCallWithParameter | 111.29 ns | 2.166 ns | 2.026 ns | 107.55 ns | 114.95 ns |      15.00 |  1.00 |    0.00 |    2 | 0.0343 |      72 B |
|              NormalLoggerCall | 112.05 ns | 2.281 ns | 2.133 ns | 109.71 ns | 115.92 ns |      15.00 |  1.01 |    0.02 |    2 | 0.0343 |      72 B |
|      FastLoggerWithParameters |  23.21 ns | 0.545 ns | 0.628 ns |  22.29 ns |  24.32 ns |      20.00 |  0.21 |    0.01 |    1 |      - |         - |
|                    FastLogger |  23.23 ns | 0.497 ns | 0.465 ns |  22.45 ns |  23.94 ns |      15.00 |  0.21 |    0.00 |    1 |      - |         - |
