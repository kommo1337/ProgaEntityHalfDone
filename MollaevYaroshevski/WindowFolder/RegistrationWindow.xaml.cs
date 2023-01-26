using MollaevYaroshevski.ClassFolder;
using MollaevYaroshevski.DataFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MollaevYaroshevski.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            SecBorder.MouseLeftButtonDown += (o, e) => DragMove();
        }

        private void RegBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Login");
                LoginTB.Focus();
            }
            else if (DBEntities.GetContext().User.FirstOrDefault(u => u.Login == LoginTB.Text) != null)
            {
                MBClass.ErrorMB("Существует");
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPSB.Password))
            {
                MBClass.ErrorMB("НЕту");

            }
            else if (string.IsNullOrWhiteSpace(RepeatPasswordPSB.Password))
            {
                MBClass.ErrorMB("Нету");
            }
            else
            {
                try
                {
                    DBEntities.GetContext().User.Add(new User()
                    {
                        Login = LoginTB.Text,
                        Password = PasswordPSB.Password,
                        IdRole = 2
                    });

                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Успешно");
                }
                catch (Exception ex)
                {

                    MBClass.ErrorMB(ex);
                }

                
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
