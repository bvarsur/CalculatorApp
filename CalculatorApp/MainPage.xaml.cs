namespace CalculatorApp
{
    public partial class MainPage : ContentPage
    {

        private string currentInput = "";
        private string lastOperator = "";
        private double total = 0;
        private bool shouldClear = false;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnDigitClicked(object sender, EventArgs e)
        {
            var digit = (sender as Button)?.Text;

            if (shouldClear)
            {
                currentInput = "";
                shouldClear = false;
            }

            if (digit == "." && currentInput.Contains("."))
                return;

            currentInput += digit;

            // Show total + operator + currentInput if operator is set, else just currentInput
            if (!string.IsNullOrEmpty(lastOperator))
            {
                ResultLabel.Text = $"{total} {lastOperator} {currentInput}";
            }
            else
            {
                ResultLabel.Text = currentInput;
            }
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            var op = (sender as Button)?.Text;

            if (!string.IsNullOrEmpty(currentInput))
            {
                double inputNum = double.Parse(currentInput);
                if (string.IsNullOrEmpty(lastOperator))
                {
                    total = inputNum;
                }
                else
                {
                    total = Calculate(total, inputNum, lastOperator);
                }

                lastOperator = op;
                currentInput = "";

                // Show total + operator
                ResultLabel.Text = $"{total} {lastOperator}";
            }
            else if (!string.IsNullOrEmpty(ResultLabel.Text))
            {
                lastOperator = op;
                // Show total + operator
                ResultLabel.Text = $"{total} {lastOperator}";
            }
        }

        void OnEqualClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(lastOperator))
            {
                double inputNum = double.Parse(currentInput);
                total = Calculate(total, inputNum, lastOperator);

                ResultLabel.Text = total.ToString();
                currentInput = "";
                lastOperator = "";
                shouldClear = true;
            }
        }

        double Calculate(double a, double b, string op)
        {
            return op switch
            {
                "+" => a + b,
                "−" => a - b,
                "×" => a * b,
                "÷" => b != 0 ? a / b : 0,
                "%" => a % b,
                _ => b
            };
        }

        void OnClear(object sender, EventArgs e)
        {
            total = 0;
            currentInput = "";
            lastOperator = "";
            ResultLabel.Text = "0";
        }

        void OnBackspace(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
                // Show total + operator + currentInput if operator is set, else just currentInput
                if (!string.IsNullOrEmpty(lastOperator))
                {
                    ResultLabel.Text = $"{total} {lastOperator} {currentInput}";
                }
                else
                {
                    ResultLabel.Text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
                }
            }
        }
    }
}