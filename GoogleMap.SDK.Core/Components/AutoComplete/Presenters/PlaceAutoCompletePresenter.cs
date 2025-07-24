using GoogleMap.SDK.API;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceAutoComplete.Response;
using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.Core.Components.AutoComplete.Presenters
{
    public class PlaceAutoCompletePresenter : IAutoCompletePresenter
    {
        protected readonly IAutoCompleteView _baseView;
        private readonly IGoogleAPIContext _googleApiContext;

        public PlaceAutoCompletePresenter(IAutoCompleteView baseView, IGoogleAPIContext googleAPIContext)
        {
            _googleApiContext = googleAPIContext;
            _baseView = baseView;
        }

        public async Task GetRelatedOptions(string query)
        {
            var response = await _googleApiContext.Place.PlaceAutoCompleteAsync(query);
            List<AutoCompleteModel> autoCompleteTexts = response.predictions.Select(x => new AutoCompleteModel(x.structured_formatting.main_text, x)).ToList();
            _baseView.UpdateListBox(autoCompleteTexts);
        }

        public async Task GetSelectedOptionInfo(object itemValue)
        {
            PlaceAutoCompleteRespnse.Prediction prediction = (PlaceAutoCompleteRespnse.Prediction)itemValue;
            var placeDetail = await _googleApiContext.Place.PlaceDetailAsync(prediction.place_id);
            _baseView.NotifySelectedItemResponse(placeDetail);
        }
    }
}
