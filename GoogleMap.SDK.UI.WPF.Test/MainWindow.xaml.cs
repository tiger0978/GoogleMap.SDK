using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private IGoogleAPIContext _context;
        private PlaceAutoCompleteView _startAutoCompleteView;

        private PlaceAutoCompleteView _endAutoCompleteView;

        //private BaseWPFAutoCompleteView<AutoCompleteModel> _baseAutoCompleteView;
        MapInfoToolTipData data = new MapInfoToolTipData();
        private PlaceDetailResponse _placeDetailInfo;
        private PlaceDetailResponse _endPlaceDetailInfo;



        private IGMap _gmap;


        public MainWindow(IComponentFactory componentFactory, IGMap gmap, IGoogleAPIContext context)
        {
            InitializeComponent();
            _context = context;
            var autoCompleteViews = componentFactory.Create<IEnumerable<IAutoCompleteView>>();
            _startAutoCompleteView = (PlaceAutoCompleteView)autoCompleteViews.FirstOrDefault(x => x is PlaceAutoCompleteView);
            _startAutoCompleteView.SelectedItem += GetDataInfomation;

            autoCompleteViews = componentFactory.Create<IEnumerable<IAutoCompleteView>>();
            _endAutoCompleteView = (PlaceAutoCompleteView)autoCompleteViews.FirstOrDefault(x => x is PlaceAutoCompleteView);
            _endAutoCompleteView.SelectedItem += GetEndInfomation;
            _gmap = gmap;
            this.mapContainer.Children.Add((UserControl)_gmap);
            this.autoCompleteContainer.Children.Add(this._startAutoCompleteView);
            this.autoCompleteContainer.Children.Add(this._endAutoCompleteView);

        }

        private async void GetDataInfomation(object sender, PlaceDetailResponse e)
        {
            Console.WriteLine(e.result.name);
            _placeDetailInfo = e;
            data.Title = _placeDetailInfo.result.name;
            data.Address = _placeDetailInfo.result.formatted_address;
        }

        private async void GetEndInfomation(object sender, PlaceDetailResponse e)
        {
            Console.WriteLine(e.result.name);
            _endPlaceDetailInfo = e;
            data.Title = _endPlaceDetailInfo.result.name;

        }



        private void AddRoute(List<PointLatLng> points)
        {
            var route = new GMapRoute(points)
            {
                Shape = new Path
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 3,
                    Opacity = 0.7
                },
                Points = new List<PointLatLng>()
                {
                        new PointLatLng(25.033964, 121.564468), // 台北101
                        new PointLatLng(24.112345, 120.615123)  // 台中高鐵站 (範例)
                }

            };

            GMapRoute gmRoute = new GMapRoute(route.Points);
            //gmap.Markers.Add(gmRoute);
        }
        private void Marker_Click(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element?.Tag is GMapMarker marker)
            {
                // 你可以用 marker.Position, marker.Tag 等等
                var info = marker.Tag?.ToString() ?? "無標題";
                MessageBox.Show($"你點了 marker：{info} ({marker.Position.Lat}, {marker.Position.Lng})");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            var location = new Location(_placeDetailInfo.result.geometry.location.lat, _placeDetailInfo.result.geometry.location.lng);

            var end = new Location(_endPlaceDetailInfo.result.geometry.location.lat, _endPlaceDetailInfo.result.geometry.location.lng);

            var tooltipStyle = (Style)FindResource("MapInfoToolTipStyle");



            // 建立 ToolTip 並綁定資料物件
            var toolTip = new ToolTip
            {
                Style = tooltipStyle,
                DataContext = data // 關鍵步驟！！
            };
            _gmap.CreateMarker(location,"Test",GMarkerGoogleType.red_dot, toolTip);
            _gmap.CreateMarker(end,"Test", GMarkerGoogleType.red_dot, toolTip);

            var route = _context.Direction.GetDirectionAsync(location, end, TrafficMode.TRANSIT, new List<Avoid>());
            _gmap.CreateRoute(route.Result.routes[0].polyline.encodedPolyline);
        }

        private void AddMarker(PlaceDetailResponse placeInfo)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var location = new Location(_placeDetailInfo.result.geometry.location.lat, _placeDetailInfo.result.geometry.location.lng);
            List<Location> locations = new List<Location>();
            locations.Add(location);
            _gmap.RemoveMarkerElement(locations, "Test");

        }
    }
}
