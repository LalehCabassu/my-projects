using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

using Common.Messages;

namespace Common.Communicator
{

    
    //Took from class notes
    public class MessageQueue
    {
        //private string queueName;
        ConcurrentQueue<Envelope> myQueue = new ConcurrentQueue<Envelope>();

        //private MonitoringStat monitoringStats;
        //private MonitoringStat tmpStats;

        private AutoResetEvent stateChanged = new AutoResetEvent(false);

        //public event MonitoringStatHandler MonitorEvent;

        //public MessageQueue(string name)
        public MessageQueue()
        {/*
            queueName = name;
            monitoringStats = new MonitoringStat(string.Format("{0} Queue Size", queueName), 0, false);
            SendMonitoringStates();
          */ 
        }

        public AutoResetEvent StateChanged
        {
            get { return stateChanged; }
        }

        /*
        public string StatName
        {
            get { return monitoringStats.Name; }
        }
        */

        public void Enqueue(Envelope task)
        {
            if (task != null)
            {
                myQueue.Enqueue(task);
                /*
                monitoringStats.Value = myQueue.Count;
                SendMonitoringStates();
                StateChanged.Reset();
                 */ 
            }
        }

        public Envelope Dequeue()
        {
            Envelope result;

            if (!myQueue.TryDequeue(out result))
                result = null;
            else
            {
                //monitoringStats.Value = myQueue.Count;
                //SendMonitoringStates();
                StateChanged.Reset();
            }

            return result;
        }

        public int Length()
        {
            return myQueue.Count;
        }
        
        /*
        private void SendMonitoringStates()
        {
            if (MonitorEvent!=null)
            {
                tmpStats = new MonitoringStat(monitoringStats);
                MonitorEvent(tmpStats);
            }
        }
        /*
        public Queue<Envelope> queue;
        
        public object myLock = new object();

        public MessageQueue()
        {
            queue = new Queue<Envelope>();
        }

        

        public Envelope Dequeue()
        {
            Envelope task = null;
            lock (myLock)
            {
                task = queue.Dequeue();
            }
            return task;
        }

        public void Enqueue(Envelope task)
        {
            lock (myLock)
            {
                queue.Enqueue(task);
            }
        }
         * */
    }
     
}
