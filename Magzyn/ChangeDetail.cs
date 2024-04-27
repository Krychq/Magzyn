using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magzyn
{
    public partial class ChangeDetail : Form
    {
        public ChangeDetail(String formLabel = "Zmień wartość", String inputLabel = "Zmień", Boolean isInputMultiline = false)
        {
            InitializeComponent();
            this.Text = formLabel;
            input_label.Text = inputLabel;
            if(isInputMultiline)
            {
                input.Multiline = true;
                input.Height = 50;
                button1.Location = new System.Drawing.Point(button1.Location.X, button1.Location.Y + 15);
                button2.Location = new System.Drawing.Point(button2.Location.X, button2.Location.Y + 15);
            }
        }

        private void ChangeDetail_Load(object sender, EventArgs e)
        {

        }

        // On change
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(input.Text))
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            Form1.tempString = input.Text;

            this.DialogResult = DialogResult.OK;
        }

        // On cancel
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
