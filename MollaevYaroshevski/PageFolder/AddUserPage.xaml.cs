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

using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MollaevYaroshevski.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
            RoleCB.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void AddBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DBEntities.GetContext().User.Add(new User()
                {
                    Login = LoginTB.Text,
                    Password = PasswordTB.Text,
                    IdRole = Convert.ToInt32(RoleCB.SelectedValue.ToString())
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
}
