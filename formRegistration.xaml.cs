using System.Windows;
using Npgsql;

namespace SportsTrainingApp
{

    public partial class formRegistration : Window
    {
        public formRegistration()
        {
            InitializeComponent();
        }

        private void Button_Click_ShowPass(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ваш пароль: {passwordBox.Password}");
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "" || passwordBox.Password == "" || emailBox.Text == "")
            {
                MessageBox.Show("Введены не все данные.");
                return;
            }
            CreateUser(loginBox.Text, passwordBox.Password, emailBox.Text);
            this.Close();
        }

        private static void CreateUser(string username, string password, string email)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("INSERT INTO dbo.users (username, email, password_hash) " +
                    "VALUES (@username, @email, dbo.crypt(@password, dbo.gen_salt('bf')))", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Пользователь создан!");
        }
    }
}
