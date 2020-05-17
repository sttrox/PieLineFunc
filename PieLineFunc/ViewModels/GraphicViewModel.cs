using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Windows.Input;
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
        }


        private readonly Graphic _graphic;


        #region Property Points(ObservableCollection<ObservablePoint>)

        public ObservableCollection<ObservablePoint> Points =>
            _graphic.Points;

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
            Points.Add(new ObservablePoint(0, 0));
        }

        #endregion
    }
}