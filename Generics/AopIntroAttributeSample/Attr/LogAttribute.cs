using AopIntroAttributeSample.Container;
using AopIntroAttributeSample.Interception;
using AopIntroAttributeSample.Logger;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Attr
{
    public class LogAttribute : InterceptAttribute, IPreVoidInterception, IPostVoidInterception
    {
        private readonly ILogger logger;

        public LogAttribute()
        { 
            logger = ContainerContext.Resolve<ILogger>( );
        }

        public void OnPre(PreInterceptArgs e)
        {
            StringBuilder sbLogMessage = new StringBuilder(); 
            sbLogMessage.AppendLine(e.ToString());

            logger.Log(string.Format("Log başladı : {0}", sbLogMessage.ToString()));
        }

        public void OnPost(PostInterceptArgs e)
        {
            StringBuilder sbLogMessage = new StringBuilder(); 
            sbLogMessage.AppendLine(e.ToString());

            logger.Log(string.Format("Log bitti : {0}", sbLogMessage.ToString()));

        }
    }
}
