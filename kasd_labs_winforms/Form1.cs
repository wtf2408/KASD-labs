using System;
using System.IO;
using System.Windows.Forms;

namespace kasd_labs_winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string matrixView = "Matrix:\n";
            string vectorView = "vector:\n";

            string[] lines = File.ReadAllLines("D:\\KUBGU\\kasd-labs\\kasd_labs_winforms\\cond.txt");
            int n = int.Parse(lines[0]);

            double[,] G = new double[n, n];
            double[] x = new double[n];

            // Заполнение матрицы G
            for (int i = 0; i < n; i++)
            {
                matrixView += "(";
                string[] line = lines[i + 1].Split();
                for (int j = 0; j < n; j++)
                {
                    matrixView += $" {line[j]} ";
                    G[i, j] = double.Parse(line[j]);
                }
                matrixView += ")\n";
            }
            // Заполнение вектора x
            string[] xLine = lines[n + 1].Split();
            vectorView += "(";
            for (int i = 0; i < n; i++)
            {
                vectorView += $" {xLine[i]} ";
                x[i] = double.Parse(xLine[i]);
            }
            vectorView += ")\n";

            // Проверка на симметричность матрицы G
            if (!IsSymmetric(G, n))
            {
                MessageBox.Show("Матрица G не является симметричной.");
                return;
            }

            // Вычисление длины вектора
            double length = CalculateLength(x, G, n);

            labelMatrix.Text = matrixView;
            vectorLabel.Text = vectorView;
            label1.Text = $"Длина вектора: {Math.Floor(length)}";
        }

        static bool IsSymmetric(double[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] != matrix[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static double CalculateLength(double[] x, double[,] G, int n)
        {
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += x[i] * G[i, j] * x[j];
                }
            }
            return Math.Sqrt(Math.Floor(sum));
        }
    }
}
