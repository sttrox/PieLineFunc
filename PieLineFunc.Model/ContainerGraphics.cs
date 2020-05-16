using System;
using System.Collections.Generic;
using System.Linq;
using PieLineFunc.Model.Dto;
using PieLineFunc.Model.Utils;

namespace PieLineFunc.Model
{
    public class ContainerGraphics
    {
        private ISerialized _serializable;
        private readonly IOpenFileWindow _openFileWindow;
        private readonly ISaveFileWindow _saveFileWindow;

        public ContainerGraphics(ISerialized serializable, IOpenFileWindow openFileWindow,
            ISaveFileWindow saveFileWindow)
        {
            _serializable = serializable;
            _openFileWindow = openFileWindow;
            _saveFileWindow = saveFileWindow;

            Graphics = new List<Graphic>();
            CreateGraphic();
        }

        #region Event ChangeGraphics(EventHandler<List<Graphic>>)

        public event EventHandler<List<Graphic>> ChangeGraphics;

        private void RaiseGraphics(List<Graphic> value) =>
            ChangeGraphics?.Invoke(this, value);

        #endregion

        #region Property Graphics(List<Graphic>)

        private List<Graphic> _graphics;

        public List<Graphic> Graphics
        {
            get { return _graphics; }
            set
            {
                _graphics = value;
                RaiseGraphics(Graphics);
            }
        }

        #endregion


        public void Import()
        {
            var path = _openFileWindow.GetPath();
            Graphics = _serializable.Deserialized<List<GraphicDto>>(path).Select(dto => new Graphic(dto)).ToList();
        }

        public void Export()
        {
            var path = _saveFileWindow.CreatePath();

            _serializable.Serialized(Graphics.Select(graphic => graphic.GetDto()).ToList(), path);
        }

        public void RemoveGraphic(Graphic model)
        {
            Graphics.Remove(model);
            RaiseGraphics(Graphics);
        }

        public void CreateGraphic()
        {
            Graphics.Add(new Graphic(new GraphicDto("График " + Graphics.Count)));
            RaiseGraphics(Graphics);
        }
    }
}