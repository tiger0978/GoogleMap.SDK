using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using GooleMap.SDK.Core.Utility.Extensions;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.UI.WPF.Components.AutoComplete.Views
{
    public abstract class BaseWPFAutoCompleteView<T> : TextBox, IAutoCompleteView
    {
        public List<AutoCompleteModel> values { get => _values; set => _values = value; }
        public EventHandler<T> SelectedItem;
        private ListBox _listBox;
        private bool _isAdded;
        private List<AutoCompleteModel> _values;
        private String _formerValue = String.Empty;
        protected IAutoCompletePresenter _presenter;
        private Popup _popup;

        public BaseWPFAutoCompleteView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            ResetListBox();
        }

        public void InitializeComponent()
        {
            this.KeyUp += autoCompleteTextBox_TextChanged;
            this.KeyDown += this_KeyDown;
            this._listBox = new ListBox();
            var itemStyle = new Style(typeof(ListBoxItem));
            itemStyle.Setters.Add(new Setter(ListBoxItem.HeightProperty, 20.0));
            this._listBox.ItemContainerStyle = itemStyle;
            this._listBox.MouseDoubleClick += new MouseButtonEventHandler(this.DoubleClick);
            _popup = new Popup
            {
                Child = _listBox,
                PlacementTarget = this,
                Placement = PlacementMode.Bottom,
                StaysOpen = false
            };
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            ((IAutoCompleteView)this).KeyDown(sender, (ConsoleKey)e.Key);
        }

        public void NotifySelectedItemResponse(object data)
        {
            SelectedItem?.Invoke(this, (T)data);
        }

        void IAutoCompleteView.SelectedItem(object item)
        {
            SelectedItem?.Invoke(this, (T)item);
        }

        public abstract void SendQuery(object itemValue);


        public void UpdateListBox(List<AutoCompleteModel> datas)
        {
            _values = datas;
            if (Text == _formerValue) return;
            _formerValue = Text;
            _listBox.ItemsSource = _values;
            _listBox.DisplayMemberPath = "Key";
            _listBox.SelectedValuePath = "Value";
            ShowListBox();
            _listBox.Width = this.ActualWidth;
        }

        void IAutoCompleteView.KeyDown(object sender, ConsoleKey e)
        {
            switch (e)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Tab:
                    {
                        if (_listBox.Visibility == Visibility.Visible)
                        {
                            AutoCompleteModel item = (AutoCompleteModel)_listBox.SelectedItem;
                            this.Text = item.Key;
                            SendQuery(item.Value);
                            //InsertWord((String)_listBox.SelectedItem);
                            ResetListBox();
                            _formerValue = Text;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if ((_listBox.Visibility == Visibility.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;

                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if ((_listBox.Visibility == Visibility.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;

                        break;
                    }
            }
        }

        void IAutoCompleteView.KeyUp(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            if (_listBox.Visibility == Visibility.Visible)
            {
                AutoCompleteModel item = (AutoCompleteModel)_listBox.SelectedItem;
                this.Text = item.Key;
                SendQuery(item.Value);

                ResetListBox();
                _formerValue = Text;
            }
        }
        private void ShowListBox()
        {
            if (!_isAdded)
            {
                _isAdded = true;
                _popup.IsOpen = true;
            }
            _listBox.Visibility = Visibility.Visible;

        }

        private void ResetListBox()
        {
            _listBox.Visibility = Visibility.Collapsed;
            _popup.IsOpen = false;
        }


        private void autoCompleteTextBox_TextChanged(object sender, EventArgs e)
        {
            this.DebounceTime(async (state) =>
            {
                await _presenter.GetRelatedOptions(this.Text);
            }, null, 500);
        }
    }
}

