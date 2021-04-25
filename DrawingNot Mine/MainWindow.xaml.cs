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

namespace FractalTree
{

    public partial class MainWindow : Window
    {

        private int II = 0;
        private int i = 0;
        private int obj_num = 0;
        ScaleTransform st = new ScaleTransform();
        public MainWindow()
        {
            InitializeComponent();

            Point center;
            center.X = Window.Height / 2;
            center.Y = Window.Width / 2;

            canvas1.RenderTransform = st;
            canvas1.MouseWheel += (sender, e) =>
            {
                if (e.Delta > 0)
                {
                    st.ScaleX *= 1.1;
                    st.ScaleY *= 1.1;
                }
                else
                {
                    st.ScaleX /= 1.1;
                    st.ScaleY /= 1.1;
                }


            };

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();
            tbLabel.Text = "";
            i = 0;
            II = 1;
            CompositionTarget.Rendering += StartAnimation;
        }



        private void StartAnimation(object sender, EventArgs e)
        {
            obj_num = 0;
            i += 1;
            if (i % 10 == 0)
            {
                DrawBinaryTree(canvas1, II, new Point(canvas1.Width / 2, 0.83 * canvas1.Height), 0.2 * canvas1.Width, -Math.PI / 2, 0.002 * canvas1.Width);
                if (II < 10)
                {
                    tbLabel.Foreground = Brushes.Black;
                    string str = "drawn lines: " + obj_num.ToString();
                    tbLabel.Text = str;
                }
                if (II >= 10)
                {
                    tbLabel.Foreground = Brushes.Red;
                    string str = "drawn lines: " + obj_num.ToString();
                    tbLabel.Text = str;
                }

                II += 1;

                if (II > 13)
                {
                    tbLabel.Foreground = Brushes.Red;
                    tbLabel.Text = "drawn lines: " + (obj_num * 2).ToString();

                    CompositionTarget.Rendering -=
                    StartAnimation;
                }
            }
        }

        private void DrawBinaryTree(Canvas canvas, int depth, Point pt, double length, double theta, double thiccness)
        {
            double lengthScale = Convert.ToDouble(delta.Text);
            double thiccnesScale = Convert.ToDouble(thiccnessss.Text);
            double deltaTheta = Convert.ToDouble(lenght.Text);
            double x1 = pt.X + length * Math.Cos(theta);
            double y1 = pt.Y + length * Math.Sin(theta);
            Line line = new Line();
            line.Stroke = Brushes.LightGreen;
            line.StrokeThickness = thiccness;
            line.X1 = pt.X;
            line.Y1 = pt.Y;
            line.X2 = x1;
            line.Y2 = y1;
            canvas.Children.Add(line);
            obj_num++;

            if (depth > 1)
            {
                DrawBinaryTree(canvas, depth - 1, new Point(x1, y1), length * lengthScale, theta + deltaTheta, thiccnesScale * thiccness);
                DrawBinaryTree(canvas, depth - 1, new Point(x1, y1), length * lengthScale, theta - deltaTheta, thiccnesScale * thiccness);
                obj_num++;
            }
            else
                return;
        }

    } 
}
