using GooleMap.SDK.Contracts.Components.AutoComplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GooleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace GooleMap.SDK.Core.Components.AutoComplete.Presenters
{
    public class EmployeeAutoCompletePresenter : IAutoCompletePresenter
    {
        protected readonly IAutoCompleteView _baseView;
        private List<AutoCompleteModel> datas = new List<AutoCompleteModel>() 
        {
            new AutoCompleteModel("Andy","A"),
            new AutoCompleteModel("Bob","B"),
            new AutoCompleteModel("Cindy","C")
        };

        private List<AutoCompleteModel> employeeInfo = new List<AutoCompleteModel>()
        {
            new AutoCompleteModel("A","男生"),
            new AutoCompleteModel("B","男生"),
            new AutoCompleteModel("C","女生"),
        };


        public EmployeeAutoCompletePresenter(IAutoCompleteView baseView)
        {
            _baseView = baseView;
        }

        public async Task GetRelatedOptions(string query)
        {
            _baseView.UpdateListBox(datas);
        }

        public async Task GetSelectedOptionInfo(object itemValue)
        {
            var emp = employeeInfo.FirstOrDefault(x => x.Key == itemValue.ToString());
            _baseView.NotifySelectedItemResponse(emp);
        }
    }
}
