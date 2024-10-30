using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_winforms
{
    class Sorting
    {
        public static void Swap(ref int a, ref int b)
        {
            int value = a;
            a = b;
            b = value;
        }
        public static void SortBubble(int[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    if (mass[j] > mass[j + 1]) { Swap(ref mass[j], ref mass[j + 1]); }
                }
            }
        }
        public static void SortShaker(int[] mass)
        {
            int left_index = 1;
            int right_index = mass.Length - 1;
            while (left_index <= right_index)
            {
                for (int i = right_index; i >= left_index; i--)
                {
                    if (mass[i - 1] > mass[i]) { Swap(ref mass[i - 1], ref mass[i]); }
                }
                left_index++;

                for (int i = left_index; i <= right_index; i++)
                {
                    if (mass[i - 1] > mass[i]) { Swap(ref mass[i - 1], ref mass[i]); }
                }
                right_index--;
            }
        }
        public static void SortComb(int[] mass)
        {
            const double factor = 1.247;
            int iter = mass.Length - 1;

            while (iter >= 1)
            {
                for (int i = 0; i < mass.Length - iter; i++)
                {
                    if (mass[i] > mass[i + iter]) { Swap(ref mass[i], ref mass[i + iter]); }
                }
                iter = (int)(iter / factor);
            }

            SortBubble(mass);
        }
        public static void SortInsertion(int[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                int elem = mass[i];
                int j = i - 1;
                while (j >= 0 && mass[j] > elem)
                {
                    mass[j + 1] = mass[j--];
                }
                mass[j + 1] = elem;
            }
        }
        public static void SortShell(int[] mass)
        {
            int d = mass.Length / 2;
            while (d > 0)
            {
                for (int i = 0; i < mass.Length - d; i++)
                {
                    int j = i;
                    while (j >= 0 && mass[j] > mass[j + d])
                    {
                        Swap(ref mass[j], ref mass[j + d]);
                        j--;
                    }
                }
                d /= 2;
            }
        }

        private class Tree
        {
            public int value, height;
            public Tree left, right;
            public Tree(int value)
            {
                this.value = value;
                height = 1;
                left = right = null;
            }
            public void InsertTree(Tree node)
            {
                if (node.value < value)
                {
                    if (left == null) { left = node; }
                    else { left.InsertTree(node); }
                }
                else
                {
                    if (right == null) { right = node; }
                    else { right.InsertTree(node); }
                }
            }
            public void Transform(ref int[] mass, ref int index)
            {
                if (left != null) { left.Transform(ref mass, ref index); }
                mass[index++] = value;
                if (right != null) { right.Transform(ref mass, ref index); }
            }
        }
        public static void SortTree(int[] mass)
        {
            Tree tree = new Tree(mass[0]);
            for (int i = 1; i < mass.Length; i++)
            {
                tree.InsertTree(new Tree(mass[i]));
            }

            int index = 0;
            tree.Transform(ref mass, ref index);
        }

        public static void SortGnome(int[] mass)
        {
            int i = 1, j = 2;
            while (i < mass.Length)
            {
                if (mass[i - 1] <= mass[i])
                {
                    i = j++;
                }
                else
                {
                    int t = mass[i];
                    mass[i] = mass[i - 1];
                    mass[--i] = t;
                    if (i == 0)
                    {
                        i = j++;
                    }
                }
            }
        }
        public static void SortSelection(int[] mass)
        {
            for (int i = 0; i < mass.Length - 1; i++)
            {
                int min_ind = i;
                for (int j = i + 1; j < mass.Length; j++)
                {
                    if (mass[j] < mass[min_ind])
                    {
                        min_ind = j;
                    }
                }
                if (min_ind != i)
                {
                    Swap(ref mass[i], ref mass[min_ind]);
                }
            }
        }

        private static void Heapify(int[] mass, int n, int i)
        {
            int largest = i;
            int ind = 2 * i + 1;
            if (ind < n && mass[ind] > mass[largest]) { largest = ind; }
            if (ind + 1 < n && mass[ind + 1] > mass[largest]) { largest = ind + 1; }
            if (largest != i)
            {
                Swap(ref mass[i], ref mass[largest]);
                Heapify(mass, n, largest);
            }
        }
        public static void SortHeap(int[] mass)
        {
            int n = mass.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Sorting.Heapify(mass, n, i);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref mass[0], ref mass[i]);
                Sorting.Heapify(mass, i, 0);
            }
        }

        private static int MedianOfThree(int[] mass, int min_ind, int max_ind)
        {
            int mid_ind = (min_ind + max_ind) / 2;
            
            if (mass[min_ind] > mass[mid_ind]) { Swap(ref mass[min_ind], ref mass[mid_ind]); }
            if (mass[min_ind] > mass[max_ind]) { Swap(ref mass[min_ind], ref mass[max_ind]); }
            if (mass[mid_ind] > mass[max_ind]) { Swap(ref mass[mid_ind], ref mass[max_ind]); }

            return mid_ind;
        }
        private static int Partition(int[] mass, int min_ind, int max_ind)
        {
            int t;
            int p_ind = MedianOfThree(mass, min_ind, max_ind);
            Swap(ref mass[p_ind], ref mass[max_ind]);

            int p = min_ind - 1;
            for (int i = min_ind; i <= max_ind; i++)
            {
                if (mass[i] < mass[max_ind] || i == max_ind)
                {
                    t = mass[++p];
                    mass[p] = mass[i];
                    mass[i] = t;
                }
            }

            return p;
        }
        private static void SortInsertion(int[] mass, int min_ind, int max_ind)
        {
            for (int i = min_ind + 1; i <= max_ind; i++)
            {
                int key = mass[i];
                int j = i - 1;

                while(j >= min_ind && mass[j] > key) { mass[j + 1] = mass[j--]; }
                mass[j + 1] = key;
            }
        }
        private static void SortQuick(int[] mass, int min_ind, int max_ind)
        {
            while(min_ind < max_ind)
            {
                if (max_ind - min_ind < 10)
                {
                    SortInsertion(mass, min_ind, max_ind);
                    break;
                }

                int p = Partition(mass, min_ind, max_ind);
                if (p - min_ind < max_ind - p)
                {
                    SortQuick(mass, min_ind, p - 1);
                    min_ind = p + 1;
                }
                else
                {
                    SortQuick(mass, p + 1, max_ind);
                    max_ind = p - 1;
                }
            }
        }
        public static void SortQuick(int[] mass)
        {
            SortQuick(mass, 0, mass.Length - 1);
        }

        private static void Merge(int[] mass, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;
            int[] left_mass = new int[n1];
            int[] right_mass = new int[n2];
            Array.Copy(mass, left, left_mass, 0, n1);
            Array.Copy(mass, mid + 1, right_mass, 0, n2);

            int i = 0, j = 0, k = left;
            while(i < n1 && j < n2)
            {
                if(left_mass[i] <= right_mass[j]) { mass[k++] = left_mass[i++]; }
                else { mass[k++] = right_mass[j++]; }
            }
            while (i < n1) { mass[k++] = left_mass[i++]; }
            while (j < n2) { mass[k++] = right_mass[j++]; }
        }
        public static void SortMerge(int[] mass, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                SortMerge(mass, left, mid);
                SortMerge(mass, mid + 1, right);
                Merge(mass, left, mid, right);
            }
        }
        public static void SortMerge(int[] mass)
        {
            SortMerge(mass, 0, mass.Length - 1);
        }

        public static void SortCounting(int[] mass)
        {
            int min = mass[0];
            int max = mass[0];
            for (int i = 1; i < mass.Length; i++)
            {
                if (mass[i] < min) { min = mass[i]; }
                if (mass[i] > max) { max = mass[i]; }
            }

            int[] count = new int[max - min + 1];
            for (int i = 0; i< mass.Length; i++)
            {
                count[mass[i] - min]++;
            }

            int k = 0;
            for(int i = 0; i < count.Length; i++)
            {
                while(count[i]-- > 0)
                {
                    mass[k++] = i + min;
                }
            }
        }

        private static void CountingSortByDigit(int[] mass, int exp)
        {
            int[] output = new int[mass.Length];
            int[] count = new int[10];
            for (int i = 0; i < mass.Length; i++) { count[(mass[i] / exp) % 10]++; }
            for (int i = 1; i < 10; i++) { count[i] += count[i - 1]; }
            for (int i = mass.Length - 1; i >= 0; i--)
            {
                output[count[(mass[i] / exp) % 10] - 1] = mass[i];
                count[(mass[i] / exp) % 10]--;
            }
            Array.Copy(output, mass, mass.Length);
        }
        public static void SortRadix(int[] mass)
        {
            int max = mass[0];
            for (int i = 1; i < mass.Length; i++)
            {
                if (mass[i] > max) { max = mass[i]; }
            }
            int exp = 1;
            while (max / exp > 0)
            {
                CountingSortByDigit(mass, exp);
                exp *= 10;
            }
        }

        public static void MergeBitonic(int[] a, int low, int lenght, bool dir)
        {
            if (lenght > 1)
            {
                int k = lenght / 2;
                for (int i = low; i < low + k; i++)
                {
                    bool check = a[i] > a[i + k];
                    if (dir == check) { Swap(ref a[i], ref a[i + k]); }
                }
                MergeBitonic(a, low, k, dir);
                MergeBitonic(a, low + k, k, dir);
            }
        }
        public static void SortBitonic(int[] mass, int low, int lenght, bool dir)
        {
            if (lenght > 1)
            {
                int k = lenght / 2;
                SortBitonic(mass, low, k, true);
                SortBitonic(mass, low + k, k, false);
                MergeBitonic(mass, low, lenght, dir);
            }
        }
        public static void SortBitonic(int[] mass)
        {
            SortBitonic(mass, 0, mass.Length, true);
        }
    }
}
