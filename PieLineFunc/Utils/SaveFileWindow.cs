using Microsoft.Win32;
using PieLineFunc.Model.Utils;

namespace PieLineFunc.Utils
{
    public class SaveFileWindow : ISaveFileWindow
    {
        private SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {Filter = "Text file (*.txt)|*.txt|Xml file (*.xml)|*.xml"};

        #region Implementation of ISaveFileWindow

        public string CreatePath()
        {
            if (_saveFileDialog.ShowDialog() == true)
            {
                return _saveFileDialog.FileName;
            }

            return null;
        }

        #endregion
    }
}