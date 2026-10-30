# Two Sum · C#

[LeetCode #1](https://leetcode.com/problems/two-sum/) · Easy · Python version: [`../python/`](../python/)

`Solution.cs` holds attempt 2b, the best accepted version. Earlier attempts are under their tags and in the code blocks below.

## Attempts

| # | Approach | Time / space | Outcome | LeetCode | Tag |
|:-:|---|---|---|---|---|
| 1 | Nested loops | O(n²) / O(1) | correct, but times out on 200,000 items | not submitted | `two-sum/cs-1-brute-force` |
| 2 | `Dictionary<int, int>` from value to index, filled with `Add` | O(n) / O(n) | fails on a repeated value: `Add` throws on a duplicate key | — | `two-sum/cs-2-dict-add` |
| 3 | `Dictionary<int, List<int>>` from value to every index | O(n) / O(n) | accepted | ✅ 9 ms · 53.4 MB | `two-sum/cs-3-dict-list` |
| 2b | `Dictionary<int, int>` filled with `dict[key] = i`, so the last index wins | O(n) / O(n) | accepted | ✅ 6 ms · 49.65 MB | `two-sum/cs-2b-dict-overwrite` · **current** |

LeetCode's runtime and memory figures vary between submissions, so treat them as rough.

## C# notes

| Need | C# |
|---|---|
| Look up without throwing | `dict.TryGetValue(key, out var value)` returns `bool` |
| Write without throwing on duplicates | `dict[key] = value` adds or overwrites; `dict.Add` throws if the key exists |
| Size of a `List<T>` | `.Count` (arrays use `.Length`) |

<details>
<summary>Attempt 1 code (brute force, O(n²))</summary>

```csharp
for (int i = 0; i < nums.Length; i++)
{
    for (int j = nums.Length - 1; j > 0; j--)
    {
        if (j <= i)
        {
            break;
        } else if (nums[i] + nums[j] == target)
        {
            return new int[] { i, j };
        }
    }
}

return new int[] { -1, -1 };
```
</details>

<details>
<summary>Attempt 2 code (Dictionary&lt;int, int&gt; with Add, throws on duplicates)</summary>

```csharp
Dictionary <int,int> dict = new Dictionary<int, int>();
int difference;
for (int i = 0; i < nums.Length; i++)
{
    dict.Add(nums[i], i);                       // throws on [3, 3]
}

for (int j = 0; j < nums.Length; j++)
{
    int x = nums[j];
    while (true)
    {
        difference = target - x;
        if (!dict.TryGetValue(difference, out int result))
        {
            break;
        } else {
            if (result == j)
            {
                break;
            } else
            {
                return new int [] {j, result};
            }
        }
    }
}

return new int [] {-1, -1};
```
</details>

<details>
<summary>Attempt 3 code (Dictionary&lt;int, List&lt;int&gt;&gt;, O(n))</summary>

```csharp
Dictionary <int,List<int>> dict = new Dictionary<int, List<int>>();
int difference;
for (int i = 0; i < nums.Length; i++)
{
    if (!dict.TryGetValue(nums[i], out var list))
    {
        list = new List<int>();
        dict[nums[i]] = list;
    }

    list.Add(i);
}

for (int j = 0; j < nums.Length; j++)
{
    int x = nums[j];
    while (true)
    {
        difference = target - x;

        if (!dict.TryGetValue(difference, out var result))
        {
            break;
        } else {
            if (result.Count > 1)
            {
                if (result[0] == j)
                {
                    break;
                } else
                {
                    return new int [] {result[0], result[1]};
                }
            } else if (result[0] == j)
            {
                break;
            } else
            {
                return new int [] {j, result[0]};
            }
        }
    }
}
return new int [] {-1, -1};
```
</details>

## Run

```bash
dotnet run      # 7 correctness cases (nums, target, returned value, sum, PASS/FAIL), then a 200,000-item efficiency test with a 3-second limit
```

Set `runEfficiency = false` at the top of `Program.cs` to skip the efficiency test while debugging. You only edit `Solution.cs`. The method signature matches LeetCode's C# template, so you can paste the class straight in to submit.
