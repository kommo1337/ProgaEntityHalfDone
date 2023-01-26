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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MollaevYaroshevski.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        public EditUserPage()
        {
            InitializeComponent();

            RoleCB.ItemsSource = DBEntities.GetContext().Role.ToList();

            var userLogin =  DBEntities.GetContext().User.FirstOrDefault(x => x.Login == VariableClass.LoginUser);

            var userPass = DBEntities.GetContext().User.FirstOrDefault(x => x.Password == VariableClass.PasswordUser);

            LoginTB.Text = userLogin.Login;

            PasswordTB.Text = userPass.Password;
        }

        private void AddBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            User user = DBEntities.GetContext().User.FirstOrDefault(x => x.IdUser == VariableClass.IdUser);

            user.Login = LoginTB.Text;
            user.Password = PasswordTB.Text;
            user.IdRole = Convert.ToInt32(RoleCB.SelectedValue.ToString());
            DBEntities.GetContext().SaveChanges();
            MBClass.InfoMB("Отредактированно");

            NavigationService.Navigate(new ListUserPAge());
        }
    }
}
