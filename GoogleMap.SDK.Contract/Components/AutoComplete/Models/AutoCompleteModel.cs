using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.Contracts.Components.AutoComplete.Models
{
    public class AutoCompleteModel
    {
        public String Key { get; set; }
        public object Value { get; set; }
        public AutoCompleteModel(string key, Object value)
        {
            Key = key;
            Value = value;
        }
    }
}