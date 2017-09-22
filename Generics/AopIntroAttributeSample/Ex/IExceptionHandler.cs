using AopIntroAttributeSample.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Ex
{
    public interface IExceptionHandler
    {
        void Handle(Exception ex);
        void Handle(ExceptionInterceptArgs arg);
    }
}
