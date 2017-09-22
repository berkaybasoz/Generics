using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopIntroAttributeSample.Interception;
using AopIntroAttributeSample.Logger;
using AopIntroAttributeSample.Container;

namespace AopIntroAttributeSample.Ex
{
    public class BasicExceptionHandler : IExceptionHandler
    {
        private readonly ILogger logger;

        public BasicExceptionHandler()
        {
            logger = ContainerContext.Resolve<ILogger>();
        }

        public void Handle(Exception ex)
        {
            logger.Log(ex.ToString());
        }

        public void Handle(ExceptionInterceptArgs arg)
        {  
            logger.Log(arg.ToString());
        }

    }
}
