﻿using Ninject;
using SocialServicesManager.App.Providers.Contracts;
using SocialServicesManager.ConsoleUI.Container;

namespace SocialServicesManager.ConsoleUI
{
    public class Startup
    {
        public static void Main()
        {
            var kernel = new StandardKernel(new SocialServiceNinjectModule());
            var engine = kernel.Get<IEngine>();

            //Sample commands:
            //createfamily Petrovi;
            //createuser Nedialka;
            //createvisit 01.01.1999 description 1 1 HomeVisit

            engine.Start();
        }
    }
}