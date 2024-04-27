using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magzyn
{
    public partial class Form1 : Form
    {
        public class Product
        {
            public String name;
            public String description;
            public Double price;
            public UInt32 quantity;

            public Product(String _name, String _description, Double _price, UInt32 _quantity)
            {
                name = _name;
                description = _description;
                price = _price;
                quantity = _quantity;
            }

            public static Boolean isThere(String name)
            {
                foreach(var product in products)
                    if(product.name == name)
                        return true;

                return false;
            }

        }
        public static String HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public class Account
        {
            private String login;
            private String password;

            public Account(String _login, String _password)
            {
                login = _login;
                password = _password;
            }

            public Boolean MatchPassword(String inputPassword)
            {
                String hashedInputPassword = HashPassword(inputPassword);

                return hashedInputPassword.Equals(this.password);
            }
        }

        public static String tempString;

        public Account activeAccount;

        public static List<Product> products;

        public static String dataFileName = "data.txt";

        public void ShowWarningMessageBox(String content, String windowLabel = "Ostrzeżenie")
        {
            MessageBox.Show(content, windowLabel, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public Form1()
        {
            InitializeComponent();
            products = new List<Product>();

            if (activeAccount == null)
            {

            }

            LoadData(dataFileName);
        }

        public void RerenderGrid()
        {
            SeralizeData();

            mainGrid.Columns.Clear();

            mainGrid.AllowUserToAddRows = false;
            mainGrid.ColumnCount = 5;

            mainGrid.TopLeftHeaderCell.Value = "ID";

            mainGrid.Columns[0].Name = "Nazwa";
            mainGrid.Columns[0].Width = 125;
            mainGrid.Columns[1].Name = "Opis";
            mainGrid.Columns[1].Width = 200;
            mainGrid.Columns[2].Name = "Cena";
            mainGrid.Columns[3].Name = "Ilość";
            mainGrid.Columns[4].Width = 50;

            /*            mainGrid.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Regular);
                        mainGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 13F, FontStyle.Bold);
                        mainGrid.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);

                        mainGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        mainGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;*/

            mainGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            mainGrid.Columns[4].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 12F, FontStyle.Bold);
            mainGrid.Columns[4].DefaultCellStyle.ForeColor = Color.DarkRed;

            Int32 currentId = 1;

            foreach (var product in products)
            {
                string[] row = { product.name, product.description, $"{product.price:N2} PLN", product.quantity.ToString(), "✖" };
                mainGrid.Rows.Add(row);
                mainGrid.Rows[currentId - 1].HeaderCell.Value = currentId.ToString();
                currentId++;
            }
        }

        public void SeralizeData()
        {
            TextWriter txt = new StreamWriter(dataFileName);

            foreach (Product product in products)
            {
                txt.WriteLine($"{product.name};{product.description};{product.price};{product.quantity}");
            }

            txt.Close();
        }
        
        public void LoadData(String fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
                string buff = String.Empty;

                while ((buff = sr.ReadLine()) != null)
                {
                    String[] data = buff.Split(';');
                    if (data.Length != 4)
                    {
                        continue;
                    }

                    Double price;
                    UInt32 quantity;

                    if (String.IsNullOrEmpty(data[0]) || Product.isThere(data[0]))
                    {
                        continue;
                    }

                    if (String.IsNullOrEmpty(data[1]))
                    {
                        continue;
                    }

                    if (!Double.TryParse(data[2], out price))
                    {
                        continue;
                    }

                    if (!UInt32.TryParse(data[3], out quantity))
                    {
                        continue;
                    }

                    products.Add(new Product(data[0], data[1], price, quantity));
                }

                sr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd wczytywania danych!");
                return;
            }

            RerenderGrid();
        }

        // Add product
        private void button1_Click(object sender, EventArgs e)
        {
            AddProductForm addProductModal = new AddProductForm();
            DialogResult result = addProductModal.ShowDialog();
            if(result == DialogResult.OK)
            {
                RerenderGrid();
            }
        }

        private void mainGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if(e.ColumnIndex == 4)
            {
                var confirmDelete = MessageBox.Show("Czy na pewno chcesz usunąć produckt?",
                                     "Potwierdź usunięcie",
                                     MessageBoxButtons.YesNo);
                if (confirmDelete == DialogResult.Yes)
                {
                    products.RemoveAt(e.RowIndex);

                    RerenderGrid();
                }
            }
            
            // Change name
            if(e.ColumnIndex == 0)
            {
                ChangeDetail modal = new ChangeDetail("Zmień nazwę produktu", "Nowa nazwa");
                DialogResult result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if(String.IsNullOrEmpty(tempString))
                    {
                        ShowWarningMessageBox("Nie podałeś żadnej nazwy!");
                        return;
                    }

                    if (Product.isThere(tempString))
                    {
                        ShowWarningMessageBox("Produkt o padanej nazwie już istnieje!");
                        return;
                    }
                    products.ElementAt(e.RowIndex).name = tempString;

                    RerenderGrid();
                }
            }
            
            // Change desc
            if(e.ColumnIndex == 1)
            {
                ChangeDetail modal = new ChangeDetail("Zmień opis produktu", "Nowy opis", true);
                DialogResult result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (String.IsNullOrEmpty(tempString))
                    {
                        ShowWarningMessageBox("Nie podałeś opisu!");
                        return;
                    }
                    products.ElementAt(e.RowIndex).description = tempString;

                    RerenderGrid();
                }
            }

            // Change price
            if (e.ColumnIndex == 2)
            {
                ChangeDetail modal = new ChangeDetail("Zmień cenę produktu", "Nowa cena");
                DialogResult result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Double price;
                    if (!Double.TryParse(tempString, out price))
                    {
                        ShowWarningMessageBox("Niepoprawna cena!");
                        return;
                    }

                    products.ElementAt(e.RowIndex).price = price;

                    RerenderGrid();
                }
            }
            
            // Change quantity
            if (e.ColumnIndex == 3)
            {
                ChangeDetail modal = new ChangeDetail("Zmień ilość dostępnych produktów", "Nowa ilość");
                DialogResult result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    UInt32 quantity;
                    if (!UInt32.TryParse(tempString, out quantity))
                    {
                        ShowWarningMessageBox("Niepoprawna ilość!");
                        return;
                    }

                    products.ElementAt(e.RowIndex).quantity = quantity;

                    RerenderGrid();
                }
            }
        }

        // Load data
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Nie udało się wczytać pliku!");
                return;
            }

            LoadData(fileDialog.FileName);
        }
    }
}
