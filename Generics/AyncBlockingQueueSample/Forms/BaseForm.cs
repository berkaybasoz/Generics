using Generics.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AyncBlockingQueueSample.Forms
{
    public class BaseForm : Form
    {
        public delegate void ItemEventHandler<T>(BaseForm sender, ItemEventArgs<T> e);
        public event ItemEventHandler<Exception> OnException;


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

        public void Run(Action action, Func<BaseForm, ExceptionArgs, ExceptionArgs> onActionException = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {

                ExceptionArgs args = onActionException?.Invoke(this, new ExceptionArgs(ex));

                if (args == null)
                {
                    HandleEx(ex);
                }
                else
                {
                    if (!args.IsHandled)
                    {
                        HandleEx(ex);
                    }

                    if (args.Retrow)
                    {
                        throw;
                    }
                }

            }
        }

        protected virtual void HandleEx(ItemEventArgs<Exception> e)
        {
            OnException?.Invoke(this, e);
        }

        protected void BeginInvoke(Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
        protected void Invoke(Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action);
            }
            else
            {
                action();
            }
        }
        protected void RunThrowableStuff()
        {
            throw new ApplicationException("I don't want to work today");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
