using System.Windows;
using System.Windows.Controls;
using Npgsql;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsTrainingApp.Activity
{
    /// <summary>
    /// Interaction logic for formAddActivity.xaml
    /// </summary>
    public partial class formAddActivity : Window
    {
        int user_id = -1;
        public formAddActivity(int _user_id)
        {
            InitializeComponent();
            user_id = _user_id;
            FillCbSports(cbSports);
            dtPicker.SelectedDate = DateTime.Now;
        }

        public class SportType
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public void FillCbSports(ComboBox cb)
        {
            var items = GetData();
            cbSports.ItemsSource = items;
            cbSports.DisplayMemberPath = "name";
            cbSports.SelectedValuePath = "id";
            cbSports.SelectedIndex = 0;
        }

        private List<SportType> GetData()
        {
            var items = new List<SportType>();
            string query = "SELECT id, name FROM dbo.sports";

            using (var connection = new NpgsqlConnection(formMain.connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new SportType
                        {
                            id = reader.GetInt32(0), 
                            name = reader.GetString(1)
                        });
                    }
                }
            }
            return items;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddActivity();
        }

        private void AddActivity()
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("INSERT INTO dbo.workouts(user_id, sport_id, date, duration, intensity, notes) " +
                    "VALUES(@user_id, @sport_id, @date, @duration, @intensity, @note); ", conn))
                {
                    cmd.Parameters.AddWithValue("user_id", user_id);
                    cmd.Parameters.AddWithValue("date", dtPicker.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("sport_id", cbSports.SelectedValue);
                    cmd.Parameters.AddWithValue("duration", Convert.ToInt32(tbDuration.Text));
                    cmd.Parameters.AddWithValue("intensity", tbIntensivity.Text);
                    cmd.Parameters.AddWithValue("note", tbNote.Text);
                    cmd.ExecuteNonQuery();
                }
                this.Close();
            }
        }
    }
}

