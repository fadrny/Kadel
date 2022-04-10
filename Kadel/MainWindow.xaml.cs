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

namespace Kadel
{  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[,] rect = new Rectangle[10, 10];
        KadelBot kadelBot = new KadelBot(new Point(9, 0));
        
        void DrawIt()
        {
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = kadelBot.GetDirectionImage();

            ug_hraci_plocha.Children.Clear();
            foreach (Rectangle r in rect)
            {
                ug_hraci_plocha.Children.Add(r);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            for (int x = 0; x < rect.GetLength(0); x++)
            {
                for (int y = 0; y < rect.GetLength(1); y++)
                {
                    Rectangle r = new Rectangle();
                    r.Stroke = Brushes.Black;
                    

                    rect[x, y] = r;
                    ug_hraci_plocha.Children.Add(r);
                }
            }
        }

        private void btn_krok_Click(object sender, RoutedEventArgs e)
        {
            DrawIt();
        }
    }
}
