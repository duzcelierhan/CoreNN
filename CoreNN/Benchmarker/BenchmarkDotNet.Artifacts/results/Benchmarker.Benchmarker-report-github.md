``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1256 (1909/November2018Update/19H2)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]        : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  .NET Core 5.0 : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT

Job=.NET Core 5.0  Runtime=.NET Core 5.0  

```
|                    Method |       Mean |    Error |   StdDev | Ratio | RatioSD | Rank | Code Size | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |-----------:|---------:|---------:|------:|--------:|-----:|----------:|------:|------:|------:|----------:|
|               MultiplyAvx |   400.7 μs |  7.90 μs | 18.63 μs |  0.25 |    0.01 |    2 |     537 B |     - |     - |     - |         - |
|       MultiplyAvxWSpanPtr |   369.8 μs |  7.38 μs | 18.09 μs |  0.23 |    0.01 |    1 |     593 B |     - |     - |     - |         - |
|               MultiplyFma |   397.5 μs |  7.88 μs | 19.61 μs |  0.25 |    0.01 |    2 |     519 B |     - |     - |     - |         - |
|       MultiplyFmaWSpanPtr |   369.1 μs |  7.26 μs | 12.72 μs |  0.23 |    0.01 |    1 |     594 B |     - |     - |     - |         - |
|        MultiplyWithVector |   430.2 μs |  7.94 μs | 15.68 μs |  0.27 |    0.01 |    3 |     546 B |     - |     - |     - |         - |
|     MultiplyWithVectorDot |   502.1 μs | 11.04 μs | 31.49 μs |  0.32 |    0.02 |    4 |     513 B |     - |     - |     - |         - |
|     MultiplyWithVectorMul |   438.2 μs |  8.74 μs | 17.65 μs |  0.28 |    0.01 |    3 |     546 B |     - |     - |     - |         - |
|     MultiplyClassicSingle | 1,579.0 μs | 12.51 μs | 11.70 μs |  1.00 |    0.00 |    7 |     372 B |     - |     - |     - |         - |
| MultiplyClassicSingleWPtr | 1,629.9 μs | 23.91 μs | 22.36 μs |  1.03 |    0.02 |    8 |     418 B |     - |     - |     - |         - |
|     MultiplyClassicGroup4 |   903.8 μs | 17.45 μs | 21.43 μs |  0.58 |    0.01 |    6 |     570 B |     - |     - |     - |         - |
|     MultiplyClassicGroup8 |   880.0 μs | 16.20 μs | 22.17 μs |  0.56 |    0.02 |    5 |     730 B |     - |     - |     - |         - |
