using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.UI.WPF
{
    // 直接使用套件達成手動綁定 model 與 UI 效果
    [AddINotifyPropertyChangedInterface]
    public class MapInfoToolTipData
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string OpeningTime { get; set; }

        //private string title;
        //private string address;
        //private string openingTime;

        //public string Title
        //{
        //    get => title;
        //    set { title = value; OnPropertyChanged(nameof(Title)); }
        //}

        //public string Address
        //{
        //    get => address;
        //    set { address = value; OnPropertyChanged(nameof(Address)); }
        //}

        //public string OpeningTime
        //{
        //    get => openingTime;
        //    set { openingTime = value; OnPropertyChanged(nameof(OpeningTime)); }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string name) =>
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
