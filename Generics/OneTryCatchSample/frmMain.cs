using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneTryCatchSample
{
    public partial class frmMain : BaseForm
    {
        public frmMain()
        {
            InitializeComponent();
            OnException += FrmMain_OnException;
        }
        private void FrmMain_OnException(BaseForm sender, Generics.Args.ItemEventArgs<Exception> e)
        {
            BeginInvoke(lblError, () => { lblError.Text = e.Item.ToString(); });
        }


        private void btnRun_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                RunThrowableStuff();
            });
        }
        private void btnRun1_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                RunThrowableStuff();
            });
        }

        private void btnRun2_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                RunThrowableStuff();
            });
        } 
    }
}
