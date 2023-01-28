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
    /// Логика взаимодействия для RecoverAccWindow.xaml
    /// </summary>
    public partial class RecoverAccWindow : Window
    {
        public RecoverAccWindow()
        {
            InitializeComponent();
            SecBorder.MouseDown += (o, e) => DragMove();
        }

        private void RecoverBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

                User user = DBEntities.GetContext().User.FirstOrDefault(x => x.Login == LoginTB.Text );

            //User CodeWord = DBEntities.GetContext().User.FirstOrDefault(u => u.Сodeword == CodeWordPSB.Text);

            if (user.Сodeword == CodeWordPSB.Text)
            {
                try
                {
                    user.Password = PasswordPSB.Password;
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Пароль поменян");
                    new AuthorizationWindow().Show();
                    Close();
                }
                catch (Exception ex)
                {

                    MBClass.ErrorMB(ex);

                }
            }
            else
                MBClass.ErrorMB("Не верное кодовое слово");

                


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
