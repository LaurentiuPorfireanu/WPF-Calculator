using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{

    public partial class Standard : Window
    {
        private  ButtonFunctions calculatorFunctionality;
        private NumberButtons numberButtonsHandler;

        public Standard()
        {
            InitializeComponent();

            NumberButtons numberButtonsHandler = new NumberButtons(this.CurrentResult, this.PastResults,false);
            ButtonFunctions calculatorFunctionality = new ButtonFunctions(this.CurrentResult, this.PastResults, numberButtonsHandler,this.ShowStackFromMemory);



            numberButtonsHandler.SetupNumberButtons(
                Button0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8,
                Button9, Point);
            calculatorFunctionality.SetupFunctionality(false,
                Plus,Minus,ProdButton,Divide,Equal,Negat,Sqrt,Pow2,Power_1,modulo,
                DeleteTheLastCharacter,EmptyTheCurrentOperation,DeleteCharacter
                );

            calculatorFunctionality.SetupMemoryButton(
                false, ShowStackFromMemory,MemoryClear, ShowValueFromMEmory,AddInMemory,DeleteInMemory,
                SaveInMemoryCurrentDisplayedValue
                );

            this.KeyDown += MainWindow_KeyDown;
        }


        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Button targetButton = null;
            bool shiftPressed = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
            
            switch (e.Key)
            {
                case System.Windows.Input.Key.D0:
                    targetButton = Button0;
                    break;
                case System.Windows.Input.Key.D1:
                    targetButton = Button1;
                    break;
                case System.Windows.Input.Key.D2:
                    targetButton = Button2;
                    break;
                case System.Windows.Input.Key.D3:
                    targetButton = Button3;
                    break;
                case System.Windows.Input.Key.D4:
                    targetButton = Button4;
                    break;
                case System.Windows.Input.Key.D6:
                    targetButton = Button6;
                    break;
                case System.Windows.Input.Key.D7:
                    targetButton = Button7;
                    break;
                case System.Windows.Input.Key.D9:
                    targetButton = Button9;
                    break;

                case System.Windows.Input.Key.OemPlus:
                    if (shiftPressed)
                        targetButton = Plus;
                    break;
                case System.Windows.Input.Key.OemMinus:
                    if (shiftPressed)
                        targetButton = Minus;
                    break;
                case System.Windows.Input.Key.D8:
                    if (shiftPressed)
                        targetButton = ProdButton;
                    else
                        targetButton = Button8;
                    break;
                case System.Windows.Input.Key.D5:          
                    if (shiftPressed)
                        targetButton = modulo;       
                    else
                        targetButton = Button5;             
                    break;
                case System.Windows.Input.Key.OemQuestion:
                    if (!shiftPressed)
                        targetButton = Divide;
                    break;
                case System.Windows.Input.Key.Enter:
                    targetButton = Equal;
                    break;
                case System.Windows.Input.Key.OemPeriod:
                    targetButton = Point;
                    break;
                case System.Windows.Input.Key.Back:
                    targetButton = DeleteCharacter;
                    break;
                case System.Windows.Input.Key.Escape:
                    targetButton = EmptyTheCurrentOperation;
                    break;
            }


            if (targetButton != null)
            { 
                targetButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));

                e.Handled = true;
            
            }
        }
       
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicatie realizata de: Porfireanu Constantin Laurentiu \nGrupa: 10LF333",
                            "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentResult.Text != null)
            {
                Clipboard.SetText(CurrentResult.Text);
                CurrentResult.Text = string.Empty;
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentResult.Text != null)
            {
                Clipboard.SetText(CurrentResult.Text);
            }
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            string clipboardText = Clipboard.GetText();
            if (!string.IsNullOrEmpty(clipboardText))
            {
                CurrentResult.Text = clipboardText;
            }
        }



    }
}