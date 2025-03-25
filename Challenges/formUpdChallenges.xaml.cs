using System.Windows;
using Npgsql;

namespace SportsTrainingApp.Challenges
{
   
    public partial class formUpdChallenges : Window
    {
        formChallenges.Challenges item;
        public formUpdChallenges(formChallenges.Challenges _item)
        {
            InitializeComponent();
            item = _item;
            FillFormFields(item);
        }

        private void FillFormFields(formChallenges.Challenges _item)
        {
            tbName.Text = _item.Name;
            tbDescription.Text = _item.Description;
            dtEnd.SelectedDate = _item.End_date.Date;
            dtStart.SelectedDate = _item.Start_date.Date;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"UPDATE dbo.challenges
	                                                  SET name=@name, description=@description, start_date=@start_date, end_date=@end_date
	                                                    WHERE id = {item.ID};", conn))
                {
                    cmd.Parameters.AddWithValue("name", tbName.Text);
                    cmd.Parameters.AddWithValue("description", tbDescription.Text);
                    cmd.Parameters.AddWithValue("start_date", dtStart.SelectedDate);
                    cmd.Parameters.AddWithValue("end_date", dtEnd.SelectedDate);
                    cmd.ExecuteNonQuery();
                }
            }
            this.Close();
        }
    }
}