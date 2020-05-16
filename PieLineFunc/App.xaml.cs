using System.Windows;
using PieLineFunc.Model;
using PieLineFunc.Utils;
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

            var model = new ContainerGraphics(new SerializerXml(), new OpenFileWindow(), new SaveFileWindow());
            var viewModel = new MainViewModel(model);
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        #endregion
    }
}