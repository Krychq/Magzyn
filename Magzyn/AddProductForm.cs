using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Magzyn.Form1;

namespace Magzyn
{
    public partial class AddProductForm : Form
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        /* Add product */
        private void button1_Click(object sender, EventArgs e)
        {
            Double price;
            UInt32 quantity;

            if (String.IsNullOrEmpty(product_name.Text))
            {
                MessageBox.Show("Błędna nazwa produktu");
                return;
            }

            if (Product.isThere(product_name.Text))
            {
                MessageBox.Show("Produkt o takiej nazwie juz istnieje!");
                return;
            }

            if (String.IsNullOrEmpty(product_desc.Text))
            {
                MessageBox.Show("Błędna nazwa opisu.");
                return;
            }

            if(!Double.TryParse(product_price.Text, out price))
            {
                MessageBox.Show("Błędna cena.");
                return;
            }
            
            if(!UInt32.TryParse(product_quantity.Text, out quantity))
            {
                MessageBox.Show("Błędna ilość.");
                return;
            }

            Form1.products.Add(new Form1.Product(product_name.Text, product_desc.Text, price, quantity));
            this.DialogResult = DialogResult.OK;
        }
    }
}
