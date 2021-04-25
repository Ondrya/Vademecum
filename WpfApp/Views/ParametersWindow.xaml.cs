using System.Windows;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        public ParametersWindow()
        {
            InitializeComponent();
        }

        private void Producer_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamProducer();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Enviroment_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamEnviroment();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void BuiltTech_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamBuiltTech();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Kind_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamKind();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Literature_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamLiterature();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Type_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamType();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void ControlType_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamControlType();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void SensElement_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamSensElement();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Measure_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamMeasure();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void Function_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamFunction();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void MeasureProccessing_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamMeasureProccessing();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }

        private void MeasureDims_Click(object sender, RoutedEventArgs e)
        {
            var popUpWindow = new ParamMeasureDims();
            popUpWindow.Show();
            popUpWindow.Owner = this;
        }
    }
}
