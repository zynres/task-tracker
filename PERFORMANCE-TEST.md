## Performance Test

### Test Environment

| Operating System | Containerization | Processor | Memory |
| :--------------- | :--------------- | :-------- | :----- |
| Windows 11 Pro | Docker | Intel Core i5 (11th Generation) | 16 GB RAM |

### Test Configuration

| Warm-up | Load | Test duration | Total requests |
| :------ | :--- | :------------ | :------------- |
| 5 seconds | 10 requests/second (RPS)| 30 seconds | 300 |
---

> Load testing was performed using **NBomber** from [Before](test/LoadTest/reports/Before-Optimization/nbomber_report_2026-07-19--14-24-38.md) and [After](test/LoadTest/reports/After-Optimization/nbomber_report_2026-07-19--14-34-09.md) Optimization

### Results

| Metric             | Before Optimization | After Optimization |
| ------------------ | ------------------: | -----------------: |
| Minimum latency    |             3.79 ms |            3.39 ms |
| Mean latency       |             7.37 ms |            6.41 ms |
| Maximum latency    |            46.04 ms |           36.09 ms |
| Standard deviation |             3.55 ms |            2.88 ms |
| p50                |             6.90 ms |            6.10 ms |
| p75                |             8.90 ms |            7.81 ms |
| p95                |            10.53 ms |            9.71 ms |
| p99                |            23.82 ms |           12.70 ms |

## Database Optimization

A composite index on **(AssigneeId, Status, Deadline)** was introduced to optimize the most frequently executed filtering query.

Before optimization, PostgreSQL used the index on `AssigneeId`, but still had to read **1,074 index entries**, access **1,047 heap pages**, and discard **811 rows** during additional filtering because the remaining conditions were evaluated after the index lookup.

The composite index allowed PostgreSQL to eliminate a significant portion of unnecessary reads directly at the index level.

As a result:

| Metric                                 |   Before |   After |
| -------------------------------------- | -------: | ------: |
| Index entries read                     |    1,074 |     263 |
| Heap pages accessed                    |    1,047 |     261 |
| SQL execution time (`EXPLAIN ANALYZE`) | 122.5 ms | 0.64 ms |
| Average HTTP latency (NBomber)         |  7.37 ms | 6.41 ms |

Additionally, `AsNoTracking()` is used for read-only queries to avoid Entity Framework Core change tracking overhead for entities that are not modified.


