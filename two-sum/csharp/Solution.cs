public class Solution
{
    // Given nums and a target, return the indices of the two numbers
    // that add up to target. Exactly one answer exists, and you can't
    // use the same element twice. Any order is fine: [0, 1] or [1, 0].
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary <int,List<int>> dict = new Dictionary<int, List<int>>(); // initialize a dict with a list holding its values
        int difference; // initialize difference var
        for (int i = 0; i < nums.Length; i++) // load the dict
        {
            if (!dict.TryGetValue(nums[i], out var list)) // see if an entry already exists for our value, if not, create it
            {
                list = new List<int>(); // initialize new list
                dict[nums[i]] = list; // create list at the key of our array value
            }

            list.Add(i); // adds our array value's index to the list (pre-existing or new); was originally `Dictionary <int,int> dict = new Dictionary<int, int>();` with `dict.Add(nums[i], i);`
        }

        for (int j = 0; j < nums.Length; j++) // check if two numbers in the array add up to the target
        {
            int x = nums[j]; // for first run: first val x is the first index of the dict's val, then indexes up
            while (true)
            {
                difference = target - x; // difference of target minus the current val in the array/dict
                
                if (!dict.TryGetValue(difference, out var result)) // if the difference val doesnt exist in the dict, break and try next index
                {
                    break;
                } else {
                    if (result.Count > 1)
                    {
                        if (result[0] == j)
                        {
                            break;
                        } else
                        {
                            return new int [] {result[0], result[1]};
                        }
                    } else if (result[0] == j) // identifies if the index matches current index (can only use num once)
                    {
                        break;
                    } else
                    {
                        return new int [] {j, result[0]}; // if it does exist, return the current index in the array the the difference's index
                    }
                }
            }
        }
        return new int [] {-1, -1}; // if it goes through the entire array and nothing exists, return "null" (basically)
    }
}
