# Contains Duplicate · C#

[LeetCode #217](https://leetcode.com/problems/contains-duplicate/) · Easy

<details>
<summary>Topics (spoiler)</summary>

Array · Hash Table · Sorting
</details>

## Attempts

| # | Approach | Time / space | Outcome | LeetCode | Tag |
|:-:|---|---|---|---|---|
| 1 | `HashSet<int>`, one pass; `Add` returns `false` on a repeat, which ends the loop early | O(n) / O(n) | accepted | ✅ 19 ms · 76.25 MB | `contains-duplicate/cs-1-hashset-add` · **current** |

LeetCode's runtime and memory figures vary between submissions, so treat them as rough.

## Run

```bash
dotnet run      # 10 correctness cases (nums, expected, returned value, PASS/FAIL), then a 200,000-item efficiency test with a 3-second limit
```

Set `runEfficiency = false` at the top of `Program.cs` to skip the efficiency test while debugging. You only edit `Solution.cs`. The method signature matches LeetCode's C# template, so you can paste the class straight in to submit.
