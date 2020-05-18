using PieLineFunc.Model.Utils;
using System.Windows;

namespace PieLineFunc.Utils
{
    public class ClipboardInstance : IClipboard
    {
        #region Implementation of IClipboard

        public void SetText(string text)
        {
            Clipboard.SetText(text);
        }

        public string GetText()
        {
            return Clipboard.GetText();
        }

        #endregion
    }
}