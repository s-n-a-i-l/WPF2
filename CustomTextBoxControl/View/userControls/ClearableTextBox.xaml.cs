using System;
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

namespace CustomTextBoxControl.View.userControls
{
    /// <summary>
    /// Interaction logic for ClearableTextBox.xaml
    /// </summary>
    public partial class ClearableTextBox : UserControl
    {

        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = tbPlaceholder.Text =value; }
        }

        public ClearableTextBox()
        {
            InitializeComponent();

        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbPlaceholder.Visibility = txtInput.Text == " " ? Visibility.Visible: Visibility.Hidden;
            //if (txtInput.Text.Length > 19) 
            //{ 
            //  txtInput.Text = txtInput.Text.Substring(0, 19); 
            //  txtInput.Select(txtInput.Text.Length, 0); 
            //}
            //if(txtInput.Text.Length > 16)txtInput.Text = txtInput.Text.Substring(0, 16);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
              Window window = Window.GetWindow(this);
              
            }
        }
    }
}
