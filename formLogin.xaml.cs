using System.Windows;
using System;
using Npgsql;

namespace SportsTrainingApp
{

    public partial class formLogin : Window
    {
        formMain formMain;
        public formLogin(formMain _formMain)
        {                                                                                                                               
            InitializeComponent();
            formMain = _formMain;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int user_id = CheckUser(loginBox.Text, passwordBox.Password);
            if (user_id != -1)
            {
                new formActivity(user_id).Show();
                this.Close();
                formMain.Close();               
            }
            else
                MessageBox.Show("Неверный логин/пароль!");
        }

        public static int CheckUser(string username, string password)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT id FROM dbo.users WHERE username = @username " +
                    "AND password_hash = dbo.crypt(@password, password_hash)", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                    return -1;
                }
            }
        }
    }
}
