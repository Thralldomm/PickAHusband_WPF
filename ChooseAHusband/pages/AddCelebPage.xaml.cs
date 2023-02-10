using ChooseAHusband.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace ChooseAHusband.pages
{
    /// <summary>
    /// Логика взаимодействия для AddCelebPage.xaml
    /// </summary>
    public partial class AddCelebPage : Page
    {
        CelebsInfo celeb = new CelebsInfo();

        MainWindow mainWindow;
        public AddCelebPage(MainWindow mainWindow)
        {
            InitializeComponent();


            MainGrid.DataContext = celeb;
            this.mainWindow = mainWindow;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder(); //хранит ошибки
            errors = null;

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
                errors.AppendLine("Insert a Name");
            if (Int32.Parse(AgeTextBox.Text) < 0 || Int32.Parse(AgeTextBox.Text) > 100 || string.IsNullOrWhiteSpace(AgeTextBox.Text))
                errors.AppendLine("Insert a correct age");


            if (errors != null)
            {
                MessageBox.Show(errors.ToString());
            }
            else
            {
                using (PickAtypeDbContext db = new PickAtypeDbContext())
                {

                    celeb.CelebName = NameTextBox.Text;
                    celeb.CelebAge = Int32.Parse(AgeTextBox.Text);
                    celeb.CelebHeight = HeightTextBox.Text;
                    celeb.CelebWeight = WeightTextBox.Text;
                    celeb.CelebDescription = DescriptionTextBox.Text;
                    celeb.CelebMeaningOfChoice = MeaningTextBox.Text;
                    celeb.Photo = CelebImage.Source.ToString();

                    int latestkod = db.CelebsInfos.OrderByDescending(b => b.CelebrityKod).Select(b => b.CelebrityKod).First();

                    celeb.CelebrityKod = (latestkod + 1);

                    
                    try
                    {
                        db.CelebsInfos.Add(celeb);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}");
                    }


                }

                mainWindow.MainFrame.Navigate(new MainPage());

            }
        }

        private void OpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                if ((myStream = dlg.OpenFile()) != null)
                {
                    string strfilename = dlg.FileName;
                    string filetext = File.ReadAllText(strfilename);

                    dlg.DefaultExt = ".png";
                    dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    dlg.Title = "Open Image";
                    dlg.InitialDirectory = @"C:\Users\123\";

                    BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                    CelebImage.Source = image;

                }
                myStream.Dispose();

            }
        }
    }
}
