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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChooseAHusband
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> images = new List<string>();

        //storing NOT picked images (the opposit of clicked on)
        List<string> notPickedImages = new List<string>();

        int CharactersLeft;

        public MainWindow()
        {
            InitializeComponent();


            images.Add("Sebastian Stan");
            images.Add("Brad Pitt");
            images.Add("Timothee Chalomet");
            images.Add("Robert Dawny");


            // initializing at first loading of the app with the first elements
            TextBlock_1.Text = images[0];
            TextBlock_2.Text = images[1];


            
             
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {  

            string newOppositeString = RandomImageChooser(TextBlock_1.Text, TextBlock_2.Text);

            if (newOppositeString != "0") TextBlock_2.Text = newOppositeString;

             
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
  
            string newOppositeString = RandomImageChooser(TextBlock_2.Text, TextBlock_1.Text);

            if (newOppositeString != "0") TextBlock_1.Text = newOppositeString;

            
        }



        public string RandomImageChooser(string textblock_1, string textblock_2)
        {
            notPickedImages.Add(textblock_2);

            CharactersLeft = images.Count() - notPickedImages.Count();
            ImagesLeft_TextBlock.Text = CharactersLeft.ToString();

            // switch the opposite object to the random element in the List

            Random random = new Random();    // create new random object
        p: int index = random.Next(0, images.Count);    //create an integer whuch will contain a randomly picked int from the list



            if (notPickedImages.Count == images.Count - 1)  //if NotPicked List is Almost full (all the objects were passed BUT ONE) , then our pick is made
            {
                if (MessageBox.Show($"Your pick is {textblock_1}", "The end", MessageBoxButton.YesNo) == MessageBoxResult.Yes) MessageBox.Show("Congrats"); else MessageBox.Show("One more time then");

                //back to basics as we`ve made the last choice
                TextBlock_1.Text = images[0];
                TextBlock_2.Text = images[1];
                notPickedImages.Clear();

                return "0";
            }

            else if (images[index] != textblock_1 && notPickedImages.All(x => x != images[index]))
            {
                textblock_2 = images[index];   //if the random obj != the obj that we picked  AND  theres no match between the random obj and ALL objects from the notPicked list objects   ONLY THEN we put the random object on the screen
                return textblock_2;
            }

            else goto p;  // if none of the conditions were matched we go back and pick a new random object
             
           
        }

    }
}
