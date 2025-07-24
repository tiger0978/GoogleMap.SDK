using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.FindPlace.Response;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.NearBySearch.Response;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceAutoComplete.Response;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.Services
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
