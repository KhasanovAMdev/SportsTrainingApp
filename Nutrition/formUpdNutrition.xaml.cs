using System.Windows;
using Npgsql;

namespace SportsTrainingApp.Nutrition
{
    /// <summary>
    /// Interaction logic for formUpdNutrition.xaml
    /// </summary>
    public partial class formUpdNutrition : Window
    {
        formNutrition.Nutrition item;
        public formUpdNutrition(formNutrition.Nutrition _item)
        {
            InitializeComponent();
            item = _item;
            FillFormFields(item);
        }

        private void FillFormFields(formNutrition.Nutrition _item)
        {
            dtPicker.SelectedDate = _item.Date;
            tbCalories.Text = _item.Calories.ToString();
            tbCarbs.Text = _item.Carbs.ToString();
            tbProtein.Text = _item.Protein.ToString();
            tbFats.Text = _item.Fats.ToString();
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
                using (var cmd = new NpgsqlCommand(@$"UPDATE dbo.nutrition 
                        SET user_id=@user_id, date=@date, calories=@calories, protein=@protein, carbs=@carbs, fats=@fats
	                    WHERE id = {item.ID};", conn))
                {
                    cmd.Parameters.AddWithValue("user_id", item.User_id);
                    cmd.Parameters.AddWithValue("date", dtPicker.SelectedDate.Value);
                    cmd.Parameters.AddWithValue("calories", Convert.ToInt32(tbCalories.Text));
                    cmd.Parameters.AddWithValue("protein", Convert.ToDouble(tbProtein.Text));
                    cmd.Parameters.AddWithValue("carbs", Convert.ToDouble(tbCarbs.Text));
                    cmd.Parameters.AddWithValue("fats", Convert.ToDouble(tbFats.Text));
                    cmd.ExecuteNonQuery();
                }
            }
            this.Close();
        }
    }
}