using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Helpers.Common;
using Helpers.Interfaces;

namespace Book.Bootstrap
{
    public class BookService : Service
    {
        public BookService(
            IWindow parent,
            IGenericFactory factory)

        {
            _parentWindow = Guard.GetNotNull(parent, "nativeWindow");
            _factory = Guard.GetNotNull(factory, "factory");

            Ident = "{1934d486-3fab-41c4-93e2-43488dde73f3}";
            Name = "Книги";
            Description = string.Format("Книги в наличии в библиотеке");
            IsVisibleToUser = true;//поменять на true
        }

        public override bool Execute()
        {
            return ExecutionShield(() =>
            {
                var orderWindow = _factory.Create<BookGrid>();
                orderWindow.Owner = _parentWindow.parent;
                orderWindow.ShowDialog();
                return true;
            });
        }

        private readonly IGenericFactory _factory;
        private readonly IWindow _parentWindow;
    }
}
