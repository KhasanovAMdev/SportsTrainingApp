using System.Windows;
using Npgsql;

namespace SportsTrainingApp.Challenges
{
   
    public partial class formAddChallenges : Window
    {

        public formAddChallenges()
        {
            InitializeComponent();
            dtStart.SelectedDate = DateTime.Now;
            dtEnd.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void AddChallenges()
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"INSERT INTO dbo.challenges(
	                                                    name, description, start_date, end_date)
	                                                    VALUES (@name, @description, @start_date, @end_date);", conn))
                {
                    cmd.Parameters.AddWithValue("name", tbName.Text);
                    cmd.Parameters.AddWithValue("description", tbDescription.Text);
                    cmd.Parameters.AddWithValue("start_date", dtStart.SelectedDate);
                    cmd.Parameters.AddWithValue("end_date", dtEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                }
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddChallenges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
