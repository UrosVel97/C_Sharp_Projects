using System.Numerics;

namespace NumericTypesSuggester
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Visible = !checkBox1.Checked;

            RecalculateSuggestedType();
        }


        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            RecalculateSuggestedType();
            SetColorOfMaxValueTextField();


        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RecalculateSuggestedType();

        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!IsValidInput(e.KeyChar, textBox!))
            {
                e.Handled = true;
            }

        }

        private static bool IsValidInput(char keyChar, TextBox textBox)
        {
            return 
                char.IsControl(keyChar) || 
                char.IsDigit(keyChar) ||
                (keyChar == '-'  && textBox.SelectionStart == 0);

        }

        private void SetColorOfMaxValueTextField()
        {
            bool isValid = true;

            if(IsInputComplete())
            {
                var minValue = BigInteger.Parse(textBox1.Text);
                var maxValue = BigInteger.Parse(textBox2.Text);

                if(maxValue < minValue)
                {
                    isValid = false;
                }

            }

            textBox2.BackColor = isValid ? Color.White : Color.IndianRed;
        }

        private bool IsInputComplete()
        {
            return 
                textBox1.Text.Length > 0 &&
                textBox1.Text != "-" &&
                textBox2.Text.Length > 0 &&
                textBox2.Text != "-";

        }

        private void RecalculateSuggestedType()
        {
            if(IsInputComplete())
            {
                var minValue = BigInteger.Parse(textBox1.Text);
                var maxValue = BigInteger.Parse(textBox2.Text);

                if (maxValue >= minValue)
                {
                    label5.Text = NumericTypesSuggester.GetName(minValue, maxValue, checkBox1.Checked, checkBox2.Checked);
                    
                    return;
                }
            }

            label5.Text = "not enough data";

        }


    }
}
