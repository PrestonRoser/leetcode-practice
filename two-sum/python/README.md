# Two Sum · Python

[LeetCode #1](https://leetcode.com/problems/two-sum/) · Easy · Continued in C#: [`../csharp/`](../csharp/)

## Attempts

| # | Approach | Time / space | Outcome | LeetCode | Tag |
|:-:|---|---|---|---|---|
| 1 | Nested loops over `enumerate`, inner loop from the end | O(n²) / O(n), from a list rebuilt on each outer pass | correct, but too slow for LeetCode | — | `two-sum/py-1-brute-force` |

<details>
<summary>Attempt 1 code (brute force, saved for comparison)</summary>

```python
for a, b in enumerate(nums):
    for c, d in reversed(list(enumerate(nums))):
        if c <= a:
            break
        elif (b + d) == target:
            return [a, c]
return
```
Debug prints removed here. `two_sum.py` on `main` (tag `two-sum/py-1-brute-force`) has the full version.
</details>

## Run

```bash
python3 test_two_sum.py      # prints each case: nums, target, returned value, and the sum
```
