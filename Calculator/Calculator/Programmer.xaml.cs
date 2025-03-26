using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Calculator
{
   
    public partial class Programmer : Window
    {
        private ButtonFunctions calculatorFunctionality;
        private NumberButtons numberButtonsHandler;
        int lastBase = Properties.Settings.Default.LastProgrammerBase;
        public Programmer()
        {
            InitializeComponent();

            numberButtonsHandler = new NumberButtons(this.CurrentResult, this.PastResults, true);
            numberButtonsHandler.UpdateProgrammerConversions = UpdateConversions;
            calculatorFunctionality = new ButtonFunctions(this.CurrentResult, this.PastResults, numberButtonsHandler,this.ShowStackFromMemory);



            numberButtonsHandler.SetupNumberButtons(
                Button0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8,
                Button9, AButton, BButton, CButton, DButton, EButton, FButton, Point);
            calculatorFunctionality.SetupFunctionality(true,
                Plus, Minus, ProdButton, Divide, Equal, Negat, Modulo,
                DeleteTheLastCharacter, EmptyTheCurrentOperation, DeleteCharacter
                );


            calculatorFunctionality.SetupMemoryButton(
               true, ShowStackFromMemory,SaveInMemoryCurrentDisplayedValue
               );

            



            switch (lastBase)
            {
                case 2:
                    BinRadio.IsChecked = true;
                    break;
                case 8:
                    OctRadio.IsChecked = true;
                    break;
                case 10:
                    DecRadio.IsChecked = true;
                    break;
                case 16:
                    HexRadio.IsChecked = true;
                    break;
                default:
                    DecRadio.IsChecked = true;
                    break;
            }

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Button targetButton = null;
            bool shiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            int currentBase = numberButtonsHandler.GetCurrentBase();
            switch (e.Key)
            {
                case Key.D0:
                    if (currentBase >= 2) targetButton = Button0;
                    break;
                case Key.D1:
                    if (currentBase >= 2) targetButton = Button1;
                    break;
                case Key.D2:
                    if (currentBase >= 3) targetButton = Button2;
                    break;
                case Key.D3:
                    if (currentBase >= 4) targetButton = Button3;
                    break;
                case Key.D4:
                    if (currentBase >= 5) targetButton = Button4;
                    break;
                case Key.D5:
                    if (shiftPressed)
                    {
                        targetButton = Modulo; 
                    }
                    else if (currentBase >= 6)
                    {
                        targetButton = Button5;
                    }
                    break;
                case Key.D6:
                    if (currentBase >= 7) targetButton = Button6;
                    break;
                case Key.D7:
                    if (currentBase >= 8) targetButton = Button7;
                    break;
                case Key.D8:
                    if (shiftPressed)
                    {
                        targetButton = ProdButton; 
                    }
                    else if (currentBase >= 9)
                    {
                        targetButton = Button8;
                    }
                    break;
                case Key.D9:
                    if (currentBase >= 10) targetButton = Button9;
                    break;

                case Key.A:
                    if (currentBase >= 16) targetButton = AButton;
                    break;
                case Key.B:
                    if (currentBase >= 16) targetButton = BButton;
                    break;
                case Key.C:
                    if (currentBase >= 16) targetButton = CButton;
                    break;
                case Key.D:
                    if (currentBase >= 16) targetButton = DButton;
                    break;
                case Key.E:
                    if (currentBase >= 16) targetButton = EButton;
                    break;
                case Key.F:
                    if (currentBase >= 16) targetButton = FButton;
                    break;

                case Key.OemPlus:
                    if (shiftPressed)
                        targetButton = Plus;
                    break;
                case Key.OemMinus:
                    targetButton = Minus;
                    break;
                case Key.OemQuestion:
                    targetButton = Divide;
                    break;
                case Key.Enter:
                    targetButton = Equal;
                    break;
                case Key.OemPeriod:
                    if (lastBase == 10)
                        targetButton = Point;
                    break;
                case Key.Back:
                    targetButton = DeleteCharacter;
                    break;
                case Key.Escape:
                    targetButton = DeleteTheLastCharacter;
                    break;
            }

            if (targetButton != null)
            {
                targetButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                e.Handled = true;
            }
        }


        private void UpdateConversions()
        {
            try
            {
                double value = numberButtonsHandler.GetCurrentValue();
                long intValue = (long)value;

                BinRes.Text = $"{Convert.ToString(intValue, 2)}";
                OctRes.Text = $"{Convert.ToString(intValue, 8)}";
                DecRes.Text = $"{intValue}";
                HexRes.Text = $"{Convert.ToString(intValue, 16).ToUpper()}";
            }
            catch
            {
                BinRes.Text = "";
                OctRes.Text = "";
                DecRes.Text = "";
                HexRes.Text = "";
            }
        }



        private void BaseMode_Checked(object sender, RoutedEventArgs e)
        {
            if (BinRadio.IsChecked == true)
            {
                SetBaseAllowedDigits(2);
                Properties.Settings.Default.LastProgrammerBase = 2;
            }  
            else if (OctRadio.IsChecked == true)
                {
                SetBaseAllowedDigits(8);
                Properties.Settings.Default.LastProgrammerBase = 8;
            }
            else if (DecRadio.IsChecked == true)
            {
                SetBaseAllowedDigits(10);
                Properties.Settings.Default.LastProgrammerBase = 10;
            }
            else if (HexRadio.IsChecked == true)
            {
                SetBaseAllowedDigits(16);
                Properties.Settings.Default.LastProgrammerBase = 16;
            }

            Properties.Settings.Default.Save();
        }
            

        private void SetBaseAllowedDigits(int numberBase)
        {
            numberButtonsHandler.SetCurrentBase(numberBase);
            Button0.IsEnabled = numberBase > 0;
            Button1.IsEnabled = numberBase > 1;
            Button2.IsEnabled = numberBase > 2;
            Button3.IsEnabled = numberBase > 3;
            Button4.IsEnabled = numberBase > 4;
            Button5.IsEnabled = numberBase > 5;
            Button6.IsEnabled = numberBase > 6;
            Button7.IsEnabled = numberBase > 7;
            Button8.IsEnabled = numberBase > 8;
            Button9.IsEnabled = numberBase > 9;

            AButton.IsEnabled = numberBase > 10;
            BButton.IsEnabled = numberBase > 11;
            CButton.IsEnabled = numberBase > 12;
            DButton.IsEnabled = numberBase > 13;
            EButton.IsEnabled = numberBase > 14;
            FButton.IsEnabled = numberBase > 15;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicatie realizata de: Porfireanu Constantin Laurentiu \nGrupa: 10LF333",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }


}
