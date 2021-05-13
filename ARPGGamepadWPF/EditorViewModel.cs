using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ARPGGamepadCore;

namespace ARPGGamepadWPF
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private GamepadProfile profile;
        public GamepadProfile Profile 
        {
            get => profile;
            set { profile = value; OnPropertyChanged(); }
        }
    }
}
