using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_Graphical_Solution
{
    public class Constraint
    {
        private int xOneCoeff;
        private int xTwoCoeff;
        private string sign;
        private int rHS;

        public Constraint(int xOneCoeff,int xTwoCoeff,string sign,int rHS)
        {
            XOneCoeff = xOneCoeff;
            XTwoCoeff = xTwoCoeff;
            Sign = sign;
            RHS = rHS;
        }

        public int XOneCoeff
        {
            get
            {
                return xOneCoeff;
            }

            set
            {
                xOneCoeff = value;
            }
        }

        public int XTwoCoeff
        {
            get
            {
                return xTwoCoeff;
            }

            set
            {
                xTwoCoeff = value;
            }
        }

        public int RHS
        {
            get
            {
                return rHS;
            }

            set
            {
                rHS = value;
            }
        }

        public string Sign
        {
            get
            {
                return sign;
            }

            set
            {
                sign = value;
            }
        }
    }
}
