public class Solution
{
    // Given nums and a target, return the indices of the two numbers
    // that add up to target. Exactly one answer exists, and you can't
    // use the same element twice. Any order is fine: [0, 1] or [1, 0].
    public int[] TwoSum(int[] nums, int target)
    {
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
    }
}
