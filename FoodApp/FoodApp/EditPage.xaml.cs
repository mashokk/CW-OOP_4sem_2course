using System;
using System.IO;
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
    public enum TableType //Перечисление, где указывается типы таблиц
    {
        DishesTT, IngredientsTT, CompositionTT, ComposTT
    }
    public partial class EditPage : Page
    {
        DbRecipes db; //Для работы с базой
        TableType currentTableType; //Хранит текущую открытую таблицу
        public EditPage()
        {
            InitializeComponent();
            //Инициализация и обновление списка
            db = new DbRecipes();
            currentTableType = TableType.DishesTT;
            RefreshTable(currentTableType);
        }
        //Обновляет таблицу 
        private void RefreshTable(TableType tt)
        {
            db = new DbRecipes(); //Создаём обект базы данных
            switch (tt)
            {
                /*case TableType.IngredientsTT:
                    db.Ingredients.Load();
                    int id1 = db.Ingredients.Max(m => m.ID);
                    BindingList<Ingredients> blIngredient = db.Ingredients.Local.ToBindingList(); //Получаем коллекцию BindingList
                    blIngredient.AddingNew += (sender, e) => e.NewObject = new Ingredients() { ID = id1 + 1, Ingredient_name = "<новый>" }; //Задаем шаблон для новой записи
                    this.ingredientTable.ItemsSource = blIngredient; //Задаем BindingList<> как источник данных
                    break;*/

                case TableType.CompositionTT:
                    db.Dish_Composition.Load();
                    int id2 = db.Dish_Composition.Max(m => m.ID);
                    BindingList<Dish_Composition> blComposition = db.Dish_Composition.Local.ToBindingList();
                    blComposition.AddingNew += (sender, e) => e.NewObject = new Dish_Composition() { ID = id2 + 1, ID_Dish = 0, ID_Ingredient = 0 };
                    this.compositionTable.ItemsSource = blComposition;
                    this.colDish.ItemsSource = db.Dishes.ToArray();
                    this.colIngred.ItemsSource = db.Ingredients.ToArray();
                    break;

                /*case TableType.DishesTT:
                    db.Dishes.Load();
                    int id3 = db.Dishes.Max(m => m.ID); //индекс
                    BindingList<Dishes> blDishes = db.Dishes.Local.ToBindingList();
                    blDishes.AddingNew += (sender, e) => e.NewObject = new Dishes() { ID = id3 + 1, Dish_name = "<новый>", ID_Group = 0, Description = "<описание>", ID_Photo = 0 };
                    this.dishesTable.ItemsSource = blDishes;
                    this.colGroup.ItemsSource = db.Groups.ToArray();
                    break;*/
            }
        }

        //Сохраняет изменения и обновляет таблицу
        private void SaveChanges(TableType tt)
        {
            db.SaveChanges(); //Сохраняем изменения

            DataGrid currTable = null;
            switch (tt)
            {
                /*case TableType.IngredientsTT:
                    currTable = ingredientTable;
                    break;*/
                case TableType.CompositionTT:
                    currTable = compositionTable;
                    break;
                /*case TableType.DishesTT:
                    currTable = dishesTable;
                    break;*/
            }

            int si = currTable.SelectedIndex; //Сохраняем индекс текущей выделенной строки
            RefreshTable(tt); //Обновляем таблицу
            currTable.SelectedIndex = si; //Выделяем строку
        }

        //Удаляет выделенную запись
        private void DeleteRecord(TableType tt)
        {
            switch (tt)
            {
                /*case TableType.IngredientsTT:
                    if (ingredientTable.SelectedItem is Ingredients v && v.Dish_Composition.Count == 0)
                        db.Ingredients.Local.Remove(v);
                    else
                        MessageBox.Show("Данный ингридиент уже содержится в блюдах. Удаление невозможно!",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;*/
                case TableType.CompositionTT:
                    if (compositionTable.SelectedItem is Dish_Composition d)
                        db.Dish_Composition.Local.Remove(d);
                    break;
                /*case TableType.DishesTT:
                    if (dishesTable.SelectedItem is Dishes b)
                        db.Dishes.Local.Remove(b);
                    break;*/
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
                else if (header == "Состав блюда")
                    currentTableType = TableType.CompositionTT;
                else if (header == "Блюда")
                    currentTableType = TableType.DishesTT;

                if (currentTableType != old)
                    RefreshTable(currentTableType);
            }
        }


        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges(currentTableType);
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //Чтобы отменить изменения, мы просто заново загружаем данные с сервера
            RefreshTable(currentTableType);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord(currentTableType);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilterPage());
        }
    }
}
