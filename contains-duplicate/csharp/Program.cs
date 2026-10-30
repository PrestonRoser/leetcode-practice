using System.Diagnostics;

// Test harness: runs every case, prints what came back, then a summary.
// Run with: dotnet run

// Set to true to run the 200,000-item efficiency test after the correctness cases.
bool runEfficiency = true;

var cases = new (string Name, int[] Nums, bool Expected)[]
{
    // the three LeetCode examples
    ("example 1",                   [1, 2, 3, 1],                       true),
    ("example 2",                   [1, 2, 3, 4],                       false),
    ("example 3",                   [1, 1, 1, 3, 3, 4, 3, 2, 4, 2],     true),
    // edge cases
    ("single element",              [7],                                false),
    ("only two, the same",          [0, 0],                             true),
    ("repeat at both ends",         [5, 1, 2, 3, 5],                    true),
    ("negatives, all distinct",     [-1, -2, -3, 0],                    false),
    ("negative repeat",             [-4, 2, -4],                        true),
    ("int extremes, all distinct",  [int.MinValue, -1, 0, 1, int.MaxValue], false),
    ("int extremes, repeated",      [int.MaxValue, int.MinValue, int.MaxValue], true),
};

int passed = 0;

foreach (var (name, nums, expected) in cases)
{
    Console.WriteLine($"\n{name}");
    Console.WriteLine($"  nums=[{string.Join(", ", nums)}]  expected={expected}");

    string? problem;
    try
    {
        var result = new Solution().ContainsDuplicate(nums);
        Console.WriteLine($"  returned: {result}");
        problem = result == expected ? null : $"expected {expected}";
    }
    catch (Exception e)
    {
        problem = $"threw {e.GetType().Name}: {e.Message}";
    }

    if (problem is null)
    {
        passed++;
        Console.WriteLine("  PASS");
    }
    else
    {
        Console.WriteLine($"  FAIL: {problem}");
    }
}

Console.WriteLine($"\n{passed}/{cases.Length} correctness cases passed");

if (!runEfficiency)
{
    Console.WriteLine("\nefficiency test skipped (runEfficiency = false at the top of Program.cs)");
    return passed == cases.Length ? 0 : 1;
}

// Efficiency: 200,000 distinct numbers in scrambled order, so the answer is false
// and nothing can stop early. A nested-loop solution compares ~20 billion pairs here.
// 7919 shares no factor with n, so i * 7919 % n visits every value in 0..n-1 exactly once.
const int n = 200_000;
var big = new int[n];
for (int i = 0; i < n; i++) big[i] = (int)((long)i * 7919 % n) - n / 2;

Console.WriteLine($"\nefficiency: n={n:N0}, all distinct, expected=False");
var sw = Stopwatch.StartNew();
var task = Task.Run(() => new Solution().ContainsDuplicate(big));
bool finished;
try
{
    finished = task.Wait(TimeSpan.FromSeconds(3));
}
catch (AggregateException e)
{
    Console.WriteLine($"  FAIL: threw {e.InnerException?.GetType().Name}: {e.InnerException?.Message}");
    return 1;
}
sw.Stop();

if (!finished)
{
    Console.WriteLine("  FAIL: still running after 3 s (LeetCode would call this Time Limit Exceeded)");
    return 1;
}

bool bigPassed = task.Result == false;
Console.WriteLine($"  returned: {task.Result} in {sw.ElapsedMilliseconds} ms");
Console.WriteLine(bigPassed ? "  PASS" : "  FAIL: expected False");

return passed == cases.Length && bigPassed ? 0 : 1;
