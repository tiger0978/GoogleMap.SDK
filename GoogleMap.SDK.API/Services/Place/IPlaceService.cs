using GoogleMap.SDK.API.Commons.Enums;
using GoogleMap.SDK.API.Services.Geocoding.Response;
using GoogleMap.SDK.API.Services.Place.FindPlace.Response;
using GoogleMap.SDK.API.Services.Place.NearBySearch.Response;
using GoogleMap.SDK.API.Services.Place.PlaceAutoComplete.Response;
using GoogleMap.SDK.API.Services.Place.PlaceDetail.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Place
{
    public interface IPlaceService
    {
        Task<FindPlaceResponse> FindPlaceAsync(string input, FindPlaceInputType inputType);
        Task<NearBySearchResponse> NearBySearchAsync(string location, string radius);
        Task<PlaceAutoCompleteRespnse> PlaceAutoCompleteAsync(string input);
        Task<PlaceDetailResponse> PlaceDetailAsync(string place_id);
        Task<Bitmap> PlacePhotoAsync(string photo_reference, int maxheight);
    }
}
