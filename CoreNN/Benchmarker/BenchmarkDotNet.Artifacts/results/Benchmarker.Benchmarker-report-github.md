``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Xeon CPU E3-1505M v6 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host] : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
  Core   : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |       Mean |     Error |   StdDev | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |-----------:|----------:|---------:|------:|--------:|-----:|------:|------:|------:|----------:|
|               MultiplyAvx |   353.4 us |  7.333 us | 20.07 us |  0.28 |    0.02 |    3 |     - |     - |     - |         - |
|       MultiplyAvxWSpanPtr |   316.1 us |  6.290 us | 14.83 us |  0.24 |    0.01 |    2 |     - |     - |     - |         - |
|               MultiplyFma |   393.5 us | 12.892 us | 37.81 us |  0.26 |    0.01 |    4 |     - |     - |     - |         - |
|       MultiplyFmaWSpanPtr |   300.1 us |  5.899 us | 11.37 us |  0.23 |    0.01 |    1 |     - |     - |     - |         - |
|     MultiplyClassicSingle | 1,323.3 us | 24.182 us | 22.62 us |  1.00 |    0.00 |    7 |     - |     - |     - |         - |
| MultiplyClassicSingleWPtr | 1,364.6 us | 27.131 us | 29.03 us |  1.04 |    0.03 |    8 |     - |     - |     - |         - |
|     MultiplyClassicGroup4 |   798.8 us | 15.884 us | 14.86 us |  0.60 |    0.01 |    6 |     - |     - |     - |         - |
|     MultiplyClassicGroup8 |   779.3 us | 16.512 us | 20.88 us |  0.59 |    0.02 |    5 |     - |     - |     - |         - |
