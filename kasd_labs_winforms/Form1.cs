using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using kasd_labs_winforms;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace kasd_labs_winforms
{
    public partial class Form1 : Form
    {

        int size;
        private GraphPane panel;

        private string gen_group;
        private string alg_group;
        private string currentType;
        

        Solver<int> intSolver;
        Solver<double> doubleSolver;
        Solver<char> charSolver;



        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += SelectGenerationGroup;
            comboBox2.SelectedIndexChanged += SelectArraysGroup;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel = zedGraphControl1.GraphPane;
            panel.CurveList.Clear();


            intSolver = new Solver<int>();
            doubleSolver = new Solver<double>();
            charSolver = new Solver<char>();
        }


        private void ViewResult<T>(List<(int, double)> sortingTime, string name, Color color)
        {
            PointPairList list = new PointPairList();

            for (int i = 0; i < sortingTime.Count; i++)
            {
                list.Add(sortingTime[i].Item1, sortingTime[i].Item2);
            }
            panel.AddCurve(name, list, color, SymbolType.None);
            
            
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        

        private void SelectGenerationGroup(object sender, EventArgs e)
        {
            gen_group = comboBox1.SelectedItem.ToString();
        }
        private void SelectArraysGroup(object sender, EventArgs e)
        {
            alg_group = comboBox2.SelectedItem.ToString();
        }
        private void SelectType(object sender, EventArgs e)
        {
            currentType = comboBox3.SelectedItem.ToString();
        }

        
        private void SortButtonClickHandler(object sender, EventArgs e)
        {

            if (currentType == "int")
                IntSort();

            else if (currentType == "double")
                DoubleSort();

            else if (currentType == "char")
                CharSort();
        }

        void IntSort()
        {
            Action<int[], Comparer<int>> sorting;

            if (alg_group == "group 1")
            {
                size = 4;
                intSolver.GenerateArrays(size, gen_group);

                sorting = Sorting.SortBubble;
                ViewResult<int>(intSolver.SortingTimes(sorting), "bubble", Color.Blue);

                sorting = Sorting.SortShaker;
                ViewResult<int>(intSolver.SortingTimes(sorting), "shaker", Color.Red);

                sorting = Sorting.SortGnome;
                ViewResult<int>(intSolver.SortingTimes(sorting), "gnome", Color.Green);
            }
            else if (alg_group == "group 2")
            {
                size = 5;
                intSolver.GenerateArrays(size, gen_group);

                sorting = Sorting.SortBitonic;
                ViewResult<int>(intSolver.SortingTimes(sorting), "bitonic", Color.Blue);

                sorting = Sorting.SortShell;
                ViewResult<int>(intSolver.SortingTimes(sorting), "shell", Color.Red);

                sorting = Sorting.SortTree;
                ViewResult<int>(intSolver.SortingTimes(sorting), "tree", Color.Green);
            }
            else if (alg_group == "group 3")
            {
                size = 6;
                intSolver.GenerateArrays(size, gen_group);

                sorting = Sorting.SortComb;
                ViewResult<int>(intSolver.SortingTimes(sorting), "comb", Color.Green);

                sorting = Sorting.SortHeap;
                ViewResult<int>(intSolver.SortingTimes(sorting), "heap", Color.Red);

                sorting = Sorting.SortQuick;
                ViewResult<int>(intSolver.SortingTimes(sorting), "quick", Color.Orange);

                sorting = Sorting.SortMerge;
                ViewResult<int>(intSolver.SortingTimes(sorting), "merge", Color.Black);

                sorting = Sorting.SortCounting;
                ViewResult<int>(intSolver.SortingTimes(sorting), "counting", Color.Blue);

                sorting = Sorting.SortRadix;
                ViewResult<int>(intSolver.SortingTimes(sorting), "radix", Color.Pink);
            }
        }
        void DoubleSort()
        {
            Action<double[], Comparer<double>> d;


            if (alg_group == "group 1")
            {
                size = 4;
                doubleSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortBubble;
                ViewResult<double>(doubleSolver.SortingTimes(d), "bubble", Color.Blue);

                d = Sorting.SortShaker;
                ViewResult<double>(doubleSolver.SortingTimes(d), "shaker", Color.Red);

                d = Sorting.SortGnome;
                ViewResult<double>(doubleSolver.SortingTimes(d), "gnome", Color.Green);
            }
            else if (alg_group == "group 2")
            {
                size = 5;
                doubleSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortBitonic;
                ViewResult<double>(doubleSolver.SortingTimes(d), "bitonic", Color.Blue);

                d = Sorting.SortShell;
                ViewResult<double>(doubleSolver.SortingTimes(d), "shell", Color.Red);

                d = Sorting.SortTree;
                ViewResult<double>(doubleSolver.SortingTimes(d), "tree", Color.Green);
            }
            else if (alg_group == "group 3")
            {
                size = 6;
                doubleSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortComb;
                ViewResult<double>(doubleSolver.SortingTimes(d), "comb", Color.Green);

                d = Sorting.SortHeap;
                ViewResult<double>(doubleSolver.SortingTimes(d), "heap", Color.Red);

                d = Sorting.SortQuick;
                ViewResult<double>(doubleSolver.SortingTimes(d), "quick", Color.Orange);

                d = Sorting.SortMerge;
                ViewResult<double>(doubleSolver.SortingTimes(d), "merge", Color.Black);

                d = Sorting.SortCounting;
                ViewResult<double>(doubleSolver.SortingTimes(d), "counting", Color.Blue);

                d = Sorting.SortRadix;
                ViewResult<double>(doubleSolver.SortingTimes(d), "radix", Color.Pink);
            }
        }

        void CharSort()
        {
            Action<char[], Comparer<char>> d;


            if (alg_group == "group 1")
            {
                size = 4;
                charSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortBubble;
                ViewResult<char>(charSolver.SortingTimes(d), "bubble", Color.Blue);
                d = Sorting.SortShaker;
                ViewResult<char>(charSolver.SortingTimes(d), "shaker", Color.Red);
                d = Sorting.SortGnome;
                ViewResult<char>(charSolver.SortingTimes(d), "gnome", Color.Green);
            }
            else if (alg_group == "group 2")
            {
                size = 5;
                charSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortBitonic;
                ViewResult<char>(charSolver.SortingTimes(d), "bitonic", Color.Blue);
                d = Sorting.SortShell;
                ViewResult<char>(charSolver.SortingTimes(d), "shell", Color.Red);
                d = Sorting.SortTree;
                ViewResult<char>(charSolver.SortingTimes(d), "tree", Color.Green);
            }
            else if (alg_group == "group 3")
            {
                size = 6;
                charSolver.GenerateArrays(size, gen_group);

                d = Sorting.SortComb;
                ViewResult<char>(charSolver.SortingTimes(d), "comb", Color.Green);
                d = Sorting.SortHeap;
                ViewResult<char>(charSolver.SortingTimes(d), "heap", Color.Red);
                d = Sorting.SortQuick;
                ViewResult<char>(charSolver.SortingTimes(d), "quick", Color.Orange);
                d = Sorting.SortMerge;
                ViewResult<char>(charSolver.SortingTimes(d), "merge", Color.Black);
                d = Sorting.SortCounting;
                ViewResult<char>(charSolver.SortingTimes(d), "counting", Color.Blue);
                d = Sorting.SortRadix;
                ViewResult<char>(charSolver.SortingTimes(d), "radix", Color.Pink);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (currentType == "int")
                intSolver.SaveResultsToFile();
            if (currentType == "double")
                doubleSolver.SaveResultsToFile();
            if (currentType == "char")
                charSolver.SaveResultsToFile();
        }

    }
}
