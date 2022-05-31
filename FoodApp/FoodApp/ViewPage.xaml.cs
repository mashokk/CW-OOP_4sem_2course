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
using System.Windows.Threading;

namespace FoodApp
{
    public partial class ViewPage : Page
    {
        DbRecipes db = new DbRecipes(); //Для работы с базой

        DispatcherTimer _timer;
        TimeSpan _time;

        public ViewPage(object dish)
        {
            InitializeComponent();
            DataContext = dish;

            sec.MaxLength = 2; //ограничение на кол-во символов
            min.MaxLength = 2;
            hours.MaxLength = 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilterPage());
        }

        private void TimerGo_Click(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(sec.Text, @"[0-9]+") && System.Text.RegularExpressions.Regex.IsMatch(min.Text, @"[0-9]+") && System.Text.RegularExpressions.Regex.IsMatch(hours.Text, @"[0-9]+"))
            {

                int seconds = Convert.ToInt32(sec.Text) + Convert.ToInt32(min.Text) * 60 + Convert.ToInt32(hours.Text) * 3600 ;
                _time = TimeSpan.FromSeconds(seconds);

                sec.Visibility = Visibility.Hidden;
                min.Visibility = Visibility.Hidden;
                hours.Visibility = Visibility.Hidden;
                l1.Visibility = Visibility.Hidden;
                l2.Visibility = Visibility.Hidden;
                tbTime.Visibility = Visibility.Visible;


                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    tbTime.Text = _time.ToString("c");
                    if (_time == TimeSpan.Zero)
                    {
                        _timer.Stop();
                        MessageBox.Show("Время вышло!");
                        sec.Visibility = Visibility.Visible;
                        min.Visibility = Visibility.Visible;
                        hours.Visibility = Visibility.Visible;
                        l1.Visibility = Visibility.Visible;
                        l2.Visibility = Visibility.Visible;
                        tbTime.Visibility = Visibility.Hidden;
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else
            {
                MessageBox.Show("В таймере могут быть ТОЛЬКО цифры!", $"Ошибка!");
            }
        }

        private void TimerReset_Click(object sender, RoutedEventArgs e)
        {
            _time = TimeSpan.FromSeconds(0);
        }
    }
}
