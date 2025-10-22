namespace NumericTypesSuggester
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22F);
            label1.Location = new Point(118, 47);
            label1.Name = "label1";
            label1.Size = new Size(153, 41);
            label1.TabIndex = 0;
            label1.Text = "Min value:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 22F);
            label2.Location = new Point(113, 111);
            label2.Name = "label2";
            label2.Size = new Size(158, 41);
            label2.TabIndex = 1;
            label2.Text = "Max value:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(277, 292);
            label5.Name = "label5";
            label5.Size = new Size(246, 40);
            label5.TabIndex = 5;
            label5.Text = "not enough data";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(37, 291);
            label6.Name = "label6";
            label6.Size = new Size(238, 40);
            label6.TabIndex = 4;
            label6.Text = "Suggested type:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 22F);
            textBox1.Location = new Point(277, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(659, 47);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += TextBox_TextChanged;
            textBox1.KeyPress += TextBox_KeyPress;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 22F);
            textBox2.Location = new Point(277, 108);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(659, 47);
            textBox2.TabIndex = 9;
            textBox2.TextChanged += TextBox_TextChanged;
            textBox2.KeyPress += TextBox_KeyPress;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.CheckAlign = ContentAlignment.MiddleRight;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Font = new Font("Segoe UI", 22F);
            checkBox1.Location = new Point(59, 186);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(216, 45);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "Integral only?";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.CheckAlign = ContentAlignment.MiddleRight;
            checkBox2.Font = new Font("Segoe UI", 22F);
            checkBox2.Location = new Point(14, 237);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(261, 45);
            checkBox2.TabIndex = 11;
            checkBox2.Text = "Must be precise?";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.Visible = false;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(948, 353);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "C# Numeric types";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private TextBox textBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}
