using System.Windows;
using Npgsql;

namespace SportsTrainingApp.Nutrition
{
    /// <summary>
    /// Interaction logic for formAddNutrition.xaml
    /// </summary>
    public partial class formAddNutrition : Window
    {
        int user_id = -1;
        public formAddNutrition(int _user_id)
        {
            InitializeComponent();
            user_id = _user_id;
            dtPicker.SelectedDate = DateTime.Now;
        }

        private void AddNutrition()
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"INSERT INTO dbo.nutrition(
	                                                user_id, date, calories, protein, carbs, fats)
	                                                VALUES (@user_id, @date, @calories, @protein, @carbs, @fats);", conn))
                {
                    cmd.Parameters.AddWithValue("user_id", user_id);
                    cmd.Parameters.AddWithValue("date", dtPicker.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("calories", Convert.ToInt32(tbCalories.Text));
                    cmd.Parameters.AddWithValue("protein", Convert.ToDouble(tbProtein.Text));
                    cmd.Parameters.AddWithValue("carbs", Convert.ToDouble(tbCarbs.Text));
                    cmd.Parameters.AddWithValue("fats", Convert.ToDouble(tbFats.Text));
                    cmd.ExecuteNonQuery();
                }
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddNutrition();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
