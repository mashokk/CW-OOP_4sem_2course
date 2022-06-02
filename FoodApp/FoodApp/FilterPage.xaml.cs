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

            //комбобокс с ингридиентами
            List<Ingredients> ingredients = db.Ingredients.ToList();
            ComboIngr.ItemsSource = ingredients;
            ComboIngr.SelectedValuePath = "ID";
            ComboIngr.DisplayMemberPath = "Ingredient_name";
            var allTypes2 = DbRecipes.GetContext().Ingredients.ToList();
            allTypes2.Insert(0, new Ingredients
            {
                Ingredient_name = "Все ингредиенты"
            });
            ComboIngr.ItemsSource = allTypes2;
            ComboIngr.SelectedIndex = 0;



            //комбобокс с группой блюда
            List<Groups> gr = db.Groups.ToList();
            ComboType.ItemsSource = gr;
            ComboType.SelectedValuePath = "ID";
            ComboType.DisplayMemberPath = "Group_name";
            var allTypes = DbRecipes.GetContext().Groups.ToList();
            allTypes.Insert(0, new Groups
            {
                Group_name = "Все группы"
            });
            ComboType.ItemsSource = allTypes;
            ComboType.SelectedIndex = 0;



            UpdateDishes();
        }

        private void UpdateDishes()
        {
            db = new DbRecipes();

            
            RecipesView.ItemsSource = db.Dishes.Where(x => x.Dish_name.Contains(TBoxSearch.Text)).ToList();
            

            if (ComboType.SelectedIndex > 0) 
            {
                RecipesView.ItemsSource = db.Dishes.Where(x => x.Dish_name.Contains(TBoxSearch.Text)).Where(x => x.ID_Group == ComboType.SelectedIndex).ToList();
                return;
            }

            if (ComboIngr.SelectedIndex > 0)
            {
                var iddish = db.Dish_Composition.Where(u => u.ID_Ingredient == ComboIngr.SelectedIndex).Select(i => i.ID_Dish).ToList();                                    //список с айди блюд где айди ингридиента = айди выбраного ингридиента
                RecipesView.ItemsSource = db.Dishes.Where(x => x.Dish_name.Contains(TBoxSearch.Text)).Where(e => iddish.Contains(e.ID)).ToList();
                return;
            }

            if (ComboIngr.SelectedIndex > 0 && ComboType.SelectedIndex > 0)
            {
                var iddish = db.Dish_Composition.Where(u => u.ID_Ingredient == ComboIngr.SelectedIndex).Select(i => i.ID_Dish).ToList();                                    //список с айди блюд где айди ингридиента = айди выбраного ингридиента
                RecipesView.ItemsSource = db.Dishes.Where(x => x.ID_Group == ComboType.SelectedIndex).Where(e => iddish.Contains(e.ID)).ToList();
                return;
            }
        }

        public class Ingreds
        {
            public List<Dish_Composition> dc { get; set; } = new List<Dish_Composition>();
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

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDishes();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDishes();
        }

        private void ComboIngr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDishes();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) //открытие блюда в отдельном окне при двойном клике на item
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var item = e.Source as ListViewItem;
                ViewPage vp = new ViewPage(item.IsSelected);
                NavigationService.Navigate(new ViewPage(item.DataContext));
            }
        }

        private void RandGenerate_Click(object sender, RoutedEventArgs e)
        {
            int id = db.Dishes.Max(m => m.ID);
            Random rnd = new Random();
            int value = rnd.Next(1, id + 1);

            var dish = db.Dishes.Where(n => n.ID == value).Select(x => x.Dish_name).FirstOrDefault();

            //MessageBox.Show(String.Format("Рекомендуем Вам приготовить блюдо: {0}.", dish), "Рандомное блюдо!");

            TBoxSearch.Text = dish;
        }

    }
}
