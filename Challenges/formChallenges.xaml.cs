using Npgsql;
using System.Windows;

namespace SportsTrainingApp.Challenges
{
    public partial class formChallenges : Window
    {

        public formChallenges()
        {
            InitializeComponent();
            FillDataGrid();
        }

        public class Challenges
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Start_date { get; set; }
            public DateTime End_date { get; set; }
        }

        public void FillDataGrid()
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"SELECT
	                                                    ID,
	                                                    NAME,
	                                                    DESCRIPTION,
	                                                    START_DATE,
	                                                    END_DATE
                                                    FROM
	                                                    DBO.CHALLENGES
                                                    ORDER BY
	                                                    START_DATE", conn))
                {
                    var reader = cmd.ExecuteReader();
                    var challenges = new List<Challenges>();
                    while (reader.Read())
                    {
                        challenges.Add(new Challenges
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Start_date = reader.GetDateTime(3),
                            End_date = reader.GetDateTime(4)
                        });
                    }
                    gridChallenges.ItemsSource = challenges;
                }
            }
        }

        private void btAddChallenges_Click(object sender, RoutedEventArgs e)
        {
            new formAddChallenges().ShowDialog();
            FillDataGrid();
        }

        private void btDelChallenges_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Challenges)gridChallenges.SelectedItem;

            if (selectedItem == null)
                return;

            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"DELETE FROM DBO.CHALLENGES
                                                      WHERE
	                                                      ID = {selectedItem.ID}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            FillDataGrid();
        }

        private void btUpdChallenges_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Challenges)gridChallenges.SelectedItem;

            if (selectedItem == null)
                return;

            new formUpdChallenges(selectedItem).ShowDialog();

            FillDataGrid();
        }
    }
}
