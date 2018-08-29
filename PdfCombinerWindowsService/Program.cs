﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    using PdfCombiner;

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main(string[] args)
        {
            var servicesToRun = new ServiceBase[]
                                              {
                                                  new PdfCombinerWindowsService(args)
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
