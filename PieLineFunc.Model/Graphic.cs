using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using LiveCharts.Defaults;
using PieLineFunc.Model.Dto;

namespace PieLineFunc.Model
{
    public class Graphic
    {
        private readonly GraphicDto _dto;

        public Graphic(GraphicDto dto)
        {
            _dto = dto;
            Points = new ObservableCollection<ObservablePoint>(dto.Points);
            Points.CollectionChanged += PointsOnCollectionChanged;
        }

        public ObservableCollection<ObservablePoint> Points { get; }

        public DataTable Table { get; } = new DataTable() {Columns = {new DataColumn("X"), new DataColumn("Y")}};


        public string Name
        {
            get => _dto.Name;
            set => _dto.Name = value;
        }

        public GraphicDto GetDto()
        {
            return _dto;
        }

        private void PointsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    Table.Rows[i + e.NewStartingIndex][0] = ((ObservablePoint) e.NewItems[i]).X;
                    Table.Rows[i + e.NewStartingIndex][1] = ((ObservablePoint) e.NewItems[i]).Y;
                }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    var point = (ObservablePoint) e.NewItems[i];
                    Table.Rows.Add(point.X.ToString(), point.Y.ToString());
                }
            }
        }
    }
}