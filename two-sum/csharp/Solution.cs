public class Solution
{
    // Given nums and a target, return the indices of the two numbers
    // that add up to target. Exactly one answer exists, and you can't
    // use the same element twice. Any order is fine: [0, 1] or [1, 0].
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary <int,int> dict = new Dictionary<int, int>(); // initialize a dict
        int difference; // initialize difference var
        for (int i = 0; i < nums.Length; i++) // load the dict
        {
            dict[nums[i]] = i; // replaces duplicate variables with the last instance of it within the array
        }

        for (int j = 0; j < nums.Length; j++) // check if two numbers in the array add up to the target
        {
            int x = nums[j]; // for first run: first val x is the first index of the dict's val, then indexes up
            while (true)
            {
                difference = target - x; // difference of target minus the current val in the array/dict

                if (!dict.TryGetValue(difference, out int result)) // if the difference val doesnt exist in the dict, break and try next index
                {
                    break;
                } else {
                    if (result == j) // identifies if the index matches current index (can only use num once)
                    {
                        break;
                    } else
                    {
                        return new int [] {j, result}; // if it does exist, return the current index in the array the the difference's index
                    }
                }
            }
        }

        return new int [] {-1, -1}; // if it goes through the entire array and nothing exists, return "null" (basically)
    }
}