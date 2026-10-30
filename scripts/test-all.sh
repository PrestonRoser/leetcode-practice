#!/usr/bin/env bash
# Runs every problem's test harness and exits non-zero if any of them fail.
# CI runs this on every push; run it locally before merging dev into main.
#   C#:     <problem>/csharp/*.csproj   -> dotnet run
#   Python: <problem>/python/test_*.py  -> python -m unittest
set -u
cd "$(dirname "$0")/.."

failed=()

for proj in */csharp/*.csproj; do
    [ -e "$proj" ] || continue
    dir=$(dirname "$proj")
    echo "=== $dir"
    if output=$(dotnet run --project "$dir" 2>&1); then
        echo "$output" | grep -E "cases passed|returned: .* in [0-9]+ ms"
        echo "    ok"
    else
        echo "$output"
        failed+=("$dir")
    fi
done

for dir in */python; do
    ls "$dir"/test_*.py > /dev/null 2>&1 || continue
    echo "=== $dir"
    if output=$(cd "$dir" && python3 -m unittest 2>&1); then
        echo "$output" | grep -E "^(Ran|OK)"
        echo "    ok"
    else
        echo "$output"
        failed+=("$dir")
    fi
done

echo
if [ ${#failed[@]} -eq 0 ]; then
    echo "all harnesses passed"
else
    echo "failed: ${failed[*]}"
    exit 1
fi
