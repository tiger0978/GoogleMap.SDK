using GoogleMap.SDK.API;
using GoogleMap.SDK.API.Commons.Models;
using GoogleMap.SDK.API.Enums;
using GoogleMap.SDK.API.Models.Responses;
using GoogleMap.SDK.API.Services.Direction.Request;
using GoogleMap.SDK.API.Services.Direction.Response;
using System;

namespace GoogleMap.SDK.UI.Winform.Test
{
    public partial class Form1 : Form
    {
        private IGoogleAPIContext _context;
        public Form1(IGoogleAPIContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private async Task<DirectionNewResponse> GetDirectAsync()
        {
            Location ori = new Location() { latLng = new Latlng { latitude = (float)37.419734, longitude = (float)-122.0827784 } };
            Location des = new Location() { latLng = new Latlng { latitude = (float)37.417670, longitude = (float)-122.079595 } };
            var route = await _context.Direction.GetDirectionAsync(ori, des, TrafficMode.DRIVE, new List<Avoid>() { Avoid.highways });
            return route;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var result = await GetDirectAsync();
            Console.WriteLine(result);
        }
    }
}
