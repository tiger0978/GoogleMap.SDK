using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GooleMap.SDK.Contract;
using GooleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

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
        MapInfoToolTipData data;
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
        }

        private async void GetEndInfomation(object sender, PlaceDetailResponse e)
        {
            Console.WriteLine(e.result.name);
            _endPlaceDetailInfo = e;
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
            if (_placeDetailInfo != null)
            {
                AddMarker(_placeDetailInfo);
            }
            if (_endPlaceDetailInfo != null)
            {
                AddMarker(_endPlaceDetailInfo);
            }

            var location = new Location(_placeDetailInfo.result.geometry.location.lat, _placeDetailInfo.result.geometry.location.lng);
            var end = new Location(_endPlaceDetailInfo.result.geometry.location.lat, _endPlaceDetailInfo.result.geometry.location.lng);

            var route = _context.Direction.GetDirectionAsync(location, end, TrafficMode.TRANSIT, new List<Avoid>());
            _gmap.CreateRoute(route.Result.routes[0].polyline.encodedPolyline);
        }

        private void AddMarker(PlaceDetailResponse placeInfo)
        {
            var mapInfoToolTip = (Style)this.FindResource("MapInfoToolTipStyle");
            data = new MapInfoToolTipData()
            {
                Title = placeInfo.result.name,
                Address = placeInfo.result.formatted_address
                //OpeningTime = placeInfo.result.current_opening_hours
            };
            ToolTip tooltip = new ToolTip
            {
                Style = mapInfoToolTip,
                DataContext = data
            };
            _gmap.CreateMarker(placeInfo.result.geometry.location.lat, placeInfo.result.geometry.location.lng, GMarkerGoogleType.red_dot, tooltip);
        }
    }
}
