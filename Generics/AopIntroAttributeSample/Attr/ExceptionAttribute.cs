using AopIntroAttributeSample.Container;
using AopIntroAttributeSample.Ex;
using AopIntroAttributeSample.Interception;
using AopIntroAttributeSample.Logger;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Attr
{
    public class ExceptionAttribute : InterceptAttribute, IExceptionInterception
    {
        private readonly IExceptionHandler handler; 

        public ExceptionAttribute( )
        { 
            handler = ContainerContext.Resolve<IExceptionHandler>();
        }

        public void OnException(ExceptionInterceptArgs e)
        {
            handler.Handle(e.Ex);

            e.Ex = new ApplicationException("Şuan işleminizi gerçekleştiremiyoruz.");
        }
    }
}
