using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Multi_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection;
        private bool isConnected = false;
        private async void Autorization_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
        }
        

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand("SELECT * FROM [Users] WHERE [Login] = @Login AND [Password] = @Password", sqlConnection);

                command.Parameters.AddWithValue("@Login", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        MainMenu mainMenu = new MainMenu();
                        mainMenu.label.Content ="Учётная запись:"+ "\r\n" + Convert.ToString(sqlReader["UserName"] + "      ");
                        User.userId = Convert.ToInt32(sqlReader["Id"]);
                        mainMenu.Show();
                        Hide();
                        isConnected = true;
                    }
                    if (!isConnected)
                        MessageBox.Show("Такого пользователя не существует или пароль неверен.");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Такого пользователя не существует или пароль неверен.", ex.Source.ToString());

                

            }
                
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != System.Data.ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
            base.OnClosed(e);
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        public void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
           
        }
 
        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration f = new Registration();
            f.Show();
            Hide();
        }
    }
}
