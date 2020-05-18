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
            _containerGraphics.ChangeGraphics += (sender, list) => { ReCreateGraphics(list); };
            ReCreateGraphics(_containerGraphics.Graphics);
        }


        #region Property Graphics(List<GraphicViewModel>)

        private List<GraphicViewModel> _graphics;

        public List<GraphicViewModel> Graphics
        {
            get { return _graphics; }
            set
            {
                _graphics = value;
                OnPropertyChanged(nameof(Graphics));
            }
        }

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

        public GraphicViewModel SelectedGraphic
            => Graphics[SelectedIndex];

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


        #region Property SelectedIndex(int)

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
                OnPropertyChanged(nameof(SelectedGraphic));
            }
        }

        #endregion

        #region Command AddGraphic(RelayCommand)

        private ICommand _addGraphicCommand;

        public ICommand AddGraphicCommand
            => _addGraphicCommand ?? (_addGraphicCommand = new RelayCommand(AddGraphicCommand_Execute));


        private void AddGraphicCommand_Execute(object parameter)
        {
            _containerGraphics.CreateGraphic();
            SelectedIndex = Graphics.Count - 1;
        }

        #endregion

        #region Command CopyToExcelCommand(RelayCommand)

        private ICommand _copyToExcelCommandCommand;

        public ICommand CopyToExcelCommand
            => _copyToExcelCommandCommand ??
               (_copyToExcelCommandCommand = new RelayCommand(CopyToExcelCommandCommand_Execute));

        private void CopyToExcelCommandCommand_Execute(object parameter)
        {
            SelectedGraphic.CopyToExcel();
        }

        #endregion


        #region Command InsertFromExcelCommand(RelayCommand)

        private ICommand _insertFromExcelCommandCommand;

        public ICommand InsertFromExcelCommand
            => _insertFromExcelCommandCommand ?? (_insertFromExcelCommandCommand =
                   new RelayCommand(InsertFromExcelCommandCommand_Execute));

        private void InsertFromExcelCommandCommand_Execute(object parameter)
        {
            SelectedGraphic.CopyFromExcel();
        }

        #endregion

        private void ReCreateGraphics(List<Graphic> list)
        {
            Graphics = list.Select(graphic => new GraphicViewModel(graphic)).ToList();
        }
    }
}