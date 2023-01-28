using MollaevYaroshevski.ClassFolder;
using MollaevYaroshevski.DataFolder;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string _CaptchaCode;

        public AuthorizationWindow()
        {
            InitializeComponent();
            SecBorder.MouseDown += (o, e) => DragMove();
            LoadContentCaptcha();

        }

        private void LoginBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CaptchaTB.Text == _CaptchaCode)
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
            else
            {
                MBClass.ErrorMB("Не верная каптча");
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

        private void Refresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoadContentCaptcha();
        }

        void LoadContentCaptcha()
        {
            int w = 100;
            int h = 36;
            var CaptchaCode = Captcha.GenerateCaptcha();
            _CaptchaCode = CaptchaCode;

            var res = Captcha.GenerateImage(w, h, CaptchaCode);

            Stream stream = new MemoryStream(res.CaptchaByteData);

            CaptchaIMG.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }
    }
}
