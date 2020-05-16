using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PieLineFunc.Common;
using PieLineFunc.Model;

namespace PieLineFunc.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ContainerGraphics _containerGraphics;

        public MainViewModel(ContainerGraphics containerGraphics)
        {
            _containerGraphics = containerGraphics;
            _containerGraphics.ChangeGraphics += (sender, list) => OnPropertyChanged(nameof(Graphics));
        }


        #region Property Graphics(List<GraphicViewModel>)

        public List<GraphicViewModel> Graphics =>
            _containerGraphics.Graphics.Select(graphic => new GraphicViewModel(graphic)).ToList();

        #endregion

        #region Command Import(RelayCommand)

        private ICommand _importCommand;

        public ICommand ImportCommand
            => _importCommand ?? (_importCommand = new RelayCommand(ImportCommand_Execute));

        private void ImportCommand_Execute(object parameter)
        {
            _containerGraphics.Import();
        }

        #endregion

        #region Command Export(RelayCommand)

        private ICommand _exportCommand;

        public ICommand ExportCommand
            => _exportCommand ?? (_exportCommand = new RelayCommand(ExportCommand_Execute));


        private void ExportCommand_Execute(object parameter)
        {
            _containerGraphics.Export();
        }

        #endregion

        #region Property SelectedGraphic(GraphicViewModel)

        private GraphicViewModel _selectedGraphic;

        public GraphicViewModel SelectedGraphic
        {
            get { return _selectedGraphic; }
            set
            {
                _selectedGraphic = value;
                OnPropertyChanged(nameof(SelectedGraphic));
            }
        }

        #endregion


        #region Command DeleteGraphic(RelayCommand)

        private ICommand _deleteGraphicCommand;

        public ICommand DeleteGraphicCommand
            => _deleteGraphicCommand ??
               (_deleteGraphicCommand = new RelayCommand(DeleteGraphicCommand_Execute, CanExecute));

        private bool CanExecute(object arg)
        {
            if (_containerGraphics.Graphics.Count == 1)
                return false;
            return true;
        }

        private void DeleteGraphicCommand_Execute(object parameter)
        {
            var graphic = (GraphicViewModel) parameter;

            _containerGraphics.RemoveGraphic(graphic.GetModel());
        }

        #endregion


        #region Command AddGraphic(RelayCommand)

        private ICommand _addGraphicCommand;

        public ICommand AddGraphicCommand
            => _addGraphicCommand ?? (_addGraphicCommand = new RelayCommand(AddGraphicCommand_Execute));

        private void AddGraphicCommand_Execute(object parameter)
        {
            _containerGraphics.CreateGraphic();
        }

        #endregion
    }
}