ClickHouse C# Client Driver
=
A fast, low-latency, memory-efficient C# driver for ClickHouse database with native protocol support and strong static typing.


## Benchmarks
Here are the results of the benchmark (`ClickHouse.Driver.Benchmarks`) running on Ubuntu 22.04.

PC specs: AMD Ryzen 5 2400G, 8GB DDR4 RAM, 512GB HDD 7200RPM

<img src="https://github.com/user-attachments/assets/1450d1c7-a091-4593-8173-fde4053702ed" alt="Image 1" style="width: 49%; ">
<img src="https://github.com/user-attachments/assets/487fd499-df28-4fef-963a-84d4a1baa041" alt="Image 1" style="width: 49%;">

<br/>
<br/>

For 1M rows inserted, `ClickHouse.Driver` is 5x faster than the second-fastest library (`ClickHouse.Client`)

| Library                    | Insertion Time Ratio  | Memory Usage  |
|:---------------------------|:----------------------|:--------------|
| ClickHouse.Driver (this)   | 1x                    | 1x            |
| ClickHouse.Client          | 5.09x                 | 3.33x         |
| Octonica.ClickHouseClient  | 6.21x                 | 0.71x         |
| ClickHouse.Ado             | 63.35                 | 4.56x         |
