using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace LP_Graphical_Solution.Classes
{
    class DataHandler
    {

        public LiniarModel ReadProblem()
        {
            LiniarModel newProblem = null;
            List<Constraint> newConstraints = new List<Constraint>();
            bool newProblemMax = true;
            int newXOneObjective = 0;
            int newXTwoObjective = 0;
            string newRestrictionOne = null;
            string newRestrictionTwo = null;
            string newSign = null;
            try
            {
                FileStream fs = new FileStream("Problem.txt", FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(fs);
                string readLine = reader.ReadLine();
                string[] arrFile = new string[4];
                arrFile = readLine.Split(' ');
                if (arrFile[0] == "Min")
                {
                    newProblemMax = false;
                }
                newXOneObjective = int.Parse(arrFile[1]);
                newXTwoObjective = int.Parse(arrFile[2]);
                try
                {
                    while (readLine != null)
                    {
                        readLine = reader.ReadLine();
                        arrFile = readLine.Split(' ');
                        if (arrFile[0] == "+" || arrFile[0] == "-" || arrFile[0] == "urs")
                        {
                            newRestrictionOne = arrFile[0];
                            newRestrictionTwo = arrFile[1];
                            break;
                        }
                        if (arrFile[2] == "<=")
                        {
                            newSign = "Less";
                        }
                        else if (arrFile[2] == ">=")
                        {
                            newSign = "Greater";
                        }
                        else { newSign = "Equal"; }
                        newConstraints.Add(new Constraint(int.Parse(arrFile[0]), int.Parse(arrFile[1]), newSign, int.Parse(arrFile[3])));
                    }
                    newProblem = new LiniarModel(newProblemMax, newXOneObjective, newXTwoObjective, newConstraints, newRestrictionOne, newRestrictionTwo);
                    reader.Close();
                    fs.Close();
                    
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("File Format Incorrect", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("File Not Found","Error",System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            return newProblem;

        }
    }
}
