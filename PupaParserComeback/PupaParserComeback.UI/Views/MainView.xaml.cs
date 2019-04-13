using PupaParserComeback.Presentation.Models;
using PupaParserComeback.Presentation.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PupaParserComeback.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            HideLoaders();
            
            SubscribeLoaders(viewModel);
            SubscribeRecruitDoubleClick(viewModel);

            DataContext = viewModel;
        }

        private void HideLoaders()
        {
            importLoader.Visibility = Visibility.Hidden;
            updateLoader.Visibility = Visibility.Hidden;
            exportRecruitLoader.Visibility = Visibility.Hidden;
            exportTableRecruitLoader.Visibility = Visibility.Hidden;
        }

        private void SubscribeLoaders(MainViewModel viewModel)
        {
            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(viewModel.IsImportingRecruits))
                {
                    importLoader.Visibility = viewModel.IsImportingRecruits
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }

                if (e.PropertyName == nameof(viewModel.IsUpdatingRecruitShortUIModels))
                {
                    updateLoader.Visibility = viewModel.IsUpdatingRecruitShortUIModels
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }

                if (e.PropertyName == nameof(viewModel.IsExportRecruit))
                {
                    exportRecruitLoader.Visibility = viewModel.IsExportRecruit
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }

                if (e.PropertyName == nameof(viewModel.IsExportTableRecruit))
                {
                    exportTableRecruitLoader.Visibility = viewModel.IsExportTableRecruit
                        ? Visibility.Visible
                        : Visibility.Hidden;
                }
            };
        }

        private void SubscribeRecruitDoubleClick(MainViewModel viewModel)
        {
            recruits.MouseDoubleClick += (s, e) =>
            {
                viewModel.EditRecruitCommand.Execute(null);
            };
        }
    }
}
