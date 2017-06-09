using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSHLib
{
    public class BackgroundWorkHandler
    {
        public virtual void SetupBW(ref BackgroundWorker bw, bool WorkerSupportsCancellation, bool WorkerReportsProgress)
        {
            if (WorkerSupportsCancellation == true)
            {
                bw.WorkerSupportsCancellation = true;
            }

            if (WorkerReportsProgress == true)
            {
                bw.WorkerReportsProgress = true;
            }
        }

        public virtual DoWorkEventHandler DoWork(Action action)
        {
            void DoWorkFunction(object sender, DoWorkEventArgs e)
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                while (true)
                {
                    if (worker.WorkerSupportsCancellation && worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        action();
                        Thread.Sleep(500);
                    }
                }
            }

            return new DoWorkEventHandler(DoWorkFunction);
        }
    }
}
