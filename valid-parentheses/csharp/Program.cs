using System.Diagnostics;

// Test harness: runs every case, prints what came back, then a summary.
// Run with: dotnet run

// Set to true to run the 200,000-character efficiency test after the correctness cases.
bool runEfficiency = true;

var cases = new (string Name, string S, bool Expected)[]
{
    // the LeetCode examples
    ("one pair",                    "()",           true),
    ("three pairs in a row",        "()[]{}",       true),
    ("wrong closing type",          "(]",           false),
    ("nested",                      "([])",         true),
    ("crossed",                     "([)]",         false),
    // edge cases
    ("lone opener",                 "(",            false),
    ("lone closer",                 ")",            false),
    ("closed before opened",        ")(",           false),
    ("extra opener",                "(()",          false),
    ("extra closer",                "())",          false),
    ("valid, then an opener",       "()[",          false),
    ("valid, then a closer",        "[]}",          false),
    ("all openers",                 "([{",          false),
    ("all closers",                 "}])",          false),
    ("deep nesting",                "{[()]}",       true),
    ("nested, then a sequence",     "{[]}()",       true),
    ("mismatch at the outer pair",  "{[()]]",       false),
    ("crossed, curly and square",   "[{]}",         false),
};

int passed = 0;

foreach (var (name, s, expected) in cases)
{
    Console.WriteLine($"\n{name}");
    Console.WriteLine($"  s=\"{s}\"  expected={expected}");

    string? problem;
    try
    {
        var result = new Solution().IsValid(s);
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

// Efficiency: 100,000 openers then their 100,000 closers, nested as deep as the
// length allows, so the answer is true and nothing can stop early. Anything that
// rescans the whole string for every bracket it resolves goes quadratic here.
const int n = 200_000;
var chars = new char[n];
for (int i = 0; i < n / 2; i++)
{
    chars[i] = "([{"[i % 3];
    chars[n - 1 - i] = ")]}"[i % 3];
}
var big = new string(chars);

Console.WriteLine($"\nefficiency: n={n:N0}, fully nested, expected=True");
var sw = Stopwatch.StartNew();
var task = Task.Run(() => new Solution().IsValid(big));
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

bool bigPassed = task.Result == true;
Console.WriteLine($"  returned: {task.Result} in {sw.ElapsedMilliseconds} ms");
Console.WriteLine(bigPassed ? "  PASS" : "  FAIL: expected True");

return passed == cases.Length && bigPassed ? 0 : 1;
