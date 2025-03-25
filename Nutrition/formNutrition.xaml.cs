using Npgsql;
using System.Windows;

namespace SportsTrainingApp.Nutrition
{
    /// <summary>
    /// Interaction logic for fromNutrition.xaml
    /// </summary>
    public partial class formNutrition : Window
    {
        int user_id = -1;
        public formNutrition(int _user_id)
        {
            InitializeComponent();
            user_id = _user_id;
            FillDataGrid(user_id);

        }
        public class Nutrition
        {
            public int ID { get; set; }
            public int User_id { get; set; }
            public DateTime Date { get; set; }
            public int Calories { get; set; }
            public double Protein { get; set; }
            public double Carbs { get; set; }
            public double Fats { get; set; }
        }

        public void FillDataGrid(int _user_id)
        {
            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($@"SELECT
	                                                ID,
	                                                USER_ID,
	                                                DATE,
	                                                CALORIES,
	                                                PROTEIN,
	                                                CARBS,
	                                                FATS
                                                FROM
	                                                DBO.NUTRITION
                                                WHERE
	                                                USER_ID = {_user_id}
                                                ORDER BY DATE", conn))
                {
                    var reader = cmd.ExecuteReader();
                    var nutrition = new List<Nutrition>();
                    while (reader.Read())
                    {
                        nutrition.Add(new Nutrition
                        {
                            ID = reader.GetInt32(0),
                            User_id = reader.GetInt32(1),
                            Date = reader.GetDateTime(2),
                            Calories = reader.GetInt32(3),
                            Protein = reader.GetDouble(4),
                            Carbs = reader.GetDouble(5),
                            Fats = reader.GetDouble(6)
                        });
                    }
                    gridNutrition.ItemsSource = nutrition;
                }
            }
        }

        private void btAddNutrition_Click(object sender, RoutedEventArgs e)
        {
            new formAddNutrition(user_id).ShowDialog();
            FillDataGrid(user_id);
        }

        private void btDelNutrition_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Nutrition)gridNutrition.SelectedItem;

            if (selectedItem == null)
                return;

            using (var conn = new NpgsqlConnection(formMain.connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"DELETE FROM DBO.NUTRITION
                                                      WHERE
	                                                      ID = {selectedItem.ID}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            FillDataGrid(user_id);
        }

        private void btUpdNutrution_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Nutrition)gridNutrition.SelectedItem;

            if (selectedItem == null)
                return;

            new formUpdNutrition(selectedItem).ShowDialog();

            FillDataGrid(user_id);
        }
    }
}
