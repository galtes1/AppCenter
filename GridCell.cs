using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AppCenter
{
    class GridCell : Grid
    {
        private Rectangle rectangle;

        public GridCell()
        {
            rectangle = new Rectangle()
            {
                Fill = Brushes.Aqua,
                Stretch = Stretch.Fill

            };
            Children.Add(rectangle);
        }

        public void SetState(CellState state)
        {
            LinearGradientBrush myBrush = new LinearGradientBrush();
            myBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.0));
            myBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.5));
            myBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));

            switch (state)
            {
                case CellState.Empty:
                    rectangle.Fill = Brushes.GreenYellow;
                    break;
                case CellState.Snake:
                    rectangle.Fill = Brushes.MediumPurple;
                    break;
                case CellState.Apple:
                    rectangle.Fill = myBrush;
                    ;
                    break;
            }
        }
    }
}

