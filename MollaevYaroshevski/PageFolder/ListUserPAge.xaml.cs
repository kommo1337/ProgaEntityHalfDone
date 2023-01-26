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
    /// Логика взаимодействия для ListUserPAge.xaml
    /// </summary>
    public partial class ListUserPAge : Page
    {
        public ListUserPAge()
        {
            InitializeComponent();
            UserDG.ItemsSource = DBEntities.GetContext().User.ToList()
            .OrderBy(c => c.IdUser);
        }

        private void modifyIt_Click(object sender, RoutedEventArgs e)
        {
            User user = UserDG.SelectedItem as User;
            VariableClass.IdUser = user.IdUser;
            VariableClass.LoginUser = user.Login;
            VariableClass.PasswordUser = user.Password;
            NavigationService.Navigate(new EditUserPage());
        }

        private void deleteIt_Click(object sender, RoutedEventArgs e)
        {
            User user = UserDG.SelectedItem as User;

                                   
            DBEntities.GetContext().User.Remove(user);

            DBEntities.GetContext().SaveChanges();

            UserDG.ItemsSource = DBEntities.GetContext().User.ToList()
            .OrderBy(c => c.IdUser);
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UserDG.ItemsSource = DBEntities.GetContext().User.Where
                (u => u.Login.StartsWith(SearchTB.Text)).ToList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }
    }
}
