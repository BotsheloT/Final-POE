using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Part_1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Main Dictionaries
            areasSwitch.Add("General Knowledge", "000");
            areasSwitch.Add("Philosophy and Psychology", "100");
            areasSwitch.Add("Religion", "200");
            areasSwitch.Add("Social Sciences", "300");
            areasSwitch.Add("Languages", "400");
            areasSwitch.Add("Science", "500");
            areasSwitch.Add("Technology", "600");
            areasSwitch.Add("Arts and Recreation", "700");
            areasSwitch.Add("Literature", "800");
            areasSwitch.Add("History and Geography", "900");

            areas.Add("000", "General Knowledge");
            areas.Add("100", "Philosophy and Psychology");
            areas.Add("200", "Religion");
            areas.Add("300", "Social Sciences");
            areas.Add("400", "Languages");
            areas.Add("500", "Science");
            areas.Add("600", "Technology");
            areas.Add("700", "Arts and Recreation");
            areas.Add("800", "Literature");
            areas.Add("900", "History and Geography");      
        }

        List<string> callNumbs = new List<string>();
        List<string> authors = new List<string>();
        List<string> sortedList = new List<string>();
        List<string> userList = new List<string>();


        //All application collection and variables
        public static Dictionary<string, string> areas = new Dictionary<string, string>();
        public static Dictionary<string, string> areasSwitch = new Dictionary<string, string>();

        public static List<string> ranCallNums = new List<string>();

        public static List<string> ranDescripts = new List<string>();

        public static Random rnd = new Random();
        public static int rand = rnd.Next(0, 9);

        public static List<string> callNums = new List<string>();
        public static List<string> Descripts = new List<string>();

        public static Dictionary<string,string> userAnswers = new Dictionary<string,string>();

        public static Dictionary<string, string> sortedAreas = new Dictionary<string, string>();

        public static int turn = rnd.Next(0, 100);

        public static int rounds = 0;
        public static int points = 0;

        int qPoints = 0;
        int qRounds = 0;

        public static string[] libraryData;

        Library<string> libraryTree = new Library<string>();

        public static int tLevel;
        public static int mLevel;
        public static int bLevel;

        public static List<string> options = new List<string>();
        public static List<string> pAnswers = new List<string>();
        public static List<string> firstQOptions = new List<string>();
        public static List<string> secondQOptions = new List<string>();

        public static string top;
        public static string middle;
        public static string bottom;

        //String for user answer
        public static string fAnswer;
        public static string sAnswer;

        //Method for List
        private void bubbleSort(List<string> stList)
        {
            for (int i = 0; i < stList.Count - 1; i++)
            {   //10, 5, 21, 7, 13
                for (int k = (i + 1); k < stList.Count; k++)
                {
                    if (stList[i].CompareTo(stList[k]) == 1)
                    {
                        string temp = stList[i];
                        stList[i] = stList[k];
                        stList[k] = temp;
                    }
                }
            }
        }

        private void Quizzer()
        {
            firstQOptions.Clear();
            options.Clear();

            tLevel = rnd.Next(0, 9);
            mLevel = rnd.Next(0, 2);
            bLevel = rnd.Next(0, 1);

            foreach (string item in pAnswers)
            {
                options.Add(item);
            }

            top = libraryTree.Root.Children[tLevel].Data;            
            middle = libraryTree.Root.Children[tLevel].Children[mLevel].Data;
            bottom = libraryTree.Root.Children[tLevel].Children[mLevel].Children[bLevel].Data;


            string uQuestion = bottom.Substring(4);

                fAnswer = top;
                sAnswer = middle;

                firstQOptions.Add(top);
                options.Remove(top);

                //
                while (firstQOptions.Count < 4)
                {
                    //Variable for random numbers for 3 other options
                    int otherThree = rnd.Next(0, options.Count);
                try
                {
                    firstQOptions.Add(options[otherThree]);
                }
                catch(Exception e)
                {
                    MessageBox.Show(otherThree.ToString());
                }
                    
                    options.Remove(options[otherThree]);
                }

                //Sorting of options for user
                bubbleSort(firstQOptions);

                //Displaying user options and questions in tab
                txtData.Text = uQuestion;
                foreach (string option in firstQOptions)
                {
                    lstbOut.Items.Add(option);
                }

            lblFeedback.Content = ("Your Points: " +  qPoints + "/" + qRounds);
                
        }

        public void nextLevelQuizzer()
        {
            //Sorting list
            bubbleSort(secondQOptions);

            //First three items in second level answers
            for (int j = 0; j < 3; j++)
            {
                secondQOptions.Add(libraryTree.Root.Children[tLevel].Children[j].Data);
            }

            //Other random numbers
            int lastRan = rnd.Next(0, 9);
            int finalRan = rnd.Next(0, 2);

            //Fourth item in second level answers
            secondQOptions.Add(libraryTree.Root.Children[lastRan].Children[finalRan].Data);

            foreach (string option in secondQOptions)
            {
                lstbOut.Items.Add(option);
            }

            lblFeedback.Content = ("Points: " + qPoints + "/" + qRounds);
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            //string to read data from file
            libraryData = System.IO.File.ReadAllLines(@"C:\Users\lab_services_student\Desktop\Final Submission\library.txt");

            //Populating tree for library data
            libraryTree.Root = new LibraryNode<string>() {Data = "DDS System" };
            libraryTree.Root.Children = new List<LibraryNode<string>>
            {
                new LibraryNode<string>() {Data = libraryData[0], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[10], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[20], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[30], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[40], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[50], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[60], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[70], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[80], Parent = libraryTree.Root},
                new LibraryNode<string>() {Data = libraryData[90], Parent = libraryTree.Root}
            };
            libraryTree.Root.Children[0].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[1], Parent = libraryTree.Root.Children[0]},
                new LibraryNode<string>() {Data = libraryData[4], Parent = libraryTree.Root.Children[0]},
                new LibraryNode<string>() {Data = libraryData[7], Parent = libraryTree.Root.Children[0]}
            };
            libraryTree.Root.Children[0].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[2], Parent = libraryTree.Root.Children[0].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[3], Parent = libraryTree.Root.Children[0].Children[0]}
            };
            libraryTree.Root.Children[0].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[5], Parent = libraryTree.Root.Children[0].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[6], Parent = libraryTree.Root.Children[0].Children[1]}
            };
            libraryTree.Root.Children[0].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[8], Parent = libraryTree.Root.Children[0].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[9], Parent = libraryTree.Root.Children[0].Children[2]}
            };
            libraryTree.Root.Children[1].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[11], Parent = libraryTree.Root.Children[1]},
                new LibraryNode<string>() {Data = libraryData[14], Parent = libraryTree.Root.Children[1]},
                new LibraryNode<string>() {Data = libraryData[17], Parent = libraryTree.Root.Children[1]}
            };
            libraryTree.Root.Children[1].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[12], Parent = libraryTree.Root.Children[1].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[13], Parent = libraryTree.Root.Children[1].Children[0]}
            };
            libraryTree.Root.Children[1].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[15], Parent = libraryTree.Root.Children[1].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[16], Parent = libraryTree.Root.Children[1].Children[1]}
            };
            libraryTree.Root.Children[1].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[18], Parent = libraryTree.Root.Children[1].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[19], Parent = libraryTree.Root.Children[1].Children[2]}
            };
            libraryTree.Root.Children[2].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[21], Parent = libraryTree.Root.Children[2]},
                new LibraryNode<string>() {Data = libraryData[24], Parent = libraryTree.Root.Children[2]},
                new LibraryNode<string>() {Data = libraryData[27], Parent = libraryTree.Root.Children[2]}
            };
            libraryTree.Root.Children[2].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[22], Parent = libraryTree.Root.Children[2].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[23], Parent = libraryTree.Root.Children[2].Children[0]}
            };
            libraryTree.Root.Children[2].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[25], Parent = libraryTree.Root.Children[2].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[26], Parent = libraryTree.Root.Children[2].Children[1]}
            };
            libraryTree.Root.Children[2].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[28], Parent = libraryTree.Root.Children[2].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[29], Parent = libraryTree.Root.Children[2].Children[2]}
            };

            libraryTree.Root.Children[3].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[31], Parent = libraryTree.Root.Children[3]},
                new LibraryNode<string>() {Data = libraryData[34], Parent = libraryTree.Root.Children[3]},
                new LibraryNode<string>() {Data = libraryData[37], Parent = libraryTree.Root.Children[3]}
            };
            libraryTree.Root.Children[3].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[32], Parent = libraryTree.Root.Children[3].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[33], Parent = libraryTree.Root.Children[3].Children[0]}
            };
            libraryTree.Root.Children[3].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[35], Parent = libraryTree.Root.Children[3].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[36], Parent = libraryTree.Root.Children[3].Children[1]}
            };
            libraryTree.Root.Children[3].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[38], Parent = libraryTree.Root.Children[3].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[39], Parent = libraryTree.Root.Children[3].Children[2]}
            };
            libraryTree.Root.Children[4].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[41], Parent = libraryTree.Root.Children[4]},
                new LibraryNode<string>() {Data = libraryData[44], Parent = libraryTree.Root.Children[4]},
                new LibraryNode<string>() {Data = libraryData[47], Parent = libraryTree.Root.Children[4]}
            };
            libraryTree.Root.Children[4].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[42], Parent = libraryTree.Root.Children[4].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[43], Parent = libraryTree.Root.Children[4].Children[0]}
            };
            libraryTree.Root.Children[4].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[45], Parent = libraryTree.Root.Children[4].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[46], Parent = libraryTree.Root.Children[4].Children[2]}
            };
            libraryTree.Root.Children[4].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[48], Parent = libraryTree.Root.Children[4].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[49], Parent = libraryTree.Root.Children[4].Children[2]}
            };
            libraryTree.Root.Children[5].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[51], Parent = libraryTree.Root.Children[5]},
                new LibraryNode<string>() {Data = libraryData[54], Parent = libraryTree.Root.Children[5]},
                new LibraryNode<string>() {Data = libraryData[57], Parent = libraryTree.Root.Children[5]}
            };
            libraryTree.Root.Children[5].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[52], Parent = libraryTree.Root.Children[5].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[53], Parent = libraryTree.Root.Children[5].Children[0]}
            };
            libraryTree.Root.Children[5].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[55], Parent = libraryTree.Root.Children[5].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[56], Parent = libraryTree.Root.Children[5].Children[1]}
            };
            libraryTree.Root.Children[5].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[58], Parent = libraryTree.Root.Children[5].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[59], Parent = libraryTree.Root.Children[5].Children[2]}
            };
            libraryTree.Root.Children[6].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[61], Parent = libraryTree.Root.Children[6]},
                new LibraryNode<string>() {Data = libraryData[64], Parent = libraryTree.Root.Children[6]},
                new LibraryNode<string>() {Data = libraryData[67], Parent = libraryTree.Root.Children[6]}
            };
            libraryTree.Root.Children[6].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[62], Parent = libraryTree.Root.Children[6].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[63], Parent = libraryTree.Root.Children[6].Children[0]}
            };
            libraryTree.Root.Children[6].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[65], Parent = libraryTree.Root.Children[6].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[66], Parent = libraryTree.Root.Children[6].Children[1]}
            };
            libraryTree.Root.Children[6].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[68], Parent = libraryTree.Root.Children[6].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[69], Parent = libraryTree.Root.Children[6].Children[2]}
            };
            libraryTree.Root.Children[7].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[71], Parent = libraryTree.Root.Children[7]},
                new LibraryNode<string>() {Data = libraryData[74], Parent = libraryTree.Root.Children[7]},
                new LibraryNode<string>() {Data = libraryData[77], Parent = libraryTree.Root.Children[7]}
            };
            libraryTree.Root.Children[7].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[72], Parent = libraryTree.Root.Children[7].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[73], Parent = libraryTree.Root.Children[7].Children[0]}
            };
            libraryTree.Root.Children[7].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[75], Parent = libraryTree.Root.Children[7].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[76], Parent = libraryTree.Root.Children[7].Children[1]}
            };
            libraryTree.Root.Children[7].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[78], Parent = libraryTree.Root.Children[7].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[79], Parent = libraryTree.Root.Children[7].Children[2]}
            };
            libraryTree.Root.Children[8].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[81], Parent = libraryTree.Root.Children[8]},
                new LibraryNode<string>() {Data = libraryData[84], Parent = libraryTree.Root.Children[8]},
                new LibraryNode<string>() {Data = libraryData[87], Parent = libraryTree.Root.Children[8]}
            };
            libraryTree.Root.Children[8].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[82], Parent = libraryTree.Root.Children[8].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[83], Parent = libraryTree.Root.Children[8].Children[0]}
            };
            libraryTree.Root.Children[8].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[85], Parent = libraryTree.Root.Children[8].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[86], Parent = libraryTree.Root.Children[8].Children[1]}
            };
            libraryTree.Root.Children[8].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[88], Parent = libraryTree.Root.Children[8].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[89], Parent = libraryTree.Root.Children[8].Children[2]}
            };
            libraryTree.Root.Children[9].Children = new List<LibraryNode<string>>()
            {
                new LibraryNode<string>() {Data = libraryData[91], Parent = libraryTree.Root.Children[9]},
                new LibraryNode<string>() {Data = libraryData[94], Parent = libraryTree.Root.Children[9]},
                new LibraryNode<string>() {Data = libraryData[97], Parent = libraryTree.Root.Children[9]}
            };
            libraryTree.Root.Children[9].Children[0].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[92], Parent = libraryTree.Root.Children[9].Children[0]},
                 new LibraryNode<string>() {Data = libraryData[93], Parent = libraryTree.Root.Children[9].Children[0]}
            };
            libraryTree.Root.Children[9].Children[1].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[95], Parent = libraryTree.Root.Children[9].Children[1]},
                 new LibraryNode<string>() {Data = libraryData[96], Parent = libraryTree.Root.Children[9].Children[1]}
            };
            libraryTree.Root.Children[9].Children[2].Children = new List<LibraryNode<string>>
            {
                 new LibraryNode<string>() {Data = libraryData[98], Parent = libraryTree.Root.Children[9].Children[2]},
                 new LibraryNode<string>() {Data = libraryData[99], Parent = libraryTree.Root.Children[9].Children[2]}
            };

            for (int  i = 0; i < 10; i++)
            {
                pAnswers.Add(libraryTree.Root.Children[i].Data);
            }

            Quizzer();

            //Tab switch
            Menu.SelectedIndex = 3;


        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();

            userList.Clear();
            sortedList.Clear();
            callNumbs.Clear();
            lsbResult.Items.Clear();
            lsbOutput.Items.Clear();          
            
            authors.Add("GOL");
            authors.Add("CHA");
            authors.Add("JAN");
            authors.Add("WIL");
            authors.Add("HOM");
            authors.Add("SOC");
            authors.Add("ARI");
            authors.Add("MAR");
            authors.Add("FRI");
            authors.Add("MAC");
            authors.Add("SAL");
            authors.Add("KAF");
            authors.Add("GAR");
            authors.Add("WES");
            authors.Add("MAL");
            authors.Add("CAR");
            authors.Add("MIL");
            authors.Add("TRA");
            authors.Add("JEN");
            authors.Add("MIC");
            authors.Add("GLO");
            authors.Add("OKA");
           
            for (int i =0; i < 10; i++)
            {
                int ran = rnd.Next(1, 99);

                string rd = ran.ToString();

                if (rd.Length == 2)
                {
                    string temporary = rd;
                    rd = "0" + temporary; 
                } else if (rd.Length == 1)
                {
                    string temp = rd;
                    rd = "00" + temp;
                }

                callNumbs.Add(rd + authors[rnd.Next(1, 22)]);
            }
   
            foreach(string item in callNumbs)
            {
                lsbOutput.Items.Add(item);
            }

            //Sorting of Call Numbers
            sortedList = callNumbs;
            bubbleSort(sortedList);
            

            //Disabling other option and switching to Replace books Feature
            
            Menu.SelectedIndex = 1;
        }

        private void btnIdentify_Click(object sender, RoutedEventArgs e)
        {
            //Disabling other option and switching to Identifying Areas Feature
            
            Menu.SelectedIndex = 2;
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            //Getting all items in the listbox and adding them to the user List
            for (int i = 0; i < lsbResult.Items.Count; i++)
            {
                string item = lsbResult.Items[i].ToString();

                userList.Add(item);
            }

            int count = 0;
            if (userList.Count == sortedList.Count)
            {
                for (int i = 0; i < sortedList.Count; i++)
                {
                    if (sortedList[i] == userList[i])
                    {
                        count = count +1;
                    }
                }
            }

            if (count == sortedList.Count)
            {
                MessageBox.Show("Right");
            }
            else
            {
                MessageBox.Show("Not Right", "Student Application", 
                MessageBoxButton.OK, MessageBoxImage.Error);          
            }
        }

        private void lstUnsorted_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, lsbOutput.SelectedItem.ToString(), DragDropEffects.Move);
            }
     
        }

        private void lstSorted_Drop(object sender, DragEventArgs e)
        {
            var myObj = e.Data.GetData(DataFormats.Text);
            lsbOutput.Items.Remove(lsbOutput.SelectedItem);
            lsbResult.Items.Add(myObj);

            if (lsbResult.Items.Contains(lsbResult.SelectedItem))
            {
                lsbResult.Items.Remove(lsbOutput.SelectedItem);
                lsbResult.Items.Remove(lsbResult.SelectedItem);
            }

            if (lsbResult.Items.Count > 0)
            {
                btnCheck.IsEnabled = true;
            }
        }

        private void lstSorted_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, lsbResult.SelectedItem.ToString(), DragDropEffects.Move);
            }

        }

        private void lstUnsorted_Drop(object sender, DragEventArgs e)
        {
            var myObj = e.Data.GetData(DataFormats.Text);
            lsbResult.Items.Remove(lsbResult.SelectedItem);
            lsbOutput.Items.Add(myObj);

            if (lsbOutput.Items.Contains(lsbOutput.SelectedItem))
            {
                lsbOutput.Items.Remove(lsbResult.SelectedItem);
                lsbOutput.Items.Remove(lsbOutput.SelectedItem);
            }



            if (lsbResult.Items.Count < 1)
            {
                btnCheck.IsEnabled = false;
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            rounds++;

            //Clearing Application Data
            callNums.Clear();
            Descripts.Clear();
            ranCallNums.Clear();
            ranDescripts.Clear();
            userAnswers.Clear();
      
            lstCallNums.Items.Clear();
            lstDescriptions.Items.Clear();
            lstResult.Items.Clear();

            //Pupulating the lists
            foreach (string keys in areas.Keys)
            {
                callNums.Add(keys);
            }

            foreach (string vals in areas.Values)
            {
                Descripts.Add(vals);
            }
 
                int randomz = rnd.Next(0, callNums.Count);
                int randz;
                int counter = 1;
                int count = 1;
                while (counter <= 4)
                {
                    randz = rnd.Next(0, callNums.Count);
                    ranCallNums.Add(callNums[randz]);
                    callNums.Remove(callNums[randz]);
                    ranDescripts.Add(Descripts[randz]);
                    Descripts.Remove(Descripts[randz]);
                    counter++;
                }

                while (count <= 3)
                {
                    randz = rnd.Next(0, Descripts.Count-1);
                    Descripts.Remove(Descripts[randz]);
                    ranDescripts.Add(Descripts[randz]);
                    count++;
                }

                foreach (string calls in ranCallNums)
                {
                    lstCallNums.Items.Add(calls);
                }

            foreach (string desc in ranDescripts)
            {
                lstDescriptions.Items.Add(desc);
            }

            
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            lstResult.Items.Add(lstCallNums.SelectedItem + "-" + lstDescriptions.SelectedItem);
            lstCallNums.Items.Remove(lstCallNums.SelectedItem);
            lstDescriptions.Items.Remove(lstDescriptions.SelectedItem);
        }

        private void btnChecker_Click(object sender, RoutedEventArgs e)
        {
            //Calculating points and comparing user answers with sorted list
            if (lstResult.Items.Count == 4)
            {
                lstResult.Items.Add("==========================================");
                lstResult.Items.Add("Right Answers:");

                for (int i = 0; i < 4; i++)
                {
                    string item = lstResult.Items[i].ToString();
                    int index = item.IndexOf("-");
                    string calls = Convert.ToString(item.Substring(0,index));
                    string descr = item.Substring(index + 1);

                    userAnswers.Add(calls, descr);
                }

                foreach (KeyValuePair<string, string> stuff in userAnswers)
                {

                    if ((areas.TryGetValue(stuff.Key, out string value) && (value.Equals(stuff.Value))))
                    {
                        points++;
                        lstResult.Items.Add(stuff.Key + "-" + value);

                    }
                    else
                    {
                        MessageBox.Show("Not all answers were right");
                        lstResult.Items.Add(stuff.Key + "-" + value);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please match all the columns");
            }
        }

        private void btnResullts_Click(object sender, RoutedEventArgs e)
        {
            lstResult.Items.Add("=========================================");
            int total = rounds * 4;
            lstResult.Items.Add("Total points: " +  Convert.ToString( points) + " / " + Convert.ToString( total));
        }

        private void btnChecking_Click(object sender, RoutedEventArgs e)
        {
            if (qRounds <= 19)
            {
                if (lstbOut.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select an option!");
                }
                else
                {
                    if (lstbOut.Items[0] == firstQOptions[0])
                    {
                        if (lstbOut.SelectedItem == fAnswer)
                        {
                            qPoints++;
                            qRounds++;
                            lstbOut.Items.Clear();
                            nextLevelQuizzer();
                        }
                        else
                        {
                            MessageBox.Show("Wrong answer kiddo!");
                            qRounds++;
                            lstbOut.Items.Clear();
                            txtData.Clear();
                            Quizzer();
                        }
                    }
                    else if (lstbOut.Items[0] == secondQOptions[0])
                    {
                        if (lstbOut.SelectedItem == sAnswer)
                        {
                            qPoints++;
                            qRounds++;
                            lstbOut.Items.Clear();
                            txtData.Clear();
                            secondQOptions.Clear();
                            firstQOptions.Clear();
                            Quizzer();
                        }
                        else
                        {
                            MessageBox.Show("Wrong answer kiddo!");
                            qRounds++;
                            lstbOut.Items.Clear();
                            txtData.Clear();
                            secondQOptions.Clear();
                            Quizzer();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You got " + qPoints + " answers correct out of "+ qRounds);
                this.Close();
            }

        }

     
    }
}
