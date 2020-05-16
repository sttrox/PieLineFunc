using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Defaults;

namespace PieLineFunc.Model.Dto
{
    public class GraphicDto
    {
        private List<ObservablePoint> _points;
        private string _name;

        public GraphicDto(string name)
        {
            _name = name;
            _points = new List<ObservablePoint>();
        }

        public List<ObservablePoint> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}