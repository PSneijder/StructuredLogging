using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Regions;

namespace StructuredLogging.Desktop.Utilities.ViewModels
{
    public abstract class ViewModelBase
        : INotifyPropertyChanged
            , INavigationAware
    {
        private bool _isBusy;
        private string _title;

        public bool IsBusy { get { return _isBusy; } set { SetPropertyChanged(ref _isBusy, value); } }

        public string Title { get { return _title; } set { SetPropertyChanged(ref _title, value); } }
        

        #region INotifyProperyChanged

        protected void SetPropertyChanged<T>(ref T refValue, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(refValue, value))
            {
                refValue = value;
                RaisePropertyChanged(propertyName);
            }
        }

        protected void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INavigationAware

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        #endregion
    } 
}