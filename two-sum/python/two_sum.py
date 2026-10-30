class Solution:
    def twoSum(self, nums: list[int], target: int) -> list[int]:
        # Given nums and a target, return the indices of the two numbers
        # that add up to target. Exactly one answer exists, and you can't
        # use the same element twice. Any order is fine: [0, 1] or [1, 0].

        # paste your loop here (body lines start 8 spaces in)
        for a, b in enumerate(nums):
            print("b: ", b)
            for c, d in reversed(list(enumerate(nums))):
                print("d: ", d)
                if c <= a:
                    break
                elif (b + d) == target:
                    print(b + d)
                    return [a, c]

        return