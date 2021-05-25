namespace MergeSortApp
{
    using System.Collections.Generic;
    using System.Linq;

    public class ParallelMergeSort
    {
        // array is m
        public List<int> MergeSort(List<int> array)
        {
            //if length of m ≤ 1 then
            //return m
            if (array.Count <= 1)
            {
                return array;
            }

            //var left := empty list
            //var right:= empty list
            var left = new List<int>();
            var right = new List<int>();

            //for each x with index i in m do
            //    if i is odd then
            //add x to left
            //else
            //add x to right
            for (int i = 0; i < array.Count; i++)
            {
                if (i % 2 != 0)
                {
                    left.Add(array[i]);
                }
                else
                {
                    right.Add(array[i]);
                }
            }

            //left:= merge_sort(left)
            //right:= merge_sort(right)
            left = MergeSort(left);
            right = MergeSort(right);

            // Then merge the now-sorted sublists.
            //return merge(left, right)
            return Merge(left, right);
        }

        private List<int> Merge(List<int> left, List<int> right)
        {
            //var result := empty list
            var result = new List<int>();

            //while left is not empty and right is not empty do
            //    if first(left) ≤ first(right) then
            //    append first(left) to result
            //left:= rest(left)
            //else
            //append first(right) to result
            //right:= rest(right)
            int l = 0; 
            int r = 0;
            int m = 0;
            while (left.Any() && right.Any())
            {
                if (left[0] <= right[0])
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else
                {
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }

            //while left is not empty do
            //    append first(left) to result
            //left := rest(left)
            //while right is not empty do
            //    append first(right) to result
            //right := rest(right)
            while (left.Any())
            {
                result.Add(left[0]);
                left.Remove(left[0]);
            }

            while (right.Any())
            {
                result.Add(right[0]);
                right.Remove(right[0]);
            }

            return result;
        }
    }
}