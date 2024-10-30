using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace kasd_labs_winforms
{
    class Generation
    {
        private static int rand_min = 0;
        private static int rand_max = 1000;
        private static int step_size_min = 2;
        private static int step_size_max = 50;
        private static int big_mass_size = 500;
        public static int[] GenSortMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));

            int step_size = size >= big_mass_size
                ? random.Next(step_size_min, step_size_max / 10)
                : random.Next(step_size_min, step_size_max);

            int[] mass = new int[size];
            int rmin = rand_min;
            for (int i = 0; i < size; i++)
            {
                mass[i] = random.Next(rmin, Min(rmin + step_size, rand_max));
                rmin = mass[i];
            }

            return mass;
        }
        public static int[] GenRandomMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            int[] mass = new int[size];
            for (int i = 0; i < size; i++)
            {
                mass[i] = random.Next(rand_min, rand_max);
            }

            return mass;
        }
        public static int[] GenUnsortMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));

            int step_size = size >= big_mass_size
                ? random.Next(step_size_min, step_size_max / 10)
                : random.Next(step_size_min, step_size_max);

            int[] mass = new int[size];
            int rmax = rand_max;
            for (int i = 0; i < size; i++)
            {
                mass[i] = random.Next(Max(rand_min, rmax - step_size), rmax);
                rmax = mass[i];
            }

            return mass;
        }
        public static int[] GenSubmassivesMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            int[] mass = new int[size];
            int index = 0;
            int rest_size = size;

            while (rest_size != 0)
            {
                int submass_size = rest_size >= big_mass_size
                    ? random.Next(1, rest_size / 100)
                    : random.Next(1, rest_size / 10);
                submass_size *= 10;
                int[] submass = GenSortMass(submass_size);
                for (int i = 0; i < submass_size; i++) { mass[index++] = submass[i]; }

                rest_size -= submass_size;
            }

            return mass;
        }
        public static int[] GenSwapMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            int[] mass = GenSortMass(size);
            int swap_size = random.Next(rand_min, size / 10);
            for (int i = 0; i < swap_size; i++)
            {
                int ind1 = random.Next(0, size - 1);
                int ind2 = random.Next(0, size - 1);

                Sorting.Swap(ref mass[ind1], ref mass[ind2]);
            }

            return mass;
        }
        public static int[] GenReplaceMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            int[] mass = GenSortMass(size);
            int swap_size = random.Next(rand_min, size);
            for (int i = 0; i < swap_size; i++)
            {
                int ind = random.Next(0, size - 1);
                int value = random.Next(rand_min, rand_max);

                mass[ind] = value;
            }
            return mass;
        }
        public static int[] GenRepeatMass(int size)
        {
            Random random = new Random(((int)DateTime.Now.Ticks));
            int[] mass = GenRandomMass(size);

            int[] mass_rep = { size / 10, size / 4, size / 2, 3 * (size / 4), 9 * (size / 10) };
            int rep_ind = random.Next(0, mass_rep.Length - 1);

            int ind = random.Next(0, size - mass_rep[rep_ind] - 1);
            int elem = mass[ind];
            for (int i = 0; i < mass_rep[rep_ind]; i++)
            {
                mass[ind++] = elem;
            }
            return mass;
        }
    }
}
