
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Multi_Login
{
    public partial class Registration : Window
    {
        SqlConnection sqlConnection;
        SqlCommand command = null;

        public Registration()
        {
            InitializeComponent();
        }

        private async void onload(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Password)
                && !string.IsNullOrEmpty(textBox3.Password) && textBox3.Password == textBox2.Password)
            {
                command = new SqlCommand("INSERT INTO [Users] (Login, Password, UserName)VALUES(@Login, @Password,@UserName) ", sqlConnection);

                command.Parameters.AddWithValue("Login", textBox1.Text);
                command.Parameters.AddWithValue("Password", textBox2.Password);
                command.Parameters.AddWithValue("UserName", textBox4.Text);
                await command.ExecuteNonQueryAsync();
                Autorization autorization = new Autorization();
                autorization.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please enter all data and make sure passwords match.");
            }
        }
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
}