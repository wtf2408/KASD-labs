using kasd_labs_console;
using System;
using System.Diagnostics;
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
        const int count1 = 10_000;
        const int count2 = 30_000;
        const int count3 = 50_000;
        const int count4 = 100_000;


        GraphPane pane;
        PointPairList listHM = new PointPairList();
        PointPairList listTM = new PointPairList();
        double[][] times;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Testing.GetTimes
                                (Testing.GetTest,count1);
                                break;
                            case 1:
                                times = Testing.GetTimes
                                (Testing.GetTest,count2);
                                break;
                            case 2:
                                times = Testing.GetTimes
                                (Testing.GetTest,count3);
                                break;
                            case 3:
                                times = Testing.GetTimes(Testing.GetTest,count4);
                                break;
                        }
                        break;
                    case 1:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Testing.GetTimes
                                (Testing.PutTest,
                                count1);
                                break;
                            case 1:
                                times = Testing.GetTimes
                                (Testing.PutTest,
                                count2);
                                break;
                            case 2:
                                times = Testing.GetTimes
                                (Testing.PutTest,
                                count3);
                                break;
                            case 3:
                                times = Testing.GetTimes
                                (Testing.PutTest,
                                count4);
                                break;
                        }
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Testing.GetTimes
                                (Testing.RemoveTest,
                                count1);
                                break;
                            case 1:
                                times = Testing.GetTimes
                                (Testing.RemoveTest,
                                count2);
                                break;
                            case 2:
                                times = Testing.GetTimes
                                (Testing.RemoveTest,
                                count3);
                                break;
                            case 3:
                                times = Testing.GetTimes
                                (Testing.RemoveTest,
                                count4);
                                break;
                        }
                        break;
                }
                if (0 <= comboBox1.SelectedIndex &&
                comboBox1.SelectedIndex <= 2 &&
                    0 <= comboBox2.SelectedIndex &&
                    comboBox2.SelectedIndex <= 3)
                {
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();

                    zedGraphControl1.Show();
                    pane = zedGraphControl1.GraphPane;
                    pane.Legend.FontSpec.Size = 16;
                    pane.YAxis.Title.Text = "Время (мс)";

                    pane.CurveList.Clear();
                    listHM.Clear();
                    listTM.Clear();

                    averages = Testing.GetMatrixAverage(times);
                    for (int i = 0; i < 20; i++)
                        listHM.Add(i + 1, times[i][0]);
                    LineItem curveHM = pane.AddCurve("MyHashMap", listHM,
                        Color.Red, SymbolType.None);
                    for (int i = 0; i < 20; i++)
                        listTM.Add(i + 1, times[i][1]);
                    LineItem curveTM = pane.AddCurve("MyTreeMap", listTM,
                        Color.Blue, SymbolType.None);

                    pane.XAxis.Scale.Min = 1;
                    pane.XAxis.Scale.Max = 20;

                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
