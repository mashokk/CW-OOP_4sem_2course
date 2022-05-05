﻿using System;
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
            RecipesView.ItemsSource = RV;

        }


        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPage());
        }
    }
}
