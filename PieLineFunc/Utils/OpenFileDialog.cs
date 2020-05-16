using Microsoft.Win32;
using PieLineFunc.Model.Utils;

namespace PieLineFunc.Utils
{
    public class OpenFileWindow : IOpenFileWindow
    {
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog()
            {Filter = "Text file (*.txt)|*.txt|Xml file (*.xml)|*.xml"};

        #region Implementation of IOpenFileWindow

        public string GetPath()
        {
            if (_openFileDialog.ShowDialog() == true)
                return _openFileDialog.FileName;
            return null;
        }

        #endregion
    }
}