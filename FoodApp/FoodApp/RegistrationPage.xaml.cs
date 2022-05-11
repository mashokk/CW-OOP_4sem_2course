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

namespace FoodApp
{
    public partial class RegistrationPage : Page
    {
        DbRecipes db;
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
                db = new DbRecipes();
                string username = tbusername.Text;
                string login = tblogin.Text;
                string password = tbpassword.Password;
                int id = db.Users.Max(m => m.ID);
                try
                {
                    Users newus = new Users
                    {
                        Username = username,
                        Login = login,
                        Password = password,
                        ID = id + 1
                    };
                    db.Users.Add(newus);
                    db.SaveChanges();
                    Users user = db.Users.FirstOrDefault((u) => u.Login == login);
                    MessageBox.Show("Успешная регистрация!", $"Добро пожаловать, {user.Username}!");
                    NavigationService.Navigate(new FilterPage());
                }
                catch
                {
                    MessageBox.Show("Ошибка!", $"Неверный логин или пароль!");
                }
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }*/

        private void aPicture_MouseDown(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
