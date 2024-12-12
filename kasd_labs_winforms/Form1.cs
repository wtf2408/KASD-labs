using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZedGraph;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kasd_labs_winforms
{
    public partial class Form1 : Form
    {
        int operationIndex = 0;
        String[] names = { "Add (value)", "Get (value)", "Set (index, value)", "Add (index, value)", "Remove (index)" };
        string name;
        Color color1;
        Color color2;
        Testing test;
        public Form1()
        {
            InitializeComponent();
            test = new Testing();
            color1 = Color.PowderBlue;
            color2 = Color.PaleGreen;
        }

        private void DrawGraph(int operationIndex)
        {
            GraphPane graphPane = zedGraphControl1.GraphPane;
            graphPane.CurveList.Clear();
            graphPane.Title.Text = name;
            graphPane.XAxis.Title.Text = "Ось X";
            graphPane.YAxis.Title.Text = "Ось Y";

            double[] X = { 100, 1000, 10000, 100000 };
            double[] Y1 = new double[4];
            double[] Y2 = new double[4];

            switch (operationIndex)
            {
                case 0:
                {
                    Y1 = test.TestAddArray();
                    Y2 = test.TestAddLinked();
                    
                    break;
                }
                case 1:
                {
                    Y1 = test.TestGetArray();
                    Y2 = test.TestGetLinked();

                    break;
                }
                case 2:
                {
                    Y1 = test.TestSetArray();
                    Y2 = test.TestSetLinked();

                    break;
                }
                case 3:
                {
                    Y1 = test.TestAddValueArray();
                    Y2 = test.TestAddValueLinked();

                    break;
                }
                case 4:
                {
                    Y1 = test.TestRemoveArray();
                    Y2 = test.TestRemoveLinked();

                    break;
                }

            }
            graphPane.AddCurve("MyArrayList", X, Y1, color1);
            graphPane.AddCurve("MyLinkedList", X, Y2, color2);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            operationIndex = comboBox1.SelectedIndex;
            name = comboBox1.SelectedText;
            DrawGraph(operationIndex);
        }
    }
}
