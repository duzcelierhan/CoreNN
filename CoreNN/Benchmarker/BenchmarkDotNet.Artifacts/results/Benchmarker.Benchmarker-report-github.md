``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Xeon CPU E3-1505M v6 3.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100
  [Host] : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT
  Core   : .NET Core 3.0.0 (CoreCLR 4.700.19.46205, CoreFX 4.700.19.46214), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |       Mean |     Error |   StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------- |-----------:|----------:|---------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
|           MultiplyAvx |   414.3 us |  7.915 us | 10.01 us |   414.9 us |  0.31 |    0.01 |    3 |     - |     - |     - |         - |
|   MultiplyAvxWSpanPtr |   418.8 us | 12.830 us | 36.40 us |   407.3 us |  0.31 |    0.02 |    3 |     - |     - |     - |         - |
|           MultiplyFma |   392.5 us | 10.268 us | 28.62 us |   386.4 us |  0.32 |    0.03 |    2 |     - |     - |     - |         - |
|   MultiplyFmaWSpanPtr |   357.8 us |  7.082 us | 10.16 us |   356.4 us |  0.27 |    0.01 |    1 |     - |     - |     - |         - |
| MultiplyClassicSingle | 1,353.4 us | 26.009 us | 25.54 us | 1,362.7 us |  1.00 |    0.00 |    6 |     - |     - |     - |         - |
| MultiplyClassicGroup4 |   864.2 us | 12.670 us | 11.23 us |   865.1 us |  0.64 |    0.02 |    5 |     - |     - |     - |         - |
| MultiplyClassicGroup8 |   824.4 us | 16.366 us | 19.48 us |   817.2 us |  0.61 |    0.02 |    4 |     - |     - |     - |         - |
