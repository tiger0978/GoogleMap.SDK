using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using GoogleMap.SDK.Core;
using GooleMap.SDK.Core.Components.AutoComplete.Presenters;


namespace GooleMap.SDK.UI.WPF.Components.AutoComplete.Views
{
    public class EmployeeAutoCompleteView : BaseWPFAutoCompleteView<AutoCompleteModel>
    {
        public EmployeeAutoCompleteView(IPresenterFactory presenterFactory) : base(presenterFactory)
        {
            _presenter = presenterFactory.CreatePresneter<IAutoCompletePresenter, IAutoCompleteView>(this, typeof(EmployeeAutoCompletePresenter));
        }

        public override async void SendQuery(object itemValue)
        {
            await _presenter.GetSelectedOptionInfo(itemValue);
        }
    }
}
