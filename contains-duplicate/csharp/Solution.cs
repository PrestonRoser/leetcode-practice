public class Solution
{
    // Given an array of integers, return true if any value appears at
    // least twice, and false if every value is distinct.
    public bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> set = new HashSet<int>();
        bool added;

        for (int i = 0; i < nums.Length; i++)
        {
            added = set.Add(nums[i]); 

            if (!added)
            {
                return true;
            }
        }

        return false;
    }
}
