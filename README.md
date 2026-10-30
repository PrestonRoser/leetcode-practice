# LeetCode Practice

Timed LeetCode practice, mostly in C#, with a local test harness for every problem so a solution can be run and debugged in VS Code before it's submitted. Every attempt is kept, including the slow and broken ones, so each problem's README shows the approaches it went through on the way to the current solution.

> [!NOTE]
> **Spoilers:** each problem folder on `main` contains a working solution. To practice a problem yourself, see [Use a harness for your own practice](#use-a-harness-for-your-own-practice).

## Who wrote what

This is a practice log, so authorship matters. **The solutions are written by hand. Claude (Anthropic's AI assistant, used through Claude Code) built everything around them.** This table is the single statement of that split, and every problem follows it.

| Written by the repository author | Written by Claude |
|---|---|
| Every solution: the method bodies in `Solution.cs` and `<problem>.py`, for every attempt | Repository setup: the layout, `.gitignore`, the editor settings, `scripts/test-all.sh`, and the CI workflow |
| Debugging, the choice of approach, and the answers to review questions | Each problem's local copy for VS Code: the LeetCode-signature stub with a short restatement of the task, the test harness (`Program.cs` or `test_*.py`) and its cases, and the `.csproj` |
| | The READMEs, including the problem index and each problem's attempt table, written from the author's results |
| | Answers to questions during an attempt, only when asked, and never as solution code |

How that stays true:
- **Claude doesn't write or edit solution code.**
- **Autocomplete and AI suggestions are off** in every practice folder (`.vscode/settings.json`), including GitHub Copilot and VS Code's built-in AI features.
- **Commits made through Claude Code end with a `Co-Authored-By: Claude` trailer.** The trailer marks who ran the commit, and that includes commits that save the author's own solutions. This table, not the trailer, is the record of who wrote what.

## Problems

| Problem | Lang | Current approach | Time / space | LeetCode |
|---|---|---|---|---|
| [Two Sum](two-sum/csharp/) | C# | `Dictionary<int, int>` from value to index | O(n) / O(n) | ✅ 6 ms |
| [Two Sum](two-sum/python/) | Python | nested loops (brute force) | O(n²) / O(n) | — |
| [Contains Duplicate](contains-duplicate/csharp/) | C# | `HashSet<int>`, one pass with early exit | O(n) / O(n) | ✅ 19 ms |

## Getting started

**Requirements:** the [.NET 9 SDK](https://dotnet.microsoft.com/download) for C# and Python 3.9 or newer for Python.

```bash
cd two-sum/csharp && dotnet run          # one C# problem: every case, then an efficiency test
cd two-sum/python && python3 -m unittest # one Python problem
./scripts/test-all.sh                    # every harness in the repo; exits 1 if any fail
```

Each harness prints the input, the returned value, and PASS or FAIL for every case. The C# harnesses also run a large-input efficiency test with a 3-second limit, which a nested-loop solution fails the way it would on LeetCode. Set `runEfficiency = false` at the top of `Program.cs` to skip it while debugging.

### Layout

```
<problem-slug>/
  csharp/   Solution.cs (the only file you edit) · Program.cs (harness) · <Name>.csproj · README.md
  python/   <problem>.py (the only file you edit) · test_<problem>.py (harness) · README.md
scripts/test-all.sh       runs every harness
.github/workflows/        CI
```

Method signatures match LeetCode's templates, so a `Solution` class can be pasted straight into LeetCode to submit.

### Use a harness for your own practice

Copy a problem folder, replace the body of the solution method with `throw new NotImplementedException();` (C#) or `pass` (Python), and run it. All cases fail until you solve it. Each problem's first commit also has the original stub: `git log --diff-filter=A -- <problem>/csharp/Solution.cs`.

## How attempts are tracked

- **`main` holds the best accepted version** of each solution. A better attempt replaces the old one there.
- **Every attempt gets a tag** named `<problem>/<lang>-<attempt>-<approach>`, so any old version is one checkout away:

  | Tag | What it is |
  |---|---|
  | `two-sum/py-1-brute-force` | Python, nested loops, O(n²) |
  | `two-sum/cs-1-brute-force` | nested loops, O(n²), efficiency TLE |
  | `two-sum/cs-2-dict-add` | `Dictionary<int, int>` with `Add`, fails on duplicates (`Add` throws) |
  | `two-sum/cs-3-dict-list` | `Dictionary<int, List<int>>`, accepted, 9 ms / 53.4 MB |
  | `two-sum/cs-2b-dict-overwrite` | `Dictionary<int, int>` with `dict[key] = i`, accepted, 6 ms / 49.65 MB · **on `main`** |
  | `contains-duplicate/cs-1-hashset-add` | `HashSet<int>.Add`, one pass, accepted, 19 ms / 76.25 MB · **on `main`** |

  `git tag -l -n1` lists them, and `git show <tag>:two-sum/csharp/Solution.cs` prints one without switching branches.
- **`dev` is where work happens; `main` is the finished side.** Attempts, new problem stubs, and half-done work are committed to `dev`, where a failing test run is expected mid-attempt. `main` only moves by merging `dev` into it once every harness passes, so a stub or a broken attempt never lands there.
  ```bash
  git switch dev               # work and commit here
  ./scripts/test-all.sh        # every harness in the repo; exits 1 if any fail
  git switch main && git merge --ff-only dev && git switch dev   # only on green
  ```
  On GitHub, the same rule runs as a pull request from `dev` to `main`.
- **CI:** `.github/workflows/tests.yml` runs `scripts/test-all.sh` on every push to `dev` or `main` and on pull requests into `main`, on GitHub's Ubuntu runner with .NET 9 and Python 3.12. It picks up any `<problem>/csharp/*.csproj` and `<problem>/python/test_*.py`, so a new problem needs no CI changes.
- **Side branches are optional.** To try a risky alternate approach without disturbing `dev`, branch off `dev`, tag the attempt when it's done, merge it if it wins, and delete the branch.
- Each problem's README lists every attempt's approach, complexity, outcome, and tag, with earlier attempts' code collapsed underneath, so the history is readable without git.

## Queue

1. Valid Parentheses
2. Valid Anagram
3. Best Time to Buy and Sell Stock
4. Binary Search
5. Group Anagrams

## Credits

- **[LeetCode](https://leetcode.com)** for the problems. Each problem's README links to the original. Problem statements aren't reproduced here: stubs carry a short paraphrase, and the test cases include LeetCode's public examples alongside original edge cases. This repository isn't affiliated with or endorsed by LeetCode.
- **[Microsoft Learn .NET API reference](https://learn.microsoft.com/dotnet/api/)** and the **[Python documentation](https://docs.python.org/3/)**: the references used during attempts.
- **[Claude Code](https://www.anthropic.com/claude-code)** (Anthropic): see [Who wrote what](#who-wrote-what).

## License

[MIT](LICENSE), covering the code and documentation in this repository. It doesn't cover LeetCode's problems, which belong to LeetCode.
