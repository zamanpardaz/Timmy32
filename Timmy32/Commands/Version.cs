﻿using McMaster.Extensions.CommandLineUtils;
using System;
using Timmy32.Exceptions;
using Timmy32.Models;

namespace Timmy32.Commands
{
    [Command(Description = "Get ZP SDK Version")]
    class Version 
    {
        public void OnExecute(IConsole console)
        {
            Console.WriteLine("2.5.1");
            Console.WriteLine("ZamanPardaz Inc.");
            Console.WriteLine("CLI Version 32bits");
        }
    }
}
