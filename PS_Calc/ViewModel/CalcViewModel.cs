using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PS_Calc.Commands;
using PS_Calc.Model;

namespace PS_Calc.ViewModel
{
    public class CalcViewModel : BaseViewModel
    {
        private bool _operandAllowed = false;
        private bool _clearDisplay = false;

        public CalcModel CalcModel { get; set; }
        public CalcViewModel()
        {
            CalcModel = new CalcModel();
        }

        public ICommand PressDigit => new RelayCommand<string>(DigitInput);

        public void DigitInput(string number)
        {
            if (_clearDisplay)
            {
                Display = "";
            }

            if (CalcModel.Operator == null)
            {
                CalcModel.FirstOperand += number;
                _operandAllowed = true;
            }
            else
            {
                CalcModel.SecondOperand += number;
            }

            Display += number;
        }

        public ICommand PressOperand => new RelayCommand<string>(OperandInput);

        public void OperandInput(string operation)
        {
            if (!string.IsNullOrEmpty(CalcModel.SecondOperand) && CalcModel.Operator != null && !string.IsNullOrEmpty(CalcModel.FirstOperand))
            {
                Calculate();
            }

            if (!_operandAllowed) return;
            switch (operation)
            {
                case ("+"):
                    Display += " + ";
                    CalcModel.Operator = Operator.Plus;
                    break;

                case ("-"):
                    Display += " - ";
                    CalcModel.Operator = Operator.Minus;
                    break;

                case ("*"):
                    Display += " * ";
                    CalcModel.Operator = Operator.Multiply;
                    break;

                case ("/"):
                    Display += " / ";
                    CalcModel.Operator = Operator.Divide;
                    break;
            }

            _operandAllowed = false; ;
        }


        public ICommand PressCalculate => new DelegateCommand(Calculate);

        public void Calculate()
        {
            if (string.IsNullOrEmpty(CalcModel.FirstOperand) || string.IsNullOrEmpty(CalcModel.SecondOperand) || CalcModel.Operator == null)
            {
                return;
            }

            try
            {
                var val1 = (double.Parse(CalcModel.FirstOperand));
                var val2 = (double.Parse(CalcModel.SecondOperand));

                if (val1 == 0 || val2 == 0)
                {
                    throw new DivideByZeroException("Cant divide by zero.");
                }

                switch (CalcModel.Operator)
                {
                    case Operator.Plus:
                        Display = (val1 + val2).ToString(CultureInfo.InvariantCulture);
                        break;

                    case Operator.Divide:
                        Display = (val1 / val2).ToString(CultureInfo.InvariantCulture);
                        break;

                    case Operator.Minus:
                        Display = (val1 - val2).ToString(CultureInfo.InvariantCulture);
                        break;

                    case Operator.Multiply:
                        Display = (val1 * val2).ToString(CultureInfo.InvariantCulture);
                        break;
                    case null:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                CalcModel.FirstOperand = Display;
                CalcModel.SecondOperand = null;
                CalcModel.Operator = null;
                _operandAllowed = true;
            }
            catch (Exception e)
            {
                Display = "Error! " + e.Message;
                CalcModel = new CalcModel();
                _operandAllowed = false;
                _clearDisplay = true;
            }
        }

        public ICommand PressClear => new DelegateCommand(Clear);

        public void Clear()
        {
            CalcModel = new CalcModel();
            _operandAllowed = false;
            Display = "";
        }

        public ICommand PressDot => new DelegateCommand(DotInput);

        public void DotInput()
        {
            if (CalcModel.SecondOperand == null)
            {
                CalcModel.FirstOperand += ".";
            }
            else
            {
                CalcModel.SecondOperand += ".";
            }

            Display += ".";

        }

        private string _display;
        public string Display
        {
            get => _display;

            set
            {
                _display = value;
                RaisePropertyChangedEvent("Display");
            }
        }
    }
}
