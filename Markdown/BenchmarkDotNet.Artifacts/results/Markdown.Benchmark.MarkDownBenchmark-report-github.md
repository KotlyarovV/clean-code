``` ini

BenchmarkDotNet=v0.10.10, OS=Windows 10 Redstone 1 [1607, Anniversary Update] (10.0.14393.1884)
Processor=Intel Core i3-3217U CPU 1.80GHz (Ivy Bridge), ProcessorCount=4
Frequency=1753830 Hz, Resolution=570.1807 ns, Timer=TSC
  [Host]     : .NET Framework 4.6.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2117.0
  DefaultJob : .NET Framework 4.6.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.2117.0


```
|                   Method |     Mean |     Error |    StdDev |
|------------------------- |---------:|----------:|----------:|
|    TestMarkDownStringBig | 2.558 ms | 0.0143 ms | 0.0119 ms |
| TestMarkDownStringBigger | 8.374 ms | 0.1366 ms | 0.1141 ms |
