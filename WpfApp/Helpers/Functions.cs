using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp.Helpers
{
    public class Functions
    {
        public static String way_file;
        public static int stat_u_dev;
        public static bool u_view = false;
        public static int id_current_user;
        public static int num_cl;
        BitmapImage BitObj;
        public static string ZapPP(int? f)
        {
            if (f == null)
            {
                return string.Empty;
            }
            else
                return Convert.ToString(f);

        }
        public static string Nazv(string s)
        {
            if (s == null)
                return string.Empty;
            else
                return s;
        }

        public static void close_mp()
        {



        }

        //public static void Zap(ComboBox cmbx, int? id)
        //{
        //    if (id == null)
        //        cmbx.SelectedIndex = -1;
        //    else
        //    {
        //        cmbx.SelectedValue = id;
        //    }
        //}

        public static string getFileExtension(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);
        }
        public static string getFileName(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(@"\") + 1);
        }

        public static void FileDialog(String filter, String defex, String tit)
        {
            OpenFileDialog named = new OpenFileDialog();

            named.InitialDirectory = @"C:\";
            named.Title = tit;//"Выберите документ";

            named.CheckFileExists = true;
            named.CheckPathExists = true;

            named.DefaultExt = defex;  //"pdf";
            named.Filter = filter; //"pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            named.FilterIndex = 2;
            named.RestoreDirectory = true;

            named.ReadOnlyChecked = true;
            named.ShowReadOnly = true;
            if (named.ShowDialog() == true)
            {
                way_file = named.FileName;
            }
        }


        public static void DounloadIMG(String filter, String defex, String tit, Image img_grid)
        {
            OpenFileDialog named = new OpenFileDialog();

            named.InitialDirectory = @"C:\";
            named.Title = tit;//"Выберите документ";

            named.CheckFileExists = true;
            named.CheckPathExists = true;

            named.DefaultExt = defex;  //"pdf";
            named.Filter = filter; //"pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            named.FilterIndex = 2;
            named.RestoreDirectory = true;

            named.ReadOnlyChecked = true;
            named.ShowReadOnly = true;
            try
            {
                if (named.ShowDialog() == true)
                {
                    img_grid.Source = new BitmapImage(new Uri(named.FileName));
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат файла!");
                return;
            }
        }


        public static string ZapF(float? f)
        {
            if (f == null)
            {
                return string.Empty;
            }
            else
                return Convert.ToString(f);
        }

        //public static void SensorError_TextBlock_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox s = sender as TextBox;
        //    try
        //    {
        //        s.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFABADB3"));
        //        if (!string.IsNullOrEmpty(s.Text))
        //        {
        //            Double.Parse(s.Text);

        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Значение должно быть числовым", "Ошибка", MessageBoxButton.OK);
        //        s.BorderBrush = Brushes.Red;
        //    }
        //}

        public static void save_img_func(String name_img, BitmapImage BitObj)
        {


            JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
            jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(BitObj));

            string fileName = @name_img;
            if (File.Exists(fileName))
                File.Delete(fileName);

            FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
            jpegBitmapEncoder.Save(fileStream);
            fileStream.Close();

        }
    }
}
