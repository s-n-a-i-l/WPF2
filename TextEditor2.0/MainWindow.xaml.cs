using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace TextEditor2._0
{
    public partial class MainWindow : Window
    {
        private bool _isSaveCompletePopupOpen = false;
        public bool IsSaveCompletePopupOpen
        {
            get => _isSaveCompletePopupOpen;
            set
            {
                if (_isSaveCompletePopupOpen != value)
                {
                    _isSaveCompletePopupOpen = value;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            FillComboBox();
            cbFontStyle.ItemsSource = Fonts.SystemFontFamilies;
            cbFontStyle.SelectedItem = new FontFamily("Segoe UI");
        }

        public void FillComboBox()
        {
            for (int i = 0; i <= 100; i++)
            {
                cbFontSize.Items.Add(i);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window WindowParent = Window.GetWindow(this);
            WindowParent.Close();
        }

        private void cbFontStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cbFontStyle.SelectedItem == null || txtBox1 == null)
                return;

            FontFamily selectedFont = (FontFamily)cbFontStyle.SelectedItem;

            var selection = txtBox1.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontFamilyProperty, selectedFont);
            }
            else
            {
              
               txtBox1.FontFamily = selectedFont;
            }
        }
        

        private void cbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectItem = cbFontSize.SelectedIndex + 1;
                var selection = txtBox1.Selection;
                if (selection.IsEmpty)
                {
                    txtBox1.FontSize = selectItem;
                }
                else
                {
                    selection.ApplyPropertyValue(TextElement.FontSizeProperty, (double)selectItem);
                }
            }
            catch (Exception ex) { System.Windows.MessageBox.Show($"Ошибка изменения размера шрифта: {ex.Message}"); }

        }

        public void Save() 
        {
            IsSaveCompletePopupOpen = false;

            System.Windows.Forms.SaveFileDialog saveFile1 = new System.Windows.Forms.SaveFileDialog();
            saveFile1.DefaultExt = "*.txt";
            saveFile1.Filter = "Text files|*.txt";
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                saveFile1.FileName.Length > 0)
            {
                try
                {
                    // Сохраняем только текст без форматирования
                    string textToSave = new TextRange(
                        txtBox1.Document.ContentStart, txtBox1.Document.ContentEnd
                    ).Text;

                    File.WriteAllText(saveFile1.FileName, textToSave);
                    System.Windows.MessageBox.Show("Файл успешно сохранен.");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                }

            }
        }
        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void txtBox1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                System.Windows.Controls.RichTextBox rtb = sender as System.Windows.Controls.RichTextBox;
                if (rtb != null)
                {
                    e.Handled = true;
                    double currentSize = rtb.FontSize;
                    double targetSize;

                    if (e.Delta > 0) targetSize = currentSize + 2;
                    else
                    {
                        targetSize = currentSize - 2;
                        if (targetSize < 1) targetSize = 1;
                    }

                    DoubleAnimation animation = new DoubleAnimation();
                    animation.To = targetSize;
                    animation.Duration = TimeSpan.FromMilliseconds(150);
                    animation.EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut };

                    rtb.BeginAnimation(System.Windows.Controls.RichTextBox.FontSizeProperty, animation);
                }
            }
        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();

            file.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = file.FileName;
                try
                {
                    string fileContent = File.ReadAllText(filePath);
                    new TextRange(txtBox1.Document.ContentStart, txtBox1.Document.ContentEnd).Text = fileContent;
                    System.Windows.MessageBox.Show($"Файл успешно загружен: {System.IO.Path.GetFileName(filePath)}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (IOException ex) { System.Windows.MessageBox.Show($"Ошибка чтения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
                catch (Exception ex) { System.Windows.MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }

        }

        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            if (txtBox1 != null) 
            {
                Save();
                txtBox1.Document.Blocks.Clear();
            }
        }
    }

}