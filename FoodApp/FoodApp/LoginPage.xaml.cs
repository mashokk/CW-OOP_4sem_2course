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
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        bool isLogin = false;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DbRecipes db = new DbRecipes();

            string login = tbLogin.Text;
            string password = tbPassword.Password;

            try
            {

                Users user = db.Users.Where((u) => u.Login == login && u.Password == password).Single();
                //MessageBox.Show("Успешная авторизация!", $"Привет, {user.Username}!");

                isLogin = true;
                

                if (user.ID == 1)
                {
                    MessageBoxResult result = MessageBox.Show("Хотите открыть таблицы с блюдами?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                        NavigationService.Navigate(new EditPage());
                }
                else
                {
                    NavigationService.Navigate(new LoadingPage());
                }
                
            }
            catch
            {
                MessageBox.Show("Ошибка!", $"Неверный логин или пароль!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
