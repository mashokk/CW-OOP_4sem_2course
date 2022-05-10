using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
    public partial class FilterPage : Page
    {
        DbRecipes db; //Для работы с базой
        public FilterPage()
        {
            InitializeComponent();


            db = new DbRecipes();
            db.Dishes.Load();
            List<Dishes> RV = db.Dishes.Local.ToList();
            //RecipesView.ItemsSource = RV;

            //ингридиентыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыы
            //BindingList<Dish_Composition> ingredients = db.Dish_Composition.Local.ToBindingList();
            //List<Ingredients> items = db.Ingredients.Local.ToList();
            //lvDataBinding.ItemsSource = items;
        }

        public class Ingreds
        {
            public List<Dish_Composition> dc { get; set; } = new List<Dish_Composition>();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPage());
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DbRecipes.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                RecipesView.ItemsSource = DbRecipes.GetContext().Dishes.ToList();
            }
        }
    }
}
