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


            var data = RecipesView.ItemsSource;



            db = new DbRecipes();
            db.Dishes.Load();
            //List<Dishes> RV = db.Dishes.Local.ToList();

            /*int IDElement = RecipesView.SelectedIndex;
            int i = (sender as ListView).SelectedIndex + 1;
            AAA.Text = IDElement.ToString();*/



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



            /////////////////////////////////////////////////////////////////////////хрень полная
            /*int indexdish = 0;
            indexdish = RecipesView.SelectedIndex;
            AAA.Text = indexdish.ToString();*/

            /////////////////////////////////////////////////////////////////////////хрень полная
            /*int indexdish = 0;
            if (RecipesView.SelectedItems.Count > 0)
            {
                for (int lcount = 0; lcount <= RecipesView.Items.Count - 1; lcount++)
                {
                    if (RecipesView.Items[lcount].Selected == true)
                    {
                        indexdish = RecipesView.SelectedIndex;
                        break;
                    }
                }
            }
            AAA.Text = indexdish.ToString();*/

        }

        private void UpdateDishes()
        {
            db = new DbRecipes();
            var idgr = db.Groups.Where(n => n.Group_name == ComboType.Text).Select(x => x.ID).FirstOrDefault();

            RecipesView.ItemsSource = db.Dishes.Where(x => x.Dish_name.Contains(TBoxSearch.Text)).ToList();

            if (ComboType.SelectedIndex > 0)
                RecipesView.ItemsSource = db.Dishes.Where(x => x.ID_Group == ComboType.SelectedIndex).ToList();

            
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e) //открытие блюда в отдельном окне при двойном клике на item
        {
            /*var dish = (sender as TextBlock).Name;
            Application.Current.Resources["TT"] = dish;*/


            //NavigationService.Navigate(new ViewPage()); //открытие рецепта на полную






            if (e.ChangedButton == MouseButton.Left)
            {
                var item = e.Source as ListViewItem;
                ViewPage vp = new ViewPage(item.IsSelected);
                NavigationService.Navigate(new ViewPage(item.DataContext));
            }
        }

    }
}
