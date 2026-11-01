# Valid Parentheses · C#

[LeetCode #20](https://leetcode.com/problems/valid-parentheses/) · Easy

<details>
<summary>Topics (spoiler)</summary>

String · Stack
</details>

## Attempts

| # | Approach | Time / space | Outcome | LeetCode | Tag |
|:-:|---|---|---|---|---|
| 1 | `Stack<char>`, one pass with a `switch`: openers are pushed, and each closer pops and must match | O(n) / O(n) | accepted | ✅ 2 ms · 40.97 MB | `valid-parentheses/cs-1-stack-switch` · **current** |

LeetCode's runtime and memory figures vary between submissions, so treat them as rough.

## Run

```bash
dotnet run      # 18 correctness cases (s, expected, returned value, PASS/FAIL), then a 200,000-character efficiency test with a 3-second limit
```

Set `runEfficiency = false` at the top of `Program.cs` to skip the efficiency test while debugging. You only edit `Solution.cs`. The method signature matches LeetCode's C# template, so you can paste the class straight in to submit.
