using MollaevYaroshevski.ClassFolder;
using MollaevYaroshevski.DataFolder;
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


namespace MollaevYaroshevski.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {

        public AuthorizationWindow()
        {
            InitializeComponent();
            SecBorder.MouseDown += (o, e) => DragMove();
        }

        private void LoginBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(PasswordPSB.Password))
            {
                MBClass.ErrorMB("Password");
                PasswordPSB.Focus();
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext().User.FirstOrDefault(u => u.Login == LOginText.Text);
                    if (user == null)
                    {
                        MBClass.ErrorMB("Incorect Login");
                        return;
                    }
                    if (user.Password != PasswordPSB.Password)
                    {
                        MBClass.ErrorMB("Incorect PAssword");
                        return;
                    }
                    else
                    {
                        switch (user.IdRole)
                        {
                            case 1:
                                new AdminWindow().Show();
                                break;
                            case 2:
                                MBClass.InfoMB("Rab");
                                break;
                        }

                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            
            
        }

        private void RegBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new RegistrationWindow().Show();
            Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
