namespace GrampsView.UserControls
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RadioItem : INotifyPropertyChanged
    {
        private bool _enabled = true;

        private bool toggled;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Text { get; set; }

        public bool Toggled
        {
            get => toggled;
            set
            {
                if (toggled != value)
                {
                    toggled = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}