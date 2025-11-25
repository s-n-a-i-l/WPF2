using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void onButtonStartClick(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = (sender as Button).Content == "Stop" ? "Start" : "Stop";
            //btnStart.Content = btnStart.Content == "Stop" ? "Start" : "Stop";
            //MessageBox.Show("Hello, WPF!");
        }
    }
}
