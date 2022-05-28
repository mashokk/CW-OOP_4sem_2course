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
    public enum TableType //Перечисление, где указывается тип таблицы
    {
        CompositionTT
    }
    public partial class EditPage : Page
    {
        DbRecipes db; //Для работы с базой
        TableType currentTableType; //Хранит текущую открытую таблицу
        public EditPage()
        {
            InitializeComponent();
            
            db = new DbRecipes();
            RefreshTable(currentTableType);
        }
        
        private void RefreshTable(TableType tt) //Обновляет таблицу 
        {
            db = new DbRecipes(); //Создаём обект базы данных
            switch (tt)
            {
                case TableType.CompositionTT:
                    db.Dish_Composition.Load();
                    int id2 = db.Dish_Composition.Max(m => m.ID);
                    BindingList<Dish_Composition> blComposition = db.Dish_Composition.Local.ToBindingList();
                    blComposition.AddingNew += (sender, e) => e.NewObject = new Dish_Composition() { ID = id2 + 1, ID_Dish = 0, ID_Ingredient = 0 };
                    this.compositionTable.ItemsSource = blComposition;
                    this.colDish.ItemsSource = db.Dishes.ToArray();
                    this.colIngred.ItemsSource = db.Ingredients.ToArray();
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
                case TableType.CompositionTT:
                    currTable = compositionTable;
                    break;
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
                case TableType.CompositionTT:
                    if (compositionTable.SelectedItem is Dish_Composition d)
                        db.Dish_Composition.Local.Remove(d);
                    break;
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
    }
}
