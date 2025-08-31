using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.Contracts.Components.AutoComplete.Contracts
{
    public class AutoCompleteContract
    {
        public interface IAutoCompleteView
        {
            List<AutoCompleteModel> values { get; set; }
            /// <summary>
            /// 約束子層的抽象類別，將選中的 item 項目，透過 EventHandler 傳給有註冊SelectedItem事件的人
            /// </summary>
            /// <param name="item"></param>
            void SelectedItem(object item); 
            void InitializeComponent();
            void UpdateListBox(List<AutoCompleteModel> datas);
            void NotifySelectedItemResponse(object data);
            void SendQuery(object itemValue);
            void KeyUp(object sender, EventArgs e);
            void KeyDown(object sender, ConsoleKey e);


        }

        public interface IAutoCompletePresenter
        {
            Task GetSelectedOptionInfo(object itemValue);
            Task GetRelatedOptions(string query);
        }
    }
}
