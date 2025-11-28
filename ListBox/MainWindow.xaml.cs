using System;
using System.IO;
using System.Collections.Generic;
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

namespace ListBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Directory.SetCurrentDirectory("C:\\");

            string[] files = Directory.EnumerateFiles("C:\\Windows").ToArray();

            for (int i = 0; i < files.Length; i++) 
            {
              listBox.Items.Add(files[i].Split('\\').Last());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(!listBox.Items.Contains(txtInput.Text))
            listBox.Items.Add(txtInput.Text);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //listBox.Items.Remove(listBox.SelectedItem);
            //System.Collections.IList files = new  List(listBox.SelectedItems);
            //foreach (object i in files) 
            //{
            //    listBox.Items.Remove(i);
            //}
            while (listBox.SelectedItems.Count > 0) listBox.Items.Remove(listBox.SelectedItem);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
        }

    }
}
