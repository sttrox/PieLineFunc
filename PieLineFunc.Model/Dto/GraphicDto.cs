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
        public GraphicDto()
        {
        }

        public GraphicDto(string name)
        {
            Name = name;
            Points = new List<ObservablePoint>();
        }

        public List<ObservablePoint> Points { get; set; }

        public string Name { get; set; }
    }
}