using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using kasd_labs_winforms;

namespace kasd_labs_winforms
{
    public delegate void Call_Sort(int[] mass);
    public partial class Form1 : Form
    {
        private GraphPane panel;
        int size = 0;

        private string gen_group = null;
        int[][] gen_mass = null;
        bool gened = false;

        private string alg_group = null;
        int[][] sort_mass = null;
        bool executed = false;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel = zedGraphControl1.GraphPane;
            panel.CurveList.Clear();
        }
        private void call_sort(Call_Sort fn, string name, Color color)
        {
            PointPairList list = new PointPairList();
            Stopwatch sw;
            for (int i = 0; i < sort_mass.Length; i++)
            {
                double middle = 0;
                for (int count = 0; count < 20; count++)
                {
                    Array.Copy(gen_mass[i], sort_mass[i], gen_mass[i].Length);
                    sw = new Stopwatch();
                    sw.Start();
                    fn(sort_mass[i]);
                    sw.Stop();
                    middle += sw.ElapsedMilliseconds;
                }
                middle /= 20;
                list.Add(i, middle);
            }
            panel.AddCurve(name, list, color, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gen_group = comboBox1.SelectedItem.ToString();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            alg_group = comboBox2.SelectedItem.ToString();
            switch (alg_group)
            {
                case "group 1":
                    size = 4;
                    break;
                case "group 2":
                    size = 5;
                    break;
                case "group 3":
                    size = 6;
                    break;
                default:
                    break;
            }
            gen_mass = new int[size][];
            sort_mass = new int[size][];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (size == 0 || gen_group == null) { return; }
            int s = 10;
            for (int i = 0; i < size; i++)
            {
                switch (gen_group)
                {
                    case "sort_mass":
                        gen_mass[i] = Generation.GenSortMass(s);
                        break;
                    case "unsort_mass":
                        gen_mass[i] = Generation.GenUnsortMass(s);
                        break;
                    case "random_mass":
                        gen_mass[i] = Generation.GenRandomMass(s);
                        break;
                    case "submassives_mass":
                        gen_mass[i] = Generation.GenSubmassivesMass(s);
                        break;
                    case "swap_mass":
                        gen_mass[i] = Generation.GenSwapMass(s);
                        break;
                    case "replace_mass":
                        gen_mass[i] = Generation.GenReplaceMass(s);
                        break;
                    case "repeat_mass":
                        gen_mass[i] = Generation.GenRepeatMass(s);
                        break;
                    default:
                        break;
                }
                sort_mass[i] = new int[s];
                s *= 10;
            }
            gened = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!gened || alg_group == null) { return; }
            Call_Sort d;
            switch (alg_group)
            {
                case "group 1":
                    d = new Call_Sort(Sorting.SortBubble);
                    call_sort(d, "bubble", Color.Blue);
                    d = new Call_Sort(Sorting.SortShaker);
                    call_sort(d, "shaker", Color.Red);
                    d = new Call_Sort(Sorting.SortGnome);
                    call_sort(d, "gnome", Color.Green);
                    break;
                case "group 2":
                    d = new Call_Sort(Sorting.SortBitonic);
                    call_sort(d, "bitonic", Color.Blue);
                    d = new Call_Sort(Sorting.SortShell);
                    call_sort(d, "shell", Color.Red);
                    d = new Call_Sort(Sorting.SortTree);
                    call_sort(d, "tree", Color.Green);
                    break;
                case "group 3":
                    d = new Call_Sort(Sorting.SortComb);
                    call_sort(d, "comb", Color.Green);
                    d = new Call_Sort(Sorting.SortHeap);
                    call_sort(d, "heap", Color.Red);
                    d = new Call_Sort(Sorting.SortQuick);
                    call_sort(d, "quick", Color.Orange);
                    d = new Call_Sort(Sorting.SortMerge);
                    call_sort(d, "merge", Color.Black);
                    d = new Call_Sort(Sorting.SortCounting);
                    call_sort(d, "counting", Color.Blue);
                    d = new Call_Sort(Sorting.SortRadix);
                    call_sort(d, "radix", Color.Pink);
                    break;
                default:
                    break;
            }
            executed = true;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!executed) { return; }
            StreamWriter gen_file = new StreamWriter("../gen_file.txt");
            for (int i = 0; i < gen_mass.Length; i++)
            {
                for (int j = 0; j < gen_mass[i].Length; j++)
                {
                    await gen_file.WriteAsync($"{gen_mass[i][j]} ");
                }
                await gen_file.WriteLineAsync();
            }

            StreamWriter sort_file = new StreamWriter("../sort_file.txt");
            for (int i = 0; i < sort_mass.Length; i++)
            {
                for (int j = 0; j < sort_mass[i].Length; j++)
                {
                    await sort_file.WriteAsync($"{sort_mass[i][j]} ");
                }
                await sort_file.WriteLineAsync();
            }
        }
    }
}
