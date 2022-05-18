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
    /// Логика взаимодействия для ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        DbRecipes db; //Для работы с базой
        public ViewPage()
        {
            InitializeComponent();
            using (DbRecipes db = new DbRecipes())
            {
                /*var nnamee = Application.Current.Resources["TT"];
                string d = nnamee.ToString();
                var dish = db.Dishes.Where(n => n.Dish_name == d).Select(x => x.ID).FirstOrDefault();

                DName.Text = dish.ID;*/
            }
        }
    }
}
