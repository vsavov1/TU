using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_Calc.Model
{
    public class CalcModel
    {
        public double Result { get; set; }

        public string FirstOperand { get; set; }

        public string SecondOperand { get; set; }

        public Operator? Operator { get; set; }
    }
}
