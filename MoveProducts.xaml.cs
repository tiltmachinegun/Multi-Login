using System;
using System.Data.SqlClient;
using System.Windows;

namespace Multi_Login
{
    public partial class MoveProducts : Window
    {
        int chosenWarehouseId1 = 0;
        int chosenWarehouseId2 = 0;
        SqlConnection sqlConnection;
        SqlCommand command = null;
        SqlDataReader sqlReader = null;

        public MoveProducts()
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
                comboBox2.Items.Add(Convert.ToString(sqlReader["Name"]));
            }
            if (sqlReader != null)
                sqlReader.Close();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!(chosenWarehouseId1 == 0) && !(chosenWarehouseId2 == 0) && !string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    command = new SqlCommand("SELECT * FROM [Products] WHERE [WarehouseID] = @WarehouseID AND [Id] =@Id", sqlConnection);
                    command.Parameters.AddWithValue("@WarehouseID", chosenWarehouseId1);
                    command.Parameters.AddWithValue("@Id", textBox1.Text);
                    sqlReader = await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        Product.name = Convert.ToString(sqlReader["Name"]);
                        Product.count = Convert.ToString(sqlReader["Count"]);
                        Product.costBuy = Convert.ToString(sqlReader["CostBuy"]);
                        Product.costSell = Convert.ToString(sqlReader["CostSell"]);
                    }
                    if (sqlReader != null)
                        sqlReader.Close();

                    command = new SqlCommand("DELETE FROM [Products] WHERE [Id] = @Id AND [WarehouseID] = @WarehouseId", sqlConnection);

                    command.Parameters.AddWithValue("Id", textBox1.Text);
                    command.Parameters.AddWithValue("WarehouseID", chosenWarehouseId1);
                    await command.ExecuteNonQueryAsync();

                    command = new SqlCommand("INSERT INTO [Products] (WarehouseID, Name, Count, CostBuy, CostSell)VALUES(@WarehouseID, @Name, @Count, @CostBuy,@CostSell) ", sqlConnection);

                    command.Parameters.AddWithValue("WarehouseID", chosenWarehouseId2);
                    command.Parameters.AddWithValue("Name", Product.name);
                    command.Parameters.AddWithValue("Count", Product.count);
                    command.Parameters.AddWithValue("CostBuy", Product.costBuy);
                    command.Parameters.AddWithValue("CostSell", Product.costSell);
                    await command.ExecuteNonQueryAsync();
                }
                catch
                {
                    MessageBox.Show("Введите коректные значения");
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name", sqlConnection);

            command.Parameters.AddWithValue("@Name", comboBox1.SelectedItem.ToString());
            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {

                chosenWarehouseId1 = Convert.ToInt32(sqlReader["Id"]);
            }
            if (sqlReader != null)
                sqlReader.Close();
        }

        private async void comboBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT [Id] FROM [WareHouses] WHERE [Name] = @Name", sqlConnection);

            command.Parameters.AddWithValue("@Name", comboBox2.SelectedItem.ToString());
            sqlReader = await command.ExecuteReaderAsync();
            while (await sqlReader.ReadAsync())
            {

                chosenWarehouseId2 = Convert.ToInt32(sqlReader["Id"]);
            }
            if (sqlReader != null)
                sqlReader.Close();
        }
    }
}