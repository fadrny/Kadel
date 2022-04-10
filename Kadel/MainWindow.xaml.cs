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

        bool codeExecRunning = false;
        
        void DrawIt()
        {
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
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = kadelBot.GetDirectionImage();
            DrawIt();
        }

        #region UserControl

        private void btn_krok_Click(object sender, RoutedEventArgs e)
        {
            if ((string)rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Tag == "fill")
                rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = Brushes.Blue;
            else
                rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = Brushes.White;

            Point startPoint = kadelBot.Position;

            switch (kadelBot.Direction)
            {
                case 0:
                    if (kadelBot.Position.Y < 9)
                    {
                        kadelBot.Position = new Point(kadelBot.Position.X, kadelBot.Position.Y + 1);
                        tv_code_output.Text += "OK - Krok vlevo" + Environment.NewLine;
                    }
                    break;
                case 1:
                    if (kadelBot.Position.X > 0)
                    {
                        kadelBot.Position = new Point(kadelBot.Position.X - 1, kadelBot.Position.Y);
                        tv_code_output.Text += "OK - Krok nahoru" + Environment.NewLine;
                    }
                    break;
                case 2:
                    if (kadelBot.Position.Y > 0)
                    {
                        kadelBot.Position = new Point(kadelBot.Position.X, kadelBot.Position.Y - 1);
                        tv_code_output.Text += "OK - Krok vpravo" + Environment.NewLine;
                    }
                    break;
                case 3:
                    if (kadelBot.Position.X < 9)
                    {
                        kadelBot.Position = new Point(kadelBot.Position.X + 1, kadelBot.Position.Y);
                        tv_code_output.Text += "OK - Krok dolu" + Environment.NewLine;
                    }
                    break;
            }

            if (startPoint == kadelBot.Position)
            {
                tv_code_output.Text += "!!! Au, Narazil jsem !!!" + Environment.NewLine;
                if(codeExecRunning)
                {
                    tv_code_output.Text += "### ukončení programu - KaDel narazil: " + Environment.NewLine;
                    codeExecRunning = false;
                }
            }

            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = kadelBot.GetDirectionImage();
            DrawIt();
        }

        private void btn_otoc_Click(object sender, RoutedEventArgs e)
        {
            kadelBot.ChangeDirection();
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = kadelBot.GetDirectionImage();
            tv_code_output.Text += "OK - otoceno" + Environment.NewLine;
            DrawIt();
        }

        private void btn_vypln_Click(object sender, RoutedEventArgs e)
        {
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Tag = "fill";
            tv_code_output.Text += "OK - vyplneno" + Environment.NewLine;
        }

        private void btn_vymaz_Click(object sender, RoutedEventArgs e)
        {
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Tag = "";
            tv_code_output.Text += "OK - vymazano" + Environment.NewLine;
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            kadelBot.Position = new Point(9, 0);
            kadelBot.ResetDirection();
            foreach (Rectangle r in rect)
            {
                r.Tag = "";
                r.Fill = Brushes.White;
            }
            rect[(int)kadelBot.Position.X, (int)kadelBot.Position.Y].Fill = kadelBot.GetDirectionImage();
            DrawIt();
            tv_code_output.Text += "OK - resetovano" + Environment.NewLine;
        }

        #endregion

        #region CodeControl

        private void btn_spustit_kod_Click(object sender, RoutedEventArgs e)
        {
            string[] code = tb_code.Text.Split('\n');
            codeExecRunning = true;

            foreach (string s in code)
            {
                string tmp = s.Trim();
                switch (tmp)
                {
                    case "krok":
                        btn_krok_Click(this, new RoutedEventArgs());
                        break;
                    case "otoc":
                        btn_otoc_Click(this, new RoutedEventArgs());
                        break;
                    case "vypln":
                        btn_vypln_Click(this, new RoutedEventArgs());
                        break;
                    case "vymaz":
                        btn_vymaz_Click(this, new RoutedEventArgs());
                        break;
                    case "reset":
                        btn_reset_Click(this, new RoutedEventArgs());
                        break;
                    case "":
                        break;
                    default:
                        tv_code_output.Text += "### ukončení programu - neznámý příkaz: " + tmp + Environment.NewLine;
                        codeExecRunning = false;
                        break;
                }

                if (!codeExecRunning)
                    break;
            }

            codeExecRunning = false;
        }

        #endregion
    }
}
