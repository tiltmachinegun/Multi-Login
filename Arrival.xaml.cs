using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Multi_Login
{
    public partial class Arrival : Window
    {
        int chosenWarehouseId = 0;
        SqlConnection sqlConnection;
        SqlCommand command = null;
        SqlDataReader sqlReader = null;

        public Arrival()
        {
            InitializeComponent();
        }

        private async void onload(object sender, EventArgs e)
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
            if (!string.IsNullOrEmpty(txtname.Text) && !string.IsNullOrEmpty(txtcount.Text) &&
                !string.IsNullOrEmpty(txtcostbuy.Text) && !string.IsNullOrEmpty(txtcostsell.Text) && chosenWarehouseId != 0)
            {
                command = new SqlCommand("INSERT INTO [Products] (WarehouseID, Name, Count, CostBuy, CostSell)VALUES(@WarehouseID, @Name, @Count, @CostBuy,@CostSell) ", sqlConnection);

                command.Parameters.AddWithValue("WarehouseID", chosenWarehouseId);
                command.Parameters.AddWithValue("Name", txtname.Text);
                command.Parameters.AddWithValue("Count", txtcount.Text);
                command.Parameters.AddWithValue("CostBuy", txtcostbuy.Text);
                command.Parameters.AddWithValue("CostSell", txtcostsell.Text);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Данные успешно обновлены!");
                    txtname.Text = "";
                    txtcount.Text = "";
                    txtcostbuy.Text = "";
                    txtcostsell.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите корректные значения");
                    txtname.Text = "";
                    txtcount.Text = "";
                    txtcostbuy.Text = "";
                    txtcostsell.Text = "";
                }


            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }
        private void txtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void comboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name", sqlConnection);

            command.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());
            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {

                chosenWarehouseId = Convert.ToInt32(sqlReader["Id"]);
            }
            if (sqlReader != null)
                sqlReader.Close();
        }
    }
}