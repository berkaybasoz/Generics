using AopIntroAttributeSample.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Attr
{
    public class ThreadLock : InterceptAttribute, IPreVoidInterception, IPostVoidInterception
    {
        private Mutex mutex = new Mutex();
        public void OnPre(PreInterceptArgs e)
        {
            mutex.WaitOne();//Thread lock yapıyoruz
        }

        public void OnPost(PostInterceptArgs e)
        {
            mutex.ReleaseMutex();//Thread release yapıyoruz
        }

    }
}
