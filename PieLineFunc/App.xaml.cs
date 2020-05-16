using System.Windows;
using PieLineFunc.ViewModels;

namespace PieLineFunc
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();

            var viewModel = new MainViewModel();
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        #endregion
    }
}