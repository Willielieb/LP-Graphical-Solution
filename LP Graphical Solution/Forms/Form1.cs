using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LP_Graphical_Solution.Classes;

namespace LP_Graphical_Solution
{
    public partial class Form1 : Form
    {
        
        #region Variables
        DataHandler dh = new DataHandler();
        List<Constraint> constraints = new List<Constraint>();
        List<Line> lines = null;
        LiniarModel lm = null;
        List<double> listofxs = new List<double>();
        List<double> listofys = new List<double>();
        #endregion
        #region FormMethods
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            lister();
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TxtX1.Text != "")
            {
                ChangingConstraints();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // lister();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            // lister();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // lister();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtX1.Clear();
            TxtX2.Clear();
            CBSign.Items.Clear();
            TxtRHS.Clear();
            //listBox3.Items.Clear();

            foreach (Constraint item in constraints)
            {
                string sign;
                if (item.Sign.Equals("Less"))
                {
                    sign = "<=";
                }
                else if (item.Sign.Equals("Greater"))
                {
                    sign = ">=";
                }
                else
                {
                    sign = "=";
                }
                string temp = item.XOneCoeff + "X1 + " + item.XTwoCoeff + "X2 " + sign + " " + item.RHS;
                if (temp.Equals(LBDisplay.SelectedItem))
                {
                    TxtX1.Text = item.XOneCoeff.ToString();
                    TxtX2.Text = item.XTwoCoeff.ToString();
                    CBSign.Items.Add(item.Sign);
                    TxtRHS.Text = item.RHS.ToString();
                }
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Runs the adding of charts
        /// </summary>
        public void Charter()
        {
            CreateObjectiveFunc();

            List<double> listofrangepoints = new List<double>();
            int i = 2;
            int j = 2;
            var chart = chart1.ChartAreas[0];
            chart.AxisX.Interval = 5;
            chart.AxisX.Minimum = 0;
            chart.AxisY.Interval = 5;
            chart.AxisY.Minimum = 0;

            foreach (Constraint item in constraints)
            {
                chart1.Series.Add("Constraint" + i.ToString());

                chart1.Series["Constraint" + i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


                //chart1.Series[i].IsVisibleInLegend = false;
                if (item.Sign == "Less")
                {
                    chart1.Series[i].Color = Color.DarkGoldenrod;
                }
                else if (item.Sign == "Greater")
                {
                    chart1.Series[i].Color = Color.Red;
                }
                else
                {
                    chart1.Series[i].Color = Color.Green;
                }
                string sign;
                if (item.Sign.Equals("Less"))
                {
                    sign = "<=";
                }
                else if (item.Sign.Equals("Greater"))
                {
                    sign = ">=";
                }
                else
                {
                    sign = "=";
                }
                LBDisplay.Items.Add(item.XOneCoeff + "X1 + " + item.XTwoCoeff + "X2 " + sign + " " + item.RHS);
              
                listofxs.Add(item.XOneCoeff);
                listofys.Add(item.XTwoCoeff);

                i++;
            }
            LBDisplay.Items.Add("X1,X2>=0");
            List<Line> points = CalculatePoints(constraints);
            foreach (Line item in points)
            {
                //add line points
                chart1.Series["Constraint"+j].Points.AddXY(item.PointOne, 0);
                chart1.Series["Constraint" + j].Points.AddXY(0, item.PointTwo);
                LBPoints.Items.Add(item.PointOne + ",0 0," + item.PointTwo);
                j++;
            }
            // feasible region add of points
            chart1.Series[0].Points.AddXY(listofxs.Min(), 0);
            chart1.Series[0].Points.AddXY(0, listofys.Min());
        }
        /// <summary>
        /// Calculates the points
        /// </summary>
        public List<Line> CalculatePoints(List<Constraint> constraintList)
        {
            lines = new List<Line>();
            foreach (Constraint item in constraintList)
            {
                lines.Add(new Line(item.RHS / item.XOneCoeff, item.RHS / item.XTwoCoeff));
            }
            return lines;
        }
        /// <summary>
         /// Creates a list of points
         /// </summary>
        public void lister()
        {
            try
            {
                lm = dh.ReadProblem();
            }
            catch (Exception)
            {
                MessageBox.Show("File Format Incorrect", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            listBox1.Items.Add(lm.ProblemMax);
            listBox1.Items.Add(lm.XOneObjective);
            listBox1.Items.Add(lm.XTwoObjective);

            foreach (Constraint item in lm.Constraints)
            {
                string sign;
                if (item.Sign.Equals("Less"))
                {
                    sign = "<=";
                }
                else if (item.Sign.Equals("Greater"))
                {
                    sign = ">=";
                }
                else
                {
                    sign = "=";
                }
                listBox1.Items.Add(item.XOneCoeff + " " + item.XTwoCoeff + " " + sign + " " + item.RHS);
                constraints.Add(item);

            }
            Charter();

            listBox1.Items.Add(lm.RestrictionOne);
            listBox1.Items.Add(lm.RestrictionTwo);
        }

        /// <summary>
        /// Creates objective function line
        /// </summary>
        public void CreateObjectiveFunc()
        {
            chart1.Series.Add("OBJ FUNC");
            chart1.Series["OBJ FUNC"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["OBJ FUNC"].Color = Color.Purple;
            Line ObjFuc = new Line(lm.XTwoObjective, lm.XOneObjective);
            chart1.Series["OBJ FUNC"].Points.AddXY(ObjFuc.PointOne, 0);
            chart1.Series["OBJ FUNC"].Points.AddXY(0, ObjFuc.PointTwo);

        }

        public void ChangingConstraints()
        {
            //for (int i = 2; i < constraints.Count+2; i++)
            //{
            //    foreach (Constraint item in constraints)
            //    {
            //        string temp = item.XOneCoeff + " " + item.XTwoCoeff + " " + item.Sign + " " + item.RHS;
            //        if (temp.Equals(listBox2.SelectedItem))
            //        {

            //            item.XOneCoeff = int.Parse(textBox1.Text);
            //            lines = CalculatePoints(constraints);
            //            chart1.Series[i].Points.RemoveAt(0);
            //            foreach (Line item1 in lines)
            //            {
            //                chart1.Series[i].Points.AddXY(item1.PointOne, 0);
            //            }

            //        }


            //    }
            //}
        }
        #endregion
        
    }
}
