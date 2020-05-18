using System;
using LiveCharts.Defaults;
using PieLineFunc.Model.Dto;
using System.Collections.ObjectModel;
using System.Linq;
using PieLineFunc.Model.Utils;

namespace PieLineFunc.Model
{
    public class Graphic
    {
        public IClipboard Clipboard { get; }
        private readonly GraphicDto _dto;

        public Graphic(GraphicDto dto, IClipboard clipboard)
        {
            Clipboard = clipboard;
            _dto = dto;
            Points = new ObservableCollection<ObservablePoint>(dto.Points);
        }

        public Graphic(string graphicName, IClipboard clipboard) : this(new GraphicDto(graphicName), clipboard)
        {
        }

        public ObservableCollection<ObservablePoint> Points { get; }

        public string Name
        {
            get => _dto.Name;
            set => _dto.Name = value;
        }

        public GraphicDto GetDto()
        {
            return _dto;
        }

        public void RemovePoint(ObservablePoint selectedPoint)
        {
            Points.Remove(selectedPoint);
            _dto.Points.Remove(selectedPoint);
        }

        public void AddNewPoint()
        {
            var point = new ObservablePoint(0, 0);
            _dto.Points.Add(point);
            Points.Add(point);
        }

        public void CopyToClipboardForExcel()
        {
            var line = "X\tY\n";
            var points = String.Concat(Points.Select(point => point.X + "\t" + point.Y + "\n"));
            Clipboard.SetText(line + points);
        }

        public void InsertPointsFromExcelByClipboard()
        {
            var lines = Clipboard.GetText();
            var rows = lines.Split(new string[1] {"\r\n"}, StringSplitOptions.None);
            if (rows.Length > 0)
                Points.Clear();

            for (var i = 0; i < rows.Length; i++)
            {
                var columns = rows[i].Split('\t');
                if (columns.Length < 2)
                    continue;
                var x = 0d;

                if (!double.TryParse(columns[0], out x))
                    continue;
                var y = 0d;
                if (!double.TryParse(columns[1], out y))
                    continue;

                Points.Add(new ObservablePoint(x, y));
            }
        }
    }
}