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
using System.Windows.Shapes;

namespace WPF_OOP_Testing.Fenster
{
    /// <summary>
    /// Interaktionslogik für LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Window
    {
        private int triesLeft = 4;
        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (PwdBox.Password == "123" && triesLeft > 0)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                this.Height = 265;
                triesLeft--;
                if (triesLeft != 1 && triesLeft > 0)
                {
                    PwWrong.Content = "Incorrect password. " + triesLeft.ToString() + " tries left.";
                }
                else if (triesLeft == 1)
                {
                    PwWrong.Content = "Incorrect password. " + triesLeft.ToString() + " try left.";
                }
                else
                {
                    PwWrong.Content = "Incorrect password. No tries left";
                }
                PwWrong.Foreground = new SolidColorBrush(Colors.Red);
                PwdBox.Password = "";
                //MessageBox.Show("Invalid Login attempt.");
            }
        }
    }
}
