using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Helpers.Common;

namespace Book.Data
{
    public class BookFilter : INotifyPropertyChanged
{
    private ConnectionSettings _connectionSettings;

    public Dictionary<int, string> _filters;
    public BookFilter(ConnectionSettings connectionSettings)
    {
        _connectionSettings = connectionSettings;
        _filters = new Dictionary<int, string>
        {
            {1, ""},
            {2, "" }
        };
    }

    private string _NameFilterText;
    public string NameFilterText
        {
        get => _NameFilterText;
        set
        {
            _NameFilterText = value;
            if (!string.IsNullOrWhiteSpace(_NameFilterText))
            {
                _filters[1] = "Name like '%" + _NameFilterText.Trim() + "%'";
            }
            else
            {
                _filters[1] = "";
            }
            OnPropertyChanged();
        }
    }

    private bool _NotInHands;
    public bool NotInHands
        {
        get => _NotInHands;
        set
        {
            _NotInHands = value;
            if (_NotInHands)
            {
                _filters[2] = "ID not in (select BookID from tPeopleBooks)";
            }
            else
            {
                _filters[2] = "";
            }
            OnPropertyChanged();
        }
    }
        public Dictionary<int, string> TableSourceForSort = new Dictionary<int, string>
    {
        {1,@"ID" },
        {2,@"Name" },
        {3,@"Writer" }
    };



    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
}
