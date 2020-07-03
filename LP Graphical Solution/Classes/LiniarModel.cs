using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP_Graphical_Solution.Classes
{
    class LiniarModel
    {
        private bool problemMax;
        private int xOneObjective;
        private int xTwoObjective;
        private List<Constraint> constraints;
        private string restrictionOne;
        private string restrictionTwo;

        public LiniarModel(bool problemMax,int xOneObjective,int xTwoObjective, List<Constraint> constraints,string restrictionOne, string restrictionTwo)
        {
            ProblemMax = problemMax;
            XOneObjective = xOneObjective;
            XTwoObjective = xTwoObjective;
            Constraints = constraints;
            RestrictionOne = restrictionOne;
            RestrictionTwo = restrictionTwo;
        }
        public bool ProblemMax
        {
            get
            {
                return problemMax;
            }

            set
            {
                problemMax = value;
            }
        }

        public int XOneObjective
        {
            get
            {
                return xOneObjective;
            }

            set
            {
                xOneObjective = value;
            }
        }

        public int XTwoObjective
        {
            get
            {
                return xTwoObjective;
            }

            set
            {
                xTwoObjective = value;
            }
        }

        internal List<Constraint> Constraints
        {
            get
            {
                return constraints;
            }

            set
            {
                constraints = value;
            }
        }

        public string RestrictionOne
        {
            get
            {
                return restrictionOne;
            }

            set
            {
                restrictionOne = value;
            }
        }

        public string RestrictionTwo
        {
            get
            {
                return restrictionTwo;
            }

            set
            {
                restrictionTwo = value;
            }
        }
    }
}
