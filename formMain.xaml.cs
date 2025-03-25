using System.Windows;

namespace SportsTrainingApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class formMain : Window
{
    public const string connectionString = "Host=192.168.0.110:5432;Username=KhasanovAM;Password=Admin123;Database=SportsTrainingApp";
    public formMain()
    {
        InitializeComponent();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        var formLogin = new formLogin(this);
        formLogin.ShowDialog();
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        new formRegistration().ShowDialog();
    }
}