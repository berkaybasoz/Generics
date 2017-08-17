using Generics.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ItemEventArgSample
{
    public class Program
    {
        public class BaseForm 
        {
            public delegate void ItemEventHandler<T>(BaseForm sender, ItemEventArgs<T> e);
            public event ItemEventHandler<Exception> OnException;
  
            protected virtual void HandleEx(ItemEventArgs<Exception> e)
            {
                OnException?.Invoke(this, e);
            }

            public void Run()
            {
                try
                {
                    RunThrowableStuff();
                }
                catch (Exception ex)
                {
                    HandleEx(ex);
                }
            }

            private void RunThrowableStuff()
            {
                throw new ApplicationException("I don't want to work today");
            }
        }
        static void Main(string[] args)
        {
            BaseForm frmSample = new BaseForm();
            frmSample.OnException += FrmSample_OnException;
            frmSample.Run();
            Console.ReadLine();
        }

        private static void FrmSample_OnException(BaseForm sender, ItemEventArgs<Exception> e)
        {
            Console.WriteLine(e.Item);
        }
    }
}
