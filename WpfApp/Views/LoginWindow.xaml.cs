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
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = ((LoginWindow)sender).DataContext as ViewModels.LoginViewModel;
            if (vm.DbRemindMe == "True")
            {
                ((App)Application.Current).CurrentDb = vm.Db;
            }
            else
            {
                ((App)Application.Current).CurrentDb.Clear();
            }
            ((App)Application.Current).CurrentDb.Save();
        }

        private void LoginTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            LoginTextBox.Text = "admin1";
            PasswordTextBox.Text = "admin1";
            ((LoginViewModel)this.DataContext).DoLogin(LoginTextBox.Text, PasswordTextBox.Text);
#endif
        }
    }
}
