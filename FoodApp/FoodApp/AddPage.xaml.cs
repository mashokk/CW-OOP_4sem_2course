﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodApp
{
    public partial class AddPage : Page
    {
        DbRecipes db; //Для работы с базой
        private Dishes _currentDish = new Dishes();
        public AddPage()
        {
            InitializeComponent();
            DataContext = _currentDish;

            db = new DbRecipes();
            List<Groups> gr = db.Groups.ToList();
            ComboGroups.ItemsSource = gr;
            ComboGroups.SelectedValuePath = "ID";
            ComboGroups.DisplayMemberPath = "Group_name";
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            db = new DbRecipes();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }

        private void AddSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = DishName.Text;
            string description = DescriptionText.Text;
            string group = ComboGroups.Text;
            var idgr = db.Groups.Where(n => n.Group_name == group).Select(x => x.ID);
            int idgroup = Convert.ToInt32(idgr);
            int id = db.Dishes.Max(m => m.ID);
            try
            {
                Dishes newdish = new Dishes
                {
                    Dish_name = name,
                    Description = description,
                    ID_Group = idgroup,
                    ID = id + 1
                };
                //this.ComboGroups.ItemsSource = db.Groups.ToArray();
                db.Dishes.Add(newdish);
                db.SaveChanges();
                Dishes user = db.Dishes.FirstOrDefault((u) => u.Dish_name == name);
                System.Windows.MessageBox.Show("Рецепт успешно добавлен", $"Готово");
                NavigationService.Navigate(new FilterPage());
            }
            catch
            {
                System.Windows.MessageBox.Show("Что-то пошло не так!", $"Ошибка");
            }




            /*StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentDish.Dish_name))
                errors.AppendLine("Укажите название блюда!");
            if (_currentDish.Groups.Group_name == null)
                errors.AppendLine("Выберите группу блюда!");
            if (string.IsNullOrEmpty(_currentDish.Description))
                errors.AppendLine("Укажите описание рецепта!");

            if (errors.Length > 0)
            {
                System.Windows.MessageBox.Show(errors.ToString()); //???
                return;
            }

            if (_currentDish.ID == 0)
                DbRecipes.GetContext().Dishes.Add(_currentDish);

            try
            {
                DbRecipes.GetContext().SaveChanges();
                System.Windows.MessageBox.Show("Информация сохранена!");
                NavigationService.Navigate(new FilterPage());
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }*/
        }
    }
}
