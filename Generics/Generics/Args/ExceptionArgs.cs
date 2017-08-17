using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Args
{
    public class ExceptionArgs : BaseArgs<Exception>
    { 
        private bool isHandled = false;
        private bool retrow = false;

        public ExceptionArgs(Exception ex, bool isHandled = false, bool retrow = false) : base(ex)
        {
            this.isHandled = isHandled;
            this.retrow = retrow;
        }

        public bool IsHandled
        {
            get
            {
                return isHandled;
            }
            set
            {
                isHandled = value;
            }
        }

        public bool Retrow
        {
            get
            {
                return retrow;
            }
            set
            {
                retrow = value;
            }
        }
    }
}
