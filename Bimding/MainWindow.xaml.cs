using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;

namespace Bimding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            txtInput.Focus();
        }
        //private void btnSet_Click(object sender, RoutedEventArgs e)
        //{
        //    if(!string.IsNullOrWhiteSpace(txtInput.Text)) 
        //    tbResult.Text = txtInput.Text;
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        private string bounText;

        public string BoundText
        {
            get { return bounText; }
            set 
            { 
                bounText = value;
                OnPropertyChanged();
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BoundText))); 
            }
        }
        void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            BoundText = "Set from button";
        }
    }
}
