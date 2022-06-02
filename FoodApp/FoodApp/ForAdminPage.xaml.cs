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
    public partial class ForAdminPage : Page
    {
        DbRecipes db; //Для работы с базой
        TableType currentTableType; //Хранит текущую открытую таблицу
        public ForAdminPage()
        {
            InitializeComponent();
            
            db = new DbRecipes();
            db.Dishes.Load();
            RefreshTable(currentTableType);
        }
        private void RefreshTable(TableType tt) //Обновляет таблицу 
        {
            db = new DbRecipes(); //Создаём обект базы данных
            switch (tt)
            {
                case TableType.IngredientsTT:
                    db.Ingredients.Load();
                    int id1 = db.Ingredients.Max(m => m.ID);
                    BindingList<Ingredients> blIngredient = db.Ingredients.Local.ToBindingList(); //Получаем коллекцию BindingList
                    blIngredient.AddingNew += (sender, e) => e.NewObject = new Ingredients() { ID = id1 + 1, Ingredient_name = "<новый>" }; //Задаем шаблон для новой записи
                    this.ingredientTable.ItemsSource = blIngredient; //Задаем BindingList<> как источник данных
                    break;

                case TableType.UsersTT:
                    db.Users.Load();
                    int id2 = db.Users.Max(m => m.ID);
                    BindingList<Users> blUsers = db.Users.Local.ToBindingList();
                    blUsers.AddingNew += (sender, e) => e.NewObject = new Users() { ID = id2 + 1, Username = "<новый>", Login = "<new>", Password = "<new>" };
                    this.usersTable.ItemsSource = blUsers;
                    break;
            }
        }
        //Сохраняет изменения и обновляет таблицу
        private void SaveChanges(TableType tt)
        {
            db.SaveChanges(); //Сохраняем изменения

            DataGrid currTable = null;
            switch (tt)
            {
                case TableType.IngredientsTT:
                    currTable = ingredientTable;
                    break;
                case TableType.UsersTT:
                    currTable = usersTable;
                    break;
            }

            int si = currTable.SelectedIndex; //Сохраняем индекс текущей выделенной строки
            RefreshTable(tt); //Обновляем таблицу
            currTable.SelectedIndex = si; //Выделяем строку
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DbRecipes.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                RecipesView.ItemsSource = DbRecipes.GetContext().Dishes.ToList();
            }
        }
        //Удаляет выделенную запись
        private void DeleteRecord(TableType tt)
        {
            switch (tt)
            {
                case TableType.IngredientsTT:
                    if (ingredientTable.SelectedItem is Ingredients v && v.Dish_Composition.Count == 0)
                        db.Ingredients.Local.Remove(v);
                    else
                        MessageBox.Show("Данный ингридиент уже содержится в блюдах. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;

                case TableType.UsersTT:
                    if (usersTable.SelectedItem is Users d)
                        db.Users.Local.Remove(d);
                    break;
            }
        }
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem ti)
            {
                TableType old = currentTableType;
                string header = ti.Header.ToString();
                if (header == "Ингридиенты")
                    currentTableType = TableType.IngredientsTT;
                else if (header == "Пользователи")
                    currentTableType = TableType.UsersTT;

                if (currentTableType != old)
                    RefreshTable(currentTableType);
            }
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges(currentTableType);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord(currentTableType);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilterPage());
        }

        private void DishDelete_Click(object sender, RoutedEventArgs e)
        {
                Dishes dish = db.Dishes.Where(o => o.ID == RecipesView.SelectedIndex).FirstOrDefault();
                foreach (Dish_Composition item in RecipesView.Items)
                {
                    Dish_Composition items = db.Dish_Composition.Where(y => y.ID_Dish == RecipesView.SelectedIndex).FirstOrDefault();
                    db.Dish_Composition.Remove(items);
                }
                db.Dishes.Remove(dish);
                db.SaveChanges();
        }
    }
}
