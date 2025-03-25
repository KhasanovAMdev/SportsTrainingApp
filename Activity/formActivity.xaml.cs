using System.Windows;
using System.Windows.Data;
using Npgsql;
using SportsTrainingApp.Activity;
using SportsTrainingApp.Challenges;
using SportsTrainingApp.Nutrition;

namespace SportsTrainingApp
{
    /// <summary>
    /// Interaction logic for formActivity.xaml
    /// </summary>
    public partial class formActivity : Window
    {
        int user_id = -1;
        public formActivity(int _user_id)
        {
            InitializeComponent();
            user_id = _user_id;
            ShowCurUser(user_id);
            FillDataGrid(user_id);
        }

        public class Workout
        {
            public int ID { get; set; }
            public string? Name { get; set; }
            public DateTime Date { get; set; }
            public int Duration { get; set; }
            public string? Intensity { get; set; }
            public string? Notes { get; set; }
        }

        private void ShowCurUser(int _user_id)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"SELECT
	                                                CONCAT(USERNAME, ' (email - ', EMAIL, ')')
                                                FROM
	                                                DBO.USERS
                                                WHERE
	                                                ID = {_user_id}", conn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tbCurUser.Text += reader.GetString(0);
                    }
                }
            }
        }

        public void FillDataGrid(int _user_id)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"SELECT
                                                    WORKOUTS.ID,    
	                                                S.NAME,
	                                                DATE,
	                                                DURATION,
	                                                INTENSITY,
	                                                NOTES
                                                FROM
	                                                DBO.WORKOUTS
	                                                LEFT JOIN DBO.SPORTS S ON S.ID = WORKOUTS.SPORT_ID
                                                WHERE
	                                                USER_ID = {_user_id}
                                                ORDER BY DATE", conn))
                {
                    var reader = cmd.ExecuteReader();
                    var workouts = new List<Workout>();
                    while (reader.Read())
                    {
                        workouts.Add(new Workout
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Date = reader.GetDateTime(2),
                            Duration = reader.GetInt32(3),
                            Intensity = reader.GetString(4),
                            Notes = reader.GetString(5)
                        });
                    }
                    gridActivity.ItemsSource = workouts;
                }
            }
        }

        private void btnAddActivity_Click(object sender, RoutedEventArgs e)
        {
            new formAddActivity(user_id).ShowDialog();
            FillDataGrid(user_id);
        }

        private void btnDelActivity_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Workout)gridActivity.SelectedItem;

            if (selectedItem == null) 
                return;

            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"DELETE FROM DBO.WORKOUTS
                                                      WHERE
	                                                      ID = {selectedItem.ID}",conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            FillDataGrid(user_id);
        }

        private void btnUpdActivity_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Workout)gridActivity.SelectedItem;

            if (selectedItem == null)
                return;

            new formUpdActivity(selectedItem).ShowDialog();

            FillDataGrid(user_id);
        }

        private void btNutrition_Click(object sender, RoutedEventArgs e)
        {
            new formNutrition(user_id).ShowDialog();
        }

        private void btChallenges_Click(object sender, RoutedEventArgs e)
        {
            new formChallenges().ShowDialog();
        }
    }
}
