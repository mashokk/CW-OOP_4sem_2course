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
    public partial class LoadingPage : Page
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public LoadingPage()
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(1);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    NavigationService.Navigate(new FilterPage());
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
    }
}
