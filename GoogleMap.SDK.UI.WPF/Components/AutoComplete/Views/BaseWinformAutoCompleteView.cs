using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.UI.Winform.Components.AutoComplete.Views
{
    public abstract class BaseWinformAutoCompleteView<T> : TextBox, IAutoCompleteView
    {
        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        public void NotifySelectedItemResponse(object data)
        {
            throw new NotImplementedException();
        }

        public abstract void SendQuery(object itemValue);

        public void UpdateListBox(List<AutoCompleteModel> datas)
        {
            throw new NotImplementedException();
        }
    }
}

