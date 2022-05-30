using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace FoodApp
{
    public partial class AddPage : Page
    {
        DbRecipes db = new DbRecipes(); //Для работы с базой
        private Dishes _currentDish = new Dishes();

        public AddPage()
        {
            InitializeComponent();
            DataContext = _currentDish;

            DishName.MaxLength = 35; //ограничение на кол-во символов

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
            dlg.Filter = "Image files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                FileNameLabel.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ImageViewer1.Source = bitmap;
            }
        }


        private void AddSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //инициализация пути к изображению
            string name = DishName.Text;
            string description = DescriptionText.Text;
            string group = ComboGroups.Text;
            string photo = FileNameLabel.Text;
            int idgr = db.Groups.Where(n => n.Group_name == group).Select(x => x.ID).FirstOrDefault();
            /*var idph = db.Photos.Where(o => o.URL_Photo == photo).Select(y => y.ID).FirstOrDefault();*/

            int id = db.Dishes.Max(m => m.ID);
            int idphoto = db.Photos.Max(k => k.ID);

            var iddsh = db.Dish_Composition.Where(w => w.ID_Dish == id).Select(z => z.ID).FirstOrDefault();

            try
            {


                if (!string.IsNullOrEmpty(this.FileNameLabel.Text.ToString()))
                {
                    if (!string.IsNullOrEmpty(this.DishName.Text.ToString()) && !string.IsNullOrEmpty(this.DescriptionText.Text.ToString()))
                    {
                        Photos newphoto = new Photos
                        {
                            ID = idphoto + 1,
                            URL_Photo = photo
                        };
                        db.Photos.Add(newphoto);
                        db.SaveChanges();

                        var idph = db.Photos.Where(o => o.URL_Photo == photo).Select(y => y.ID).FirstOrDefault();

                        Dishes newdish = new Dishes
                        {
                            Dish_name = name,
                            Description = description,
                            ID_Group = idgr,
                            ID_Photo = idph,
                            ID = id + 1
                        };
                        db.Dishes.Add(newdish);
                        db.SaveChanges();
                        Dishes user = db.Dishes.FirstOrDefault((u) => u.Dish_name == name);

                        System.Windows.MessageBox.Show("Рецепт успешно добавлен", $"Готово");
                        NavigationService.Navigate(new EditPage());
                    }

                    else
                    {
                        System.Windows.MessageBox.Show("Текстовые поля не должны быть пустыми!", $"Ошибка!");
                    }
                }
                else
                {
                    lb1.Visibility = Visibility.Visible;
                    tb1.Visibility = Visibility.Visible;
                }
                


                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FilterPage());
        }

        private void ComboGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NoPhoto_Click(object sender, RoutedEventArgs e)
        {
            string nophoto = db.Photos.Where(k => k.ID == 0).Select(y => y.URL_Photo).FirstOrDefault();
            string selectedFileName = nophoto;
            FileNameLabel.Text = selectedFileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            ImageViewer1.Source = bitmap;
        }
    }
}
