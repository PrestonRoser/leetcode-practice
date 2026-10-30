using System.Diagnostics;

// Test harness: runs every case, prints what came back, then a summary.
// Run with: dotnet run

// Set to true to run the 200,000-item efficiency test after the correctness cases.
bool runEfficiency = true;

var cases = new (string Name, int[] Nums, int Target)[]
{
    // the three LeetCode examples
    ("example 1",              [2, 7, 11, 15],        9),
    ("example 2",              [3, 2, 4],             6),
    ("duplicate values",       [3, 3],                6),
    // edge cases
    ("don't reuse an element", [3, 2, 4],             6),
    ("negatives",              [-3, 4, 3, 90],        0),
    ("answer in the middle",   [1, 5, 8, 12, 20, 2], 20),
    ("zeros",                  [0, 4, 3, 0],          0),
};

int passed = 0;

foreach (var (name, nums, target) in cases)
{
    Console.WriteLine($"\n{name}");
    Console.WriteLine($"  nums=[{string.Join(", ", nums)}]  target={target}");

    string? problem;
    try
    {
        var result = new Solution().TwoSum(nums, target);
        Console.WriteLine($"  returned: {Show(result)}");
        problem = Check(nums, target, result);
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

// Efficiency: 200,000 numbers, and the only pair that works is at the very end.
// A nested-loop solution checks ~20 billion pairs here; a one-pass solution is instant.
const int n = 200_000;
var big = new int[n];
for (int i = 0; i < n - 2; i++) big[i] = 10 + 2 * i;   // evens, every pair sums past 4
big[n - 2] = 1;
big[n - 1] = 3;

Console.WriteLine($"\nefficiency: n={n:N0}, answer is the last two indices");
var sw = Stopwatch.StartNew();
var task = Task.Run(() => new Solution().TwoSum(big, 4));
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

var bigProblem = Check(big, 4, task.Result);
Console.WriteLine($"  returned: {Show(task.Result)} in {sw.ElapsedMilliseconds} ms");
Console.WriteLine(bigProblem is null ? "  PASS" : $"  FAIL: {bigProblem}");

return passed == cases.Length && bigProblem is null ? 0 : 1;


// Returns null when the answer is valid, otherwise what's wrong with it.
static string? Check(int[] nums, int target, int[]? result)
{
    if (result is null) return "returned null";
    if (result.Length != 2) return $"expected two indices, got {result.Length}";
    var (i, j) = (result[0], result[1]);
    if (i < 0 || i >= nums.Length || j < 0 || j >= nums.Length) return $"index out of range: [{i}, {j}]";
    if (i == j) return $"used the same element twice: [{i}, {j}]";
    Console.WriteLine($"  nums[{i}] + nums[{j}] = {nums[i]} + {nums[j]} = {nums[i] + nums[j]}");
    if (nums[i] + nums[j] != target) return $"sum is {nums[i] + nums[j]}, not {target}";
    return null;
}

static string Show(int[]? a) => a is null ? "null" : $"[{string.Join(", ", a)}]";
