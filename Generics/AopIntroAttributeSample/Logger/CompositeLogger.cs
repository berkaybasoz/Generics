using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Logger
{
    public class CompositeLogger : ILogger
    {
        [Dependency("FirstLogger")]
        public ILogger FirstLogger { get; set; }

        [Dependency("SecondLogger")]
        public ILogger SecondLogger { get; set; }
          
        public void Log(string message)
        {
            FirstLogger.Log(message);
            SecondLogger.Log(message);
        }
    }
}
