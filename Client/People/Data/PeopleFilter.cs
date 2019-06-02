using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Helpers.Common;

namespace People.Data
{
    public class PeopleFilter:INotifyPropertyChanged
    {
        private ConnectionSettings _connectionSettings;
        
        public Dictionary<int, string> _filters;
        public PeopleFilter(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _filters = new Dictionary<int, string>
            {
                {1, ""}
            };
        }

        private string _FamilyFilterText;
        public string FamilyFilterText
        {
            get => _FamilyFilterText;
            set
            {
                _FamilyFilterText = value;
                if (!string.IsNullOrWhiteSpace(_FamilyFilterText))
                {
                    _filters[1] = "Family like '%" + _FamilyFilterText.Trim() + "%'";
                }
                else
                {
                    _filters[1] = "";
                }
                OnPropertyChanged();
            }
        }
        public Dictionary<int, string> TableSourceForSort = new Dictionary<int, string>
        {
            {1,@"ID" },
            {2,@"Family" },
            {3,@"Name" }
        };



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
