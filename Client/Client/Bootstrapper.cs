﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Helpers.Common;
using Helpers;
using Helpers.DB;
using Helpers.Interfaces;
using System.Windows;
using Book.Bootstrap;
using Example.Bootstrap;
using People.Bootstrap;
using PeopleBooks.Bootstrap;

namespace Client
{
    internal class Bootstrapper
    {
        private readonly IWindsorContainer _container;

        public Bootstrapper(IWindsorContainer container)
        {
            _container = Guard.GetNotNull(container, "container");
            AppDomain.CurrentDomain.UnhandledException += ProcessUnhandledException;
        }

        private void ProcessUnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            CtmMessageBox.Show("Ошибка приложения", "Информация в StackTrace", ex.ExceptionObject.ToString());
        }

        public void Install()
        {
            _container
               .Install(new CoreInstaller())
               .Install(new LauncherInstaller())
               .Install(new ExampleInstaller())
               .Install(new PeopleInstaller())
               .Install(new BookInstaller())
               .Install(new PeopleBooksInstaller())
               ;
            
        }

        public void RunApplication()
        {
            try
            {
                InitializeCulture();

                var mainView = _container.Resolve<IWindow>();
                if(mainView is null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    ((Window)mainView).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                CtmMessageBox.Show("Ошибка", ex.Message, ex.StackTrace);
            }
        }

        private static void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
        }
    }
}
