using GoogleMap.SDK.API.Commons;
using GoogleMap.SDK.API.Commons.Enums;
using GoogleMap.SDK.API.Services.Geocoding.Request;
using GoogleMap.SDK.API.Services.Geocoding.Response;
using GoogleMap.SDK.API.Services.Place.FindPlace.Request;
using GoogleMap.SDK.API.Services.Place.FindPlace.Response;
using GoogleMap.SDK.API.Services.Place.NearBySearch.Request;
using GoogleMap.SDK.API.Services.Place.NearBySearch.Response;
using GoogleMap.SDK.API.Services.Place.PlaceAutoComplete.Request;
using GoogleMap.SDK.API.Services.Place.PlaceAutoComplete.Response;
using GoogleMap.SDK.API.Services.Place.PlaceDetail.Request;
using GoogleMap.SDK.API.Services.Place.PlaceDetail.Response;
using GoogleMap.SDK.API.Services.Place.PlacePhoto.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Place
{
    public class PlaceService : BaseService, IPlaceService
    {
        public PlaceService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<FindPlaceResponse> FindPlaceAsync(string input, FindPlaceInputType inputType)
        {
            var findPlaceRequest = new FindPlaceRequest(input, inputType);
            var response = await GetAsync<FindPlaceResponse>(findPlaceRequest.URL);
            return response;
        }

        public async Task<NearBySearchResponse> NearBySearchAsync(string location, string radius)
        {
            var nearBySearchRequest = new NearBySearchRequest(location, radius);
            var response = await GetAsync<NearBySearchResponse>(nearBySearchRequest.URL);
            return response;
        }

        public async Task<PlaceAutoCompleteRespnse> PlaceAutoCompleteAsync(string input)
        {
            var placeAutoCompleteRequest = new PlaceAutoCompleteRequest(input);
            var response = await GetAsync<PlaceAutoCompleteRespnse>(placeAutoCompleteRequest.URL);
            return response;
        }

        public async Task<PlaceDetailResponse> PlaceDetailAsync(string place_id)
        {
            var placeDetailRequest = new PlaceDetailRequest(place_id);
            var response = await GetAsync<PlaceDetailResponse>(placeDetailRequest.URL);
            return response;
        }

        public async Task<Bitmap> PlacePhotoAsync(string photo_reference, int maxheight)
        {
            var placePhotoRequest = new PlacePhotoRequest(photo_reference, maxheight);
            var response = await GetAsync<Bitmap>(placePhotoRequest.URL);
            return response;
        }


    }
}
