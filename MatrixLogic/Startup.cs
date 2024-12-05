using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {      
            StaticConfig = configuration;
        }
        public static IConfiguration StaticConfig { get; set; }

    }
}
