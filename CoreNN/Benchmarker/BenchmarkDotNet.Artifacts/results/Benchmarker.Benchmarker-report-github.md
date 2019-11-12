``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18362
Intel Xeon CPU E3-1505M v6 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host]     : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT
  Job-EVJDWA : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), X64 RyuJIT

Runtime=.NET Core 3.0  

```
|                    Method |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
|               MultiplyAvx |   389.7 us |  7.69 us | 14.45 us |   388.7 us |  0.29 |    0.01 |    2 |     - |     - |     - |         - |
|       MultiplyAvxWSpanPtr |   380.4 us | 15.61 us | 46.03 us |   370.3 us |  0.27 |    0.02 |    2 |     - |     - |     - |         - |
|               MultiplyFma |   430.9 us |  8.51 us | 20.55 us |   432.9 us |  0.32 |    0.02 |    3 |     - |     - |     - |         - |
|       MultiplyFmaWSpanPtr |   334.2 us |  6.65 us | 15.00 us |   333.0 us |  0.25 |    0.01 |    1 |     - |     - |     - |         - |
|        MultiplyWithVector |   385.9 us |  7.59 us |  9.60 us |   384.6 us |  0.28 |    0.01 |    2 |     - |     - |     - |         - |
|     MultiplyWithVectorDot |   433.5 us |  8.56 us |  9.52 us |   431.6 us |  0.32 |    0.01 |    3 |     - |     - |     - |         - |
|     MultiplyWithVectorMul |   385.2 us |  7.52 us | 13.56 us |   383.2 us |  0.29 |    0.01 |    2 |     - |     - |     - |         - |
|     MultiplyClassicSingle | 1,367.0 us | 21.14 us | 19.77 us | 1,363.9 us |  1.00 |    0.00 |    6 |     - |     - |     - |       1 B |
| MultiplyClassicSingleWPtr | 1,375.2 us | 21.59 us | 20.20 us | 1,371.3 us |  1.01 |    0.03 |    6 |     - |     - |     - |       1 B |
|     MultiplyClassicGroup4 |   837.1 us | 16.55 us | 17.71 us |   832.8 us |  0.61 |    0.01 |    5 |     - |     - |     - |         - |
|     MultiplyClassicGroup8 |   820.6 us | 16.33 us | 40.96 us |   806.8 us |  0.64 |    0.03 |    4 |     - |     - |     - |         - |
