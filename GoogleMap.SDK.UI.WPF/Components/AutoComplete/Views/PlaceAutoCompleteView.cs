using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using GooleMap.SDK.Core.Components.AutoComplete.Presenters;

namespace GooleMap.SDK.UI.WPF.Components.AutoComplete.Views
{
    public class PlaceAutoCompleteView : BaseWPFAutoCompleteView<PlaceDetailResponse>
    {
        public PlaceAutoCompleteView(IPresenterFactory presenterFactory) : base(presenterFactory)
        {
            _presenter = presenterFactory.CreatePresneter<IAutoCompletePresenter, IAutoCompleteView>(this, typeof(PlaceAutoCompletePresenter));
        }

        public override async void SendQuery(object itemValue)
        {
            await _presenter.GetSelectedOptionInfo(itemValue);
        }
    }
}
