using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace edytor
{
    public partial class MainWindow : Window
    {
        private Brush selectedColor;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ColorCell_Click(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                selectedColor = border.Background;
            }
        }

        private void GridCell_Click(object sender, MouseButtonEventArgs e)
        {
            if (selectedColor != null)
            {
                var border = sender as Border;
                if (border != null)
                {
                    border.Background = selectedColor;
                }
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var borders = ColorGrid.Children.OfType<Border>().ToList();

            // Sortowanie bąbelkowe na podstawie koloru
            for (int i = 0; i < borders.Count - 1; i++)
            {
                for (int j = 0; j < borders.Count - i - 1; j++)
                {
                    var currentColor = GetColorFromBrush(borders[j].Background);
                    var nextColor = GetColorFromBrush(borders[j + 1].Background);

                    if (currentColor > nextColor)
                    {
                        // Zamiana miejscami kolorów
                        var temp = borders[j].Background;
                        borders[j].Background = borders[j + 1].Background;
                        borders[j + 1].Background = temp;
                    }
                }
            }
        }

        private int GetColorFromBrush(Brush brush)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                var color = solidColorBrush.Color;
                return (color.R << 16) | (color.G << 8) | color.B;
            }
            return 0;
        }
    }
}