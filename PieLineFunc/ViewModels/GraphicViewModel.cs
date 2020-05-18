using System;
using System.Collections.Specialized;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Defaults;
using PieLineFunc.Common;
using PieLineFunc.Model;

namespace PieLineFunc.ViewModels
{
    public class GraphicViewModel : BaseViewModel
    {
        public GraphicViewModel(Graphic graphic)
        {
            _graphic = graphic;
            _graphic.Points.CollectionChanged += PointsOnCollectionChanged;
            Points = new ChartValues<ObservablePoint>(_graphic.Points);
        }

        private void PointsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ObservablePoint item in e.NewItems)
                    Points.Add(item);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ObservablePoint item in e.OldItems)
                    Points.Remove(item);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Points.Clear();
            }
            else
            {
                throw new NotImplementedException("Func don't implemented");
            }
        }


        private readonly Graphic _graphic;


        #region Property Points(ObservableCollection<ObservablePoint>)

        public ChartValues<ObservablePoint> Points { get; }

        #endregion


        #region Property NameGraphic(string)

        public string NameGraphic
        {
            get => _graphic.Name;
            set
            {
                _graphic.Name = value;
                OnPropertyChanged(nameof(NameGraphic));
            }
        }

        #endregion

        public Graphic GetModel()
            => _graphic;


        #region Command AddPoint(RelayCommand)

        private ICommand _addPointCommand;

        public ICommand AddPointCommand
            => _addPointCommand ?? (_addPointCommand = new RelayCommand(AddPointCommand_Execute));

        private void AddPointCommand_Execute(object parameter)
        {
            _graphic.AddNewPoint();
        }

        #endregion


        #region Command DeletePoint(RelayCommand)

        private ICommand _deletePointCommand;

        public ICommand DeletePointCommand
            => _deletePointCommand ?? (_deletePointCommand =
                   new RelayCommand(DeletePointCommand_Execute, DeletePointCommand_CanExecute));


        private bool DeletePointCommand_CanExecute(object arg)
        {
            var p = this.GetHashCode().ToString("X");

            return SelectedPoint != null;
        }

        private void DeletePointCommand_Execute(object parameter)
        {
            _graphic.RemovePoint(SelectedPoint);
        }

        #endregion


        #region Property SelectedPoint(ObservablePoint)

        private ObservablePoint _selectedPoint;

        public ObservablePoint SelectedPoint
        {
            get { return _selectedPoint; }
            set
            {
                var p = this.GetHashCode().ToString("X");
                _selectedPoint = value;
                OnPropertyChanged(nameof(SelectedPoint));
            }
        }

        #endregion

        public void CopyToExcel()
        {
            _graphic.CopyToClipboardForExcel();
        }

        public void CopyFromExcel()
        {
            _graphic.InsertPointsFromExcelByClipboard();
        }
    }
}