using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Common;

namespace PeopleBooks.Data
{
    public class PeopleBooksFilter : INotifyPropertyChanged
    {
        ConnectionSettings _connectionSettings;
        public Dictionary<int, string> _filters;
        public PeopleBooksFilter(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            _filters = new Dictionary<int, string>
            {
                {1, ""},
                {2, ""}
            };
        }

        public Dictionary<int, string> TableSourceForSort = new Dictionary<int, string>
        {
            {1,@"zak.ID" },
            {3,@"p.Family" },
            {2,@"b.Name" }
        };



        private int _ReaderNumFilterText;
        public string ReaderNumFilterText
        {
            get => _ReaderNumFilterText == 0 ? "" : _ReaderNumFilterText.ToString();
            set
            {
                _ReaderNumFilterText = Convert.ToInt32(string.IsNullOrWhiteSpace(value) ? "0" : value);
                if (_ReaderNumFilterText == 0)
                {
                    _filters[1] = "";
                }
                else
                {
                    _filters[1] = "p.ID="+ _ReaderNumFilterText;
                }
                OnPropertyChanged();
            }
        }

        private int _BookNumFilterText;
        public string BookNumFilterText
        {
            get => _BookNumFilterText == 0 ? "" : _BookNumFilterText.ToString();
            set
            {
                _BookNumFilterText = Convert.ToInt32(string.IsNullOrWhiteSpace(value) ? "0" : value);
                if (_BookNumFilterText == 0)
                {
                    _filters[2] = "";
                }
                else
                {
                    _filters[2] = "b.ID="+ _BookNumFilterText;
                }
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
