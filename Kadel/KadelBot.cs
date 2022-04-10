using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kadel
{
    internal class KadelBot
    {
        private Point _position;

        private int _direction;
        
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public int Direction
        {
            get { return _direction; }
        }

        public KadelBot(Point pozice)
        {
            _position = pozice;
            _direction = 0;
        }

        public void ChangeDirection()
        {
            _direction += 1;
            if (_direction > 3)
                _direction = 0;
        }

        public void ResetDirection()
        {
            _direction = 0;
        }
        
        public ImageBrush GetDirectionImage()
        {
            ImageBrush imgBrush = new ImageBrush();
            
            switch (_direction)
            {      
                case 0:
                    imgBrush.ImageSource = new BitmapImage(new Uri("../../../images/vpravo.png", UriKind.Relative));
                    break;
                case 1:
                    imgBrush.ImageSource = new BitmapImage(new Uri("../../../images/nahoru.png", UriKind.Relative));
                    break;
                case 2:
                    imgBrush.ImageSource = new BitmapImage(new Uri("../../../images/vlevo.png", UriKind.Relative));
                    break;
                case 3:
                    imgBrush.ImageSource = new BitmapImage(new Uri("../../../images/dolu.png", UriKind.Relative));
                    break;
                default:
                    imgBrush.ImageSource = new BitmapImage(new Uri("../../../images/vpravo.png", UriKind.Relative));
                    break;
            }
            return imgBrush;
        }
        
        
    }
}
