using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Calculator
{
    class NumberButtons
    {
        private readonly TextBlock displayTextBlock;
        private readonly TextBlock pastResultTextBlock;
        private bool isNewCalulation = true;
        private readonly bool isProgrammerMode;

        private List<string> displayCharacter = new List<string>();

        private int currentBase = 10;
        public Action UpdateProgrammerConversions { get; set; }


        public NumberButtons(TextBlock displayTextBlock, TextBlock pastResultTextBlock,bool isProgrammerMode)
        {
            this.displayTextBlock = displayTextBlock;
            this.pastResultTextBlock = pastResultTextBlock;
            this.isProgrammerMode = isProgrammerMode;

            displayCharacter.Add("0");
            UpdateDisplay();

        }


        public void SetCurrentBase(int numberBase)
        {
            currentBase = numberBase;
        }
        public void SetupNumberButtons(params Button[] buttons)
        {
            buttons[0].Click += (sender, e) => NumberButtonClick("0");
            buttons[1].Click += (sender, e) => NumberButtonClick("1");
            buttons[2].Click += (sender, e) => NumberButtonClick("2");
            buttons[3].Click += (sender, e) => NumberButtonClick("3");
            buttons[4].Click += (sender, e) => NumberButtonClick("4");
            buttons[5].Click += (sender, e) => NumberButtonClick("5");
            buttons[6].Click += (sender, e) => NumberButtonClick("6");
            buttons[7].Click += (sender, e) => NumberButtonClick("7");
            buttons[8].Click += (sender, e) => NumberButtonClick("8");
            buttons[9].Click += (sender, e) => NumberButtonClick("9");
            


            if(isProgrammerMode)
            {
                buttons[10].Click += (sender, e) => NumberButtonClick("A");
                buttons[11].Click += (sender, e) => NumberButtonClick("B");
                buttons[12].Click += (sender, e) => NumberButtonClick("C");
                buttons[13].Click += (sender, e) => NumberButtonClick("D");
                buttons[14].Click += (sender, e) => NumberButtonClick("E");
                buttons[15].Click += (sender, e) => NumberButtonClick("F");
                buttons[16].Click += (sender, e) => PointButtonClick();
            }
            else
            {
                buttons[10].Click += (sender, e) => PointButtonClick();
            }
        }

        private void NumberButtonClick(string number)
        {   
            if(displayCharacter.Count==1 && displayCharacter[0]=="0"|| isNewCalulation)
            {
                displayCharacter.Clear();
                displayCharacter.Add(number);
                isNewCalulation = false;
            }
            else
            {
                displayCharacter.Add(number);
            }
            UpdateDisplay();     
         }

        public int GetCurrentBase()
        {
            return this.currentBase;
        }
        private void UpdateDisplay()
        {
            string numberString = string.Join("", displayCharacter);
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            

            if(double.TryParse(numberString,NumberStyles.Any,CultureInfo.InvariantCulture,out double number))
            {
                if(numberString.Contains("."))
                {
                    string[] parts = numberString.Split(".");

                    if (double.TryParse(parts[0],out double wholePart))
                    {
                        string formatedWholePart = AppSettings.EnableDigitGrouping
                                                    ? wholePart.ToString("N0", CultureInfo.CurrentCulture)
                                                    : wholePart.ToString("F0", CultureInfo.CurrentCulture);


                        displayTextBlock.Text = formatedWholePart + decimalSeparator + parts[1];
                    }
                    else
                    {
                        displayTextBlock.Text = numberString;
                    }
                }
                else
                {
                    displayTextBlock.Text = AppSettings.EnableDigitGrouping
                                            ? number.ToString("N0", CultureInfo.CurrentCulture)
                                            : number.ToString("F0", CultureInfo.CurrentCulture);

                }
            }
            else
            {
                displayTextBlock.Text=numberString;
            }


            if (isProgrammerMode && UpdateProgrammerConversions != null)
            {
                UpdateProgrammerConversions.Invoke();
            }

        }

        private void PointButtonClick()
        {
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (!string.Join("",displayCharacter).Contains(decimalSeparator))
            {
                displayCharacter.Add(decimalSeparator);
                UpdateDisplay();
                
            }
        }

        public void ResetDisplay()
        {
            displayCharacter.Clear();
            displayCharacter.Add("0");
            UpdateDisplay();
        }
        public void StartNewCalculation()
        {
            isNewCalulation = true;
        }

        public void DeleteLastChaaracterFromList()
        {
            if(displayCharacter.Count>1)
            {
                displayCharacter.RemoveAt(displayCharacter.Count - 1);
            }
            else
            {
                displayCharacter.Clear();
                displayCharacter.Add("0");
            }
            UpdateDisplay();
        }

        public double GetCurrentValue()
        {
            string rawInput = string.Join("", displayCharacter);

            try
            {
                if(isProgrammerMode)
                {
                    if(currentBase==10)
                    {
                        return double.Parse(rawInput, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        int value = Convert.ToInt32(rawInput, currentBase);
                        return (double)value;
                    }
                }
                else
                {
                    return double.Parse(rawInput, CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                return 0;
            }
        }
        public void SetDisplayValue(double value)
        {
            displayCharacter.Clear();

            if (isProgrammerMode)
            {
                long intValue = (long)value;
                string resultInBase = Convert.ToString(intValue, currentBase).ToUpper();

                foreach (char c in resultInBase)
                {
                    displayCharacter.Add(c.ToString());
                }
            }
            else
            {
                string valueSTR = value.ToString(CultureInfo.InvariantCulture);

                if (Math.Abs(value) < 1e-10)
                {
                    valueSTR = "0";
                }

                if (valueSTR.Contains("E"))
                {
                    foreach (char c in valueSTR)
                    {
                        displayCharacter.Add(c.ToString());
                    }
                }
                else
                {
                    if (valueSTR.Contains("."))
                    {
                        valueSTR = valueSTR.TrimEnd('0');
                        valueSTR = valueSTR.TrimEnd('.');
                    }

                    foreach (char c in valueSTR)
                    {
                        displayCharacter.Add(c.ToString());
                    }
                }
            }
            UpdateDisplay();
        }

    }
}
