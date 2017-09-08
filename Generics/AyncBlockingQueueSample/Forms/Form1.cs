using AyncBlockingQueueSample.Message;
using Generics.Args;
using Generics.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AyncBlockingQueueSample.Forms
{
    public partial class Form1 : BaseForm
    {

        public Form1()
        {
            InitializeComponent();
        }

        private AsyncBlockingQueue<IMessage> messageQueue;
        private AsyncBlockingQueue<Action> databaseCommandQueue;
        private void Form1_Load(object sender, EventArgs e)
        {
            messageQueue = new MessageQueue();
            messageQueue.OnException += _messageQueue_OnException;
            messageQueue.OnDequeue += _messageQueue_OnDequeue;
            InitIncomingThreads();

            databaseCommandQueue = new DatabaseCommandQueue();
            databaseCommandQueue.OnDequeue += DatabaseCommandQueue_OnDequeue;
        }

        private void DatabaseCommandQueue_OnDequeue(object arg1, QueueEventArgs<Action> arg)
        {
            arg.Entry();
        }

        private void _messageQueue_OnException(object sender, QueueEventArgs<IMessage> arg)
        {
            Run(() =>
            {
                WriteError($"Messeque error : Desc: {arg.Exception.ToString()}");
            });

        }

        private void _messageQueue_OnDequeue(object sender, QueueEventArgs<IMessage> arg)
        {
            Run(() =>
            {
                WriteInfo(arg.Entry.ToString());
            });
        }

        private void InitIncomingThreads()
        {
            Task.Factory.StartNew(() =>
            {

                for (int i = 0; i < 1000; i++)
                {
                    Thread.Sleep(1000);
                    if (i % 2 == 0)
                        messageQueue.Enqueue(new SimpleMessage(i, "nolu mesaj işlendi. ilk thread"));
                }
            });
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    Thread.Sleep(1000);
                    if (i % 2 == 1)
                        messageQueue.Enqueue(new SimpleMessage(i, "nolu mesaj işlendi. ikinci thread"));
                }
            });

            for (int i = 0; i < 1000; i++)
            {
                Task.Factory.StartNew((s) =>
                {
                    databaseCommandQueue.Enqueue(() => //POCO objeleri yerine anonymous function ekledik kuyruğumuza
                    {
                        //Burası DB işlemlerini gerçekleştireceğimiz kısım

                        Thread.Sleep(100);
                        WriteInfo($"{s} nolu db işlemi gerçekleştirildi");//burada i değişkenini kullanamayız. Biz i değişkenini kullanana kadar Thread.Sleep yüzünden değişkenimiz çoktan 1000 olacaktır.

                    });
                }, i);
            }
        }

        private void WriteInfo(string text)
        {
            BeginInvoke(lblInfo, () => { lblInfo.Items.Add("Info  : " + text); });
        }

        private void WriteError(string text)
        {
            BeginInvoke(lblInfo, () => { lblInfo.Items.Add("Error : " + text); });
        }
    }

    public class DatabaseCommandQueue : AsyncBlockingQueue<Action>
    {

    }
}
