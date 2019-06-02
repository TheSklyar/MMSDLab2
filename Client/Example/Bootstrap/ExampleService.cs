using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Helpers.Common;
using Helpers.Interfaces;

namespace Example.Bootstrap
{
    public class ExampleService:Service
    {
        public ExampleService(
            IWindow parent,
            IGenericFactory factory)

        {
            _parentWindow = Guard.GetNotNull(parent, "nativeWindow");
            _factory = Guard.GetNotNull(factory, "factory");

            Ident = "{1934d486-3fab-41c4-93e2-43488dde73f1}";
            Name = "Пример";
            Description = string.Format("Описание примера");
            IsVisibleToUser = true;//поменять на true
        }

        public override bool Execute()
        {
            return ExecutionShield(() =>
            {
                var orderWindow = _factory.Create<ExampleWindow>();
                orderWindow.Owner = _parentWindow.parent;
                orderWindow.ShowDialog();
                return true;
            });
        }

        private readonly IGenericFactory _factory;
        private readonly IWindow _parentWindow;
    }
}

