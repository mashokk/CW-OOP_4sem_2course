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
using System.Windows.Shapes;

namespace FoodApp
{
    /// <summary>
    /// Логика взаимодействия для InsertWindow.xaml
    /// </summary>
    public partial class InsertWindow : Window
    {
        DbRecipes db = new DbRecipes();
        public InsertWindow()
        {
            InitializeComponent();
            List<Ingredients> ingr = db.Ingredients.ToList();
            comboBox.ItemsSource = ingr;
            comboBox.SelectedValuePath = "ID";
            comboBox.DisplayMemberPath = "Ingredient_name";
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            string ig = comboBox.Text;
            var ingrd = db.Ingredients.Where(n => n.Ingredient_name == ig).Select(x => x.ID).FirstOrDefault();
            Dish_Composition ingredients = new Dish_Composition()
            {
                ID_Ingredient = ingrd
            };
            db.Dish_Composition.Add(ingredients);
            db.SaveChanges();
            AddPage.datagrid.ItemsSource = db.Dish_Composition.ToList();
            this.Hide();
        }
    }
}
