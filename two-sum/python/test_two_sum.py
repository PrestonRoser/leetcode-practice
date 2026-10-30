import unittest

from two_sum import Solution


class TestTwoSum(unittest.TestCase):
    def check(self, nums, target):
        print(f"\n  nums={nums}  target={target}")
        result = Solution().twoSum(nums, target)
        print(f"  returned: {result!r}  ({type(result).__name__})")
        if isinstance(result, list) and len(result) == 2:
            try:
                i, j = result
                print(f"  nums[{i}] + nums[{j}] = {nums[i]} + {nums[j]} = {nums[i] + nums[j]}")
            except (TypeError, IndexError) as e:
                print(f"  can't index nums with {result!r}: {e}")

        self.assertIsInstance(result, list, f"expected a list of two indices, got {result!r}")
        self.assertEqual(len(result), 2, f"expected two indices, got {result!r}")
        i, j = result
        self.assertIsInstance(i, int, f"indices should be ints, got {result!r}")
        self.assertIsInstance(j, int, f"indices should be ints, got {result!r}")
        self.assertNotEqual(i, j, f"used the same element twice: {result!r}")
        self.assertEqual(nums[i] + nums[j], target,
                         f"nums[{i}] + nums[{j}] = {nums[i]} + {nums[j]}, not {target}")

    # the three LeetCode examples
    def test_example_1(self):
        self.check([2, 7, 11, 15], 9)

    def test_example_2(self):
        self.check([3, 2, 4], 6)

    def test_duplicate_values(self):
        self.check([3, 3], 6)

    # edge cases
    def test_dont_reuse_same_element(self):
        # 3 + 3 = 6, but there's only one 3
        self.check([3, 2, 4], 6)

    def test_negatives(self):
        self.check([-3, 4, 3, 90], 0)

    def test_answer_in_middle(self):
        self.check([1, 5, 8, 12, 20, 2], 20)

    def test_zeros(self):
        self.check([0, 4, 3, 0], 0)


if __name__ == "__main__":
    unittest.main(verbosity=2)
