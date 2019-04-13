using System.Windows;

namespace PupaParserComeback.UI.DialogViews
{
    /// <summary>
    /// Interaction logic for ErrorDialogView.xaml
    /// </summary>
    public partial class ErrorDialogView : Window
    {
        public ErrorDialogView(string errorMessage)
        {
            InitializeComponent();

            error.Text = errorMessage;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
