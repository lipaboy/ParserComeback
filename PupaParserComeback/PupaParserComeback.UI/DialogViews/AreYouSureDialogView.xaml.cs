using System.Windows;

namespace PupaParserComeback.UI.DialogViews
{
    /// <summary>
    /// Interaction logic for AreYouSureDialogView.xaml
    /// </summary>
    public partial class AreYouSureDialogView : Window
    {
        public AreYouSureDialogView()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
