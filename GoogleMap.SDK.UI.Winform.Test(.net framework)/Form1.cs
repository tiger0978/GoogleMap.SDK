using GoogleMap.SDK.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using GooleMap.SDK.UI.Winform;
using GooleMap.SDK.UI.Winform.Components.AutoComplete.Views;
using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Response;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.GoogleAPI;
namespace GoogleMap.SDK.UI.Winform.Test_.net_framework_
{
    public partial class Form1 : Form
    {
        private IGoogleAPIContext _context;
        private EmployeeAutoCompleteView _autoCompleteView;
        private BaseWinformAutoCompleteView<AutoCompleteModel> _baseautoCompleteView;



        GMapsUtility map = null;

        public Form1(IGoogleAPIContext context, IEnumerable<IAutoCompleteView> autoCompleteViews)
        {
            _context = context;
            _autoCompleteView = (EmployeeAutoCompleteView)autoCompleteViews.FirstOrDefault(x=>x is EmployeeAutoCompleteView);
            InitializeComponent();
            map = new GMapsUtility();
            this.Controls.Add(_autoCompleteView);
            _autoCompleteView.SelectedItem += GetDataInfomation;
            //this.Controls.Add(map);

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
            //var target = textBox1.Text;
            //var place = await _context.Place.FindPlaceAsync(target, FindPlaceInputType.textquery);
            //var location = place.candidates[0].geometry.location;
            //map.CreateMarker(location.lat, location.lng);
        }
        private async void GetDataInfomation(object sender, AutoCompleteModel e)
        {
            Console.WriteLine(e.Value);
            //var target = textBox1.Text;
            //var place = await _context.Place.FindPlaceAsync(target, FindPlaceInputType.textquery);
            //var location = place.candidates[0].geometry.location;
            //map.CreateMarker(location.lat, location.lng);
        }
    }
}
