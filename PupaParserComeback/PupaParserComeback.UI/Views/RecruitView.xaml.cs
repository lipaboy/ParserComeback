using PupaParserComeback.Presentation.ViewModels;
using System.Windows;

namespace PupaParserComeback.UI.Views
{
    /// <summary>
    /// Interaction logic for RecruitView.xaml
    /// </summary>
    public partial class RecruitView : Window
    {
        public RecruitView(RecruitViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
