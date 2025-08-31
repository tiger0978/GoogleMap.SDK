using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using GooleMap.SDK.UI.Winform.Utility.Extentsions;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.UI.Winform.Components.AutoComplete.Views
{
    // typeof(IAutoCompleteView<>),PlaceAutoCompleteView
    public abstract class BaseWinformAutoCompleteView<T> : TextBox, IAutoCompleteView
    {
        protected IAutoCompletePresenter _presenter;
        private ListBox _listBox;
        private bool _isAdded;
        private List<AutoCompleteModel> _values;
        private String _formerValue = String.Empty;
        public EventHandler<T> SelectedItem;
        public List<AutoCompleteModel> values { get => _values; set => _values = value; }


        public BaseWinformAutoCompleteView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            ResetListBox();
        }


        public void InitializeComponent()
        {

            this.KeyUp += autoCompleteTextBox_TextChanged;
            this.KeyDown += this_KeyDown;
            this._listBox = new ListBox();
            this._listBox.ItemHeight = 20;
            this._listBox.Location = new System.Drawing.Point(0, 0);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(120, 96);
            this._listBox.TabIndex = 0;
            //this._listBox.SelectedIndexChanged += new System.EventHandler(this._listBox_SelectedIndexChanged);
            this._listBox.DoubleClick += new System.EventHandler(this._listBox_DoubleClick);
            this.ResumeLayout(false);
            this.SuspendLayout();
        }

        private void ShowListBox()
        {
            if (!_isAdded)
            {
                Parent.Controls.Add(_listBox);
                // 原版寫法，長在下面
                //_listBox.Left = Left;
                //_listBox.Top = Top + Height;
                _listBox.Left = Right;
                _listBox.Top = 0;
                _isAdded = true;
            }
            _listBox.Visible = true;
            _listBox.BringToFront();
        }

        private void ResetListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            ((IAutoCompleteView)(this)).KeyDown(sender, (ConsoleKey)e.KeyCode); 
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        public List<String> SelectedValues
        {
            get
            {
                String[] result = Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new List<String>(result);
            }
        }


        private void _listBox_DoubleClick(object sender, EventArgs e)
        {
            if (_listBox.Visible)
            {
                AutoCompleteModel item = (AutoCompleteModel)_listBox.SelectedItem;
                this.Text = item.Key;
                SendQuery(item.Value);

                ResetListBox();
                _formerValue = Text;
            }
        }


        public abstract void SendQuery(object itemValue);

        public void NotifySelectedItemResponse(object data)
        {
            SelectedItem?.Invoke(this, (T)data);
        }

        private void autoCompleteTextBox_TextChanged(object sender, EventArgs e)
        {
            this.DebounceTime(async (state) =>
            {
                await _presenter.GetRelatedOptions(this.Text);
            }, null, 500);
        }

        public void UpdateListBox(List<AutoCompleteModel> datas)
        {
            _values = datas;
            if (Text == _formerValue) return;
            _formerValue = Text;
            _listBox.DataSource = _values;
            _listBox.DisplayMember = "Key";
            _listBox.ValueMember = "Value";
            ShowListBox();
            _listBox.Width = this.Size.Width;
        }

        void IAutoCompleteView.SelectedItem(object item)
        {
            SelectedItem?.Invoke(this,(T)item);
        }

        void IAutoCompleteView.KeyUp(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void IAutoCompleteView.KeyDown(object sender, ConsoleKey e)
        {
            switch (e)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Tab:
                    {
                        if (_listBox.Visible)
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
                        if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            _listBox.SelectedIndex++;

                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            _listBox.SelectedIndex--;

                        break;
                    }
            }
        }
    }
}

