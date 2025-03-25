using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Npgsql;

namespace SportsTrainingApp.Activity
{
    /// <summary>
    /// Interaction logic for formUpdActivity.xaml
    /// </summary>
    public partial class formUpdActivity : Window
    {
        formActivity.Workout item;

        public formUpdActivity(formActivity.Workout _item)
        {
            InitializeComponent();
            item = _item;
            FillFormFields(item);
        }

        private void FillFormFields(formActivity.Workout _item)
        {
            FillCbSports(cbSports);
            for (int i = 0; i < cbSports.Items.Count; i++)
            {
                var sport = cbSports.Items[i] as SportType;
                if (_item.Name == sport?.name.ToString())
                {
                    cbSports.SelectedIndex = i;
                }
            }
            dtPicker.SelectedDate = _item.Date;
            tbDuration.Text = _item.Duration.ToString();
            tbIntensivity.Text = _item.Intensity?.ToString();
            tbNote.Text = _item.Notes?.ToString();
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using(var cmd = new NpgsqlCommand(@$"UPDATE dbo.workouts
	                    SET sport_id=@sport_id, date=@date, duration=@dur, intensity=@intensity, notes=@notes
	                    WHERE id = {item.ID};", conn))
                {
                    cmd.Parameters.AddWithValue("date", dtPicker.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("sport_id", cbSports.SelectedValue);
                    cmd.Parameters.AddWithValue("dur", Convert.ToInt32(tbDuration.Text));
                    cmd.Parameters.AddWithValue("intensity", tbIntensivity.Text);
                    cmd.Parameters.AddWithValue("notes", tbNote.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            this.Close();
        }
    }
}
