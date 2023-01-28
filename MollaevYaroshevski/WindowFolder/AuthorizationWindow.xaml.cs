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
using System.Xml;


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
                                    ChangeXML();
                                    break;
                                case 2:
                                    MBClass.InfoMB("Rab");
                                    ChangeXML();
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
            int w = (int)CaptchaIMG.Width;
            int h = (int)CaptchaIMG.Height;
            var CaptchaCode = Captcha.GenerateCaptcha();
            _CaptchaCode = CaptchaCode;

            var res = Captcha.GenerateImage(w, h, CaptchaCode);

            Stream stream = new MemoryStream(res.CaptchaByteData);

            CaptchaIMG.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        }

        XmlDocument load;
        XmlElement xmlElement;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load = new XmlDocument();
            load.Load(@"C:\\Users\\kommo\\Source\\Repos\\ProgaEntityHalfDone1\\MollaevYaroshevski\\ResourceFolder\\LoadFile.xml"); 
            xmlElement = load.DocumentElement;
            if (xmlElement != null)
            {
                foreach (XmlNode node in xmlElement)
                {
                    if (node?.FirstChild.InnerText == "1")
                    {
                        RememberCHB.IsChecked = true;
                    }
                    else if (node?.FirstChild.InnerText == "0")
                    {
                        break;
                    }
                    if (node.Name == "user")
                    {
                        foreach (XmlElement el in node)
                        {
                            if (el?.Name == "login")
                            {
                                LOginText.Text = el.InnerText;
                            }
                            if (el?.Name == "password")
                            {
                                PasswordPSB.Password = el.InnerText;
                            }
                        }
                    }
                }
            }
        }

        private void ChangeXML()
        {
            load = new XmlDocument();
            load.Load(@"C:\\Users\\kommo\\Source\\Repos\\ProgaEntityHalfDone1\\MollaevYaroshevski\\ResourceFolder\\LoadFile.xml");
            xmlElement = load.DocumentElement;
            bool Check = false;
            if (xmlElement != null)
            {
                foreach (XmlNode node in xmlElement)
                {
                    if (RememberCHB.IsChecked == false)
                    {
                        node.FirstChild.InnerText = "0";
                        break;
                    }
                    else if (RememberCHB.IsChecked == true)
                    {
                        node.FirstChild.InnerText = "1";
                        Check = true;
                    }
                    if (Check)
                    {
                        if (node.Name == "user")
                        {
                            foreach (XmlElement el in node)
                            {
                                if (el?.Name == "login")
                                {
                                    el.InnerText = LOginText.Text;
                                }
                                if (el?.Name == "password")
                                {
                                    el.InnerText = PasswordPSB.Password;
                                }
                            }
                        }
                    }
                }
            }
            load.Save(@"C:\Users\\kommo\Source\Repos\ProgaEntityHalfDone1\MollaevYaroshevski\ResourceFolder\LoadFile.xml");
        }

        private void recoverBD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new RecoverAccWindow().Show();
            Close();
        }
    }
}
