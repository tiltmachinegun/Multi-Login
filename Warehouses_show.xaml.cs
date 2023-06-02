using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Multi_Login
{
    public partial class Warehouses_show : Window
    {
        int chosenWarehouseId = 0;
        SqlConnection sqlConnection;
        SqlCommand command = null;
        SqlDataReader sqlReader = null;
        List<Product> productList = new List<Product>();

        public Warehouses_show()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                command = new SqlCommand("INSERT INTO [WareHouses] (UserId, Name)VALUES(@UserId, @Name) ", sqlConnection);

                command.Parameters.AddWithValue("UserId", User.userId);
                command.Parameters.AddWithValue("Name", textBox1.Text);
                await command.ExecuteNonQueryAsync();
                System.Windows.Forms.MessageBox.Show("Склад создан.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Заполните поле.");
            }
        }

        private async void onload(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT [Name] FROM [WareHouses] WHERE [UserId] = @UserId", sqlConnection);

            command.Parameters.AddWithValue("@UserId", User.userId);

            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {
                comboBox1.Items.Add(Convert.ToString(sqlReader["Name"]));
            }
            if (sqlReader != null)
                sqlReader.Close();

        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (chosenWarehouseId != 0)
            {
                dataGridView1.Items.Clear();
                SqlCommand command = new SqlCommand("SELECT * FROM [Products] WHERE [WarehouseID] = @WarehouseID", sqlConnection);
                command.Parameters.AddWithValue("@WarehouseID", chosenWarehouseId);
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    dataGridView1.Items.Add(new
                    {
                        Id = Convert.ToString(sqlReader["Id"]),
                        Name = Convert.ToString(sqlReader["Name"]),
                        Count = Convert.ToString(sqlReader["Count"]),
                        CostBuy = Convert.ToString(sqlReader["CostBuy"]),
                        CostSell = Convert.ToString(sqlReader["CostSell"])
                    });
                }
                if (sqlReader != null)
                    sqlReader.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите склад.");
            }
        }

        private class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
            public double CostBuy { get; set; }
            public double CostSell { get; set; }
        }

        private async void comboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name AND [UserId] = @UserId", sqlConnection);

            command.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@UserId", User.userId);
            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {

                chosenWarehouseId = Convert.ToInt32(sqlReader["Id"]);
            }
            if (sqlReader != null)
                sqlReader.Close();

            else
            {
                System.Windows.MessageBox.Show("Выберите склад.");
            }
        }
    }

}