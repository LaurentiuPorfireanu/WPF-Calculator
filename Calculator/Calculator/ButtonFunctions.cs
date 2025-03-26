using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    class ButtonFunctions
    {
        private readonly TextBlock displayTextBlock;
        private readonly TextBlock pastResultsTextBlock;
        private readonly NumberButtons numberButton;

        private double firstNumber = 0;
        private string currentOperation = "";
        private bool divideByZeroError = false;

        private List<double> memoryValue = new List<double>();
        private ComboBox memoryComboBox;

        public ButtonFunctions(TextBlock displayTextBlock, TextBlock pastResultsTextBlock, NumberButtons numberButton,ComboBox memoryComboBox)
        {
            this.displayTextBlock = displayTextBlock;
            this.pastResultsTextBlock = pastResultsTextBlock;
            this.numberButton = numberButton;
            this.memoryComboBox = memoryComboBox;
        }

        public void SetupFunctionality(bool programer,params Button[] buttons)
        {
            if(programer==false)
            {
                buttons[0].Click += (sender, e) => OperationButtonClick("+");
                buttons[1].Click += (sender, e) => OperationButtonClick("-");
                buttons[2].Click += (sender, e) => OperationButtonClick("*");
                buttons[3].Click += (sender, e) => OperationButtonClick("/");
                buttons[4].Click += (sender, e) => EqualButtonClick();

                buttons[5].Click += (sender, e) => NegateButtonClick();
                buttons[6].Click += (sender, e) => SqrtButtonClick();
                buttons[7].Click += (sender, e) => PowerButtonClick();
                buttons[8].Click += (sender, e) => ReciprocalButtonClick();
                buttons[9].Click += (sender, e) => OperationButtonClick("%");

                buttons[10].Click += (sender, e) => CeButtonClick();
                buttons[11].Click += (sender, e) => CButtonClick();
                buttons[12].Click += (sender, e) => DeleteButtonClick();
            }
            else
            {
                buttons[0].Click += (sender, e) => OperationButtonClick("+");
                buttons[1].Click += (sender, e) => OperationButtonClick("-");
                buttons[2].Click += (sender, e) => OperationButtonClick("*");
                buttons[3].Click += (sender, e) => OperationButtonClick("/");
                buttons[4].Click += (sender, e) => EqualButtonClick();

                buttons[5].Click += (sender, e) => NegateButtonClick();
                buttons[6].Click += (sender, e) => OperationButtonClick("%");

                buttons[7].Click += (sender, e) => CeButtonClick();
                buttons[8].Click += (sender, e) => CButtonClick();
                buttons[9].Click += (sender, e) => DeleteButtonClick();
            }
            


        }

        private void OperationButtonClick(string operation)
        {
            if(divideByZeroError)
            {
                CButtonClick();
                divideByZeroError = false;
            }
            else
            {
                EqualButtonClick();
            }
                firstNumber = numberButton.GetCurrentValue();
            currentOperation = operation;
            pastResultsTextBlock.Text = $"{displayTextBlock.Text}{operation}";
            
            numberButton.StartNewCalculation();
        }

        private void EqualButtonClick()
        {
            if (currentOperation == "" || divideByZeroError)
                return;

            double seccondNumber = numberButton.GetCurrentValue();
            double result = 0;
            bool succcesfulcalculation = true;
            switch(currentOperation)
            {
                case "+":
                    result = firstNumber + seccondNumber;
                    break;
                case "-":
                    result = firstNumber - seccondNumber;
                    break;
                case "*":
                    result = firstNumber * seccondNumber;
                    break;
                case "/":
                    if (seccondNumber != 0)
                    {
                        result = firstNumber / seccondNumber;
                    }
                    else
                    {
                        displayTextBlock.Text = "Cannot divide by 0";
                        MessageBox.Show("Division by zero is not allowed!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        divideByZeroError = true;
                        succcesfulcalculation = false;
                    }
                        break;
                case "%":
                    if(seccondNumber!=0)
                    {
                        result = firstNumber % seccondNumber;
                    }
                    else
                    {
                        displayTextBlock.Text = "Cannot perform modulo operation with 0";
                        MessageBox.Show("Modulo by zero is not allowed!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        divideByZeroError = true;
                        succcesfulcalculation = false;

                    }
                        break;
            }

            if(succcesfulcalculation)
            {
                pastResultsTextBlock.Text = $"{firstNumber}{currentOperation}{seccondNumber}=";
                numberButton.SetDisplayValue(result);
                currentOperation = "";
                numberButton.StartNewCalculation();
            }
               
        }

        private void NegateButtonClick()
        {
            if (divideByZeroError)
            {
                CButtonClick();
                divideByZeroError = false;
                return;
            }

            double value = numberButton.GetCurrentValue();
            if(displayTextBlock.Text!="0")
            {
                value = (-1) * value;
                numberButton.SetDisplayValue(value);
            }
        }

        private void SqrtButtonClick()
        {


            if (divideByZeroError)
            {
                CButtonClick();
                divideByZeroError = false;
                return;
            }

            double value = numberButton.GetCurrentValue();
            if(value>=0)
            {
                double result = Math.Sqrt(value);
                pastResultsTextBlock.Text=$"√{value}=";
                numberButton.SetDisplayValue(result);
                numberButton.StartNewCalculation();
            }
            else
            {
                displayTextBlock.Text = "Complex number as input";
            }
        }

        private void PowerButtonClick()
        {
            double value = numberButton.GetCurrentValue();
            double result = value * value;
            pastResultsTextBlock.Text = $"{value}²=";
            numberButton.SetDisplayValue(result);
            numberButton.StartNewCalculation();
        }

        private void ReciprocalButtonClick()
        {

            if (divideByZeroError)
            {
                CButtonClick();
                divideByZeroError = false;
                return;
            }

            double value = numberButton.GetCurrentValue();
            if(value!=0)
            {
                double result = 1 / value;
                pastResultsTextBlock.Text = $"1/{value}=";
                numberButton.SetDisplayValue(result);
                numberButton.StartNewCalculation();
            }
        }

        private void CeButtonClick()
        {
            if (divideByZeroError)
            {
                divideByZeroError = false;
            }
            numberButton.ResetDisplay();
        }
        private void CButtonClick()
        {
            if (divideByZeroError)
            {
                divideByZeroError = false;
            }
            numberButton.ResetDisplay();
            pastResultsTextBlock.Text = "0";
            currentOperation = "";
            firstNumber = 0;
        }

        private void DeleteButtonClick()
        {
            if (divideByZeroError)
            {
                divideByZeroError = false;
            }
            numberButton.DeleteLastChaaracterFromList();
        }
        


        public void SetupMemoryButton(bool programer, ComboBox memoryComboBox, params Button[] buttons)

        {
            if(programer==false)
            {
                buttons[0].Click += (sender, e) => MemoryClear();
                buttons[1].Click += (sender, e) => MemoryRecal();
                buttons[2].Click += (sender, e) => MemoryAdd();
                buttons[3].Click += (sender, e) => MemorySubstract();
                buttons[4].Click += (sender, e) => MemoryStore();
            }
            else
            {
                buttons[0].Click += (sender, e) => MemoryStore();
            }



                this.memoryComboBox = memoryComboBox;
            UpdateMemoryDisplay();
        } 

        private void MemoryClear()
        {
            memoryValue.Clear();
            UpdateMemoryDisplay();
        }
        private void MemoryRecal()
        {
            if (memoryComboBox.SelectedItem != null)
            {
                double selectedValue = (double)memoryComboBox.SelectedItem;
                numberButton.SetDisplayValue(selectedValue);
                numberButton.StartNewCalculation();
            }
        }
        private void MemoryAdd()
        {
            if (memoryComboBox.SelectedIndex >= 0)
            {
                double currentValue = numberButton.GetCurrentValue();
                int selectedIndex = memoryComboBox.SelectedIndex;

                memoryValue[selectedIndex] += currentValue;
                UpdateMemoryDisplay();

                
                memoryComboBox.SelectedIndex = selectedIndex;
            }
        }
        private void MemorySubstract()
        {
            if (memoryComboBox.SelectedIndex >= 0)
            {
                double currentValue = numberButton.GetCurrentValue();
                int selectedIndex = memoryComboBox.SelectedIndex;

                memoryValue[selectedIndex] -= currentValue;
                UpdateMemoryDisplay();

                
                memoryComboBox.SelectedIndex = selectedIndex;
            }
        }
        private void MemoryStore()
        {
            double value = numberButton.GetCurrentValue();
            memoryValue.Add(value);
            UpdateMemoryDisplay();
        }

        private void UpdateMemoryDisplay()
        {
            memoryComboBox.Items.Clear();
            foreach(double value in memoryValue)
            {
                memoryComboBox.Items.Add(value);
            }

            if(memoryValue.Count>0)
            {
                memoryComboBox.SelectedIndex = 0;
            }
        }

    }
}
