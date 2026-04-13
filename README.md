GoogleMap.SDK 🌍
這是一個專為 .NET 平台打造的整合型 Google Maps 開發套件，結合了強大的 Google Maps REST API 與 GMap.NET 繪圖引擎。本 SDK 採用高度模組化的設計，不僅支援 WinForms 與 WPF 雙介面框架，更提供了一套完整的圖層管理（Overlay Service）與元件化解決方案。

🚀 核心特色 (Core Features)
雙平台支持 (Multi-Platform UI)：針對 WinForms 與 WPF 均提供專屬的 GoogleMapControl，確保在不同框架下都能擁有一致的開發體驗與地圖操作邏輯。

豐富的 API 整合：完整封裝 Google Maps 核心服務：

Place Service：支援搜尋（FindPlace）、鄰近搜尋（NearBy）、景點細節（Detail）及自動完成（AutoComplete）。

Direction Service：提供路徑規劃功能，支援多種交通模式與中繼點設定。

Geocoding Service：提供經緯度與地址之間的座標轉換功能。

強大的圖層與標記管理 (Overlay System)：透過 IMapOverlayService 統一管理地圖上的圖記（Markers）與路徑軌跡（Routes）。

元件化 Presenter 邏輯：將 UI 與邏輯抽離，內建 PlaceAutoCompletePresenter，方便快速開發景點搜尋輸入框等互動元件。

高彈性的配置：支援透過 appsettings.json 管理 API Key，並提供相應的註冊機制（Registration）。

📂 專案架構 (Architecture)
專案遵循關注點分離原則，拆分為多個功能專屬的組件：

GoogleMap.SDK.API：底層 API 實作層。負責與 Google 伺服器進行 HTTP 通訊，處理 Request 與 Response 的序列化。

GoogleMap.SDK.Contract：定義層。包含所有介面（Interfaces）、資料傳輸物件（DTOs）、列舉（Enums）以及如 Polyline 解析等通用工具。

GoogleMap.SDK.Core：核心邏輯層。包含圖層管理服務（MapOverlayService）以及跨平台的 Presenter 邏輯。

GoogleMap.SDK.UI.WPF：WPF 專屬組件。包含 XAML 介面控制項、自定義 Tooltip 樣式及 WPF 圖層實作。

GoogleMap.SDK.UI.Winform：WinForms 專屬組件。包含 WinForms 控制項實作與傳統視窗下的地圖渲染處理。

🛠️ 技術標籤 (Technology Stack)
Language: C# (.NET Framework / .NET Standard)

Map Engine: GMap.NET

Design Patterns: MVP (Model-View-Presenter), Repository Pattern, Interface-Based Design

Utilities: Newtonsoft.Json, Fody (Property Change Notification)

📝 快速開始 (Quick Start)
1. 初始化 API 上下文
在應用程式啟動時，註冊並初始化 GoogleAPIContext：

C#
// 設定連線配置
var apiRegistration = new GoogleMapAPIRegistration(configuration);
var context = new GoogleAPIContext(apiRegistration);

// 使用 Place Service
var searchResult = await context.Place.FindPlaceAsync(new FindPlaceRequest { ... });
2. 在 WPF 中使用地圖控制項
直接在 XAML 中引用控制項並綁定圖層服務：

XML
<gmap:GoogleMapControl x:Name="MyMap" />
3. 圖層操作範例
透過 Presenter 或 Service 動態添加 Marker：

C#
mapOverlayService.AddMarker(new MarkerInfo 
{ 
    Latlng = new Latlng(25.0339, 121.5644), 
    Title = "台北 101" 
});
🧪 測試與範例 (Samples)
專案內附帶了 GoogleMap.SDK.UI.WPF.Test 與 GoogleMap.SDK.UI.Winform.Test 兩個範例專案，開發者可以參考其中的實作方式來快速上手地圖控制項的各種互動功能。
