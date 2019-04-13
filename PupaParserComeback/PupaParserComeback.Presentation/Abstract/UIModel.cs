using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PupaParserComeback.Presentation.Abstract
{
    public abstract class UIModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
