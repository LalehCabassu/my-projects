using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Common.Messages;
using Common.Threading;
using log4net;
using log4net.Config;

namespace Common.Communicator
{
    public class Communicator : BackgroundThread
    {
        #region Data members and Getters/Senders
        private static readonly ILog log = LogManager.GetLogger(typeof(Communicator));

        private MessageQueue outgoingQueue;
        public MessageQueue incomingQueue;
        private UdpClient myUdpClient;
        private IPEndPoint myIPEndPoint;
        #endregion

        #region Public Methods
        public Communicator()
        {
        }

        public override string ThreadName()
        {
            return "Communicator";
        }

        public override void Start()
        {
            outgoingQueue = new MessageQueue();
            incomingQueue = new MessageQueue();
            SetupClient();
            base.Start();
        }

        public IPEndPoint LocalEP
        {
            get { return myIPEndPoint; }
            set { myIPEndPoint = value; SetupClient(); }
        }

        public bool IncomingAvailable()
        {
            // For Doer to check if any message available or not
            if (incomingQueue.Length() > 0)
            {
                log.InfoFormat("IncomingAvailable", ThreadName());
                return true;
            }
            return false;
        }

        public Envelope Receive()
        {
            // For Doer
            if (incomingQueue.Length() > 0)
            {
                log.InfoFormat("Receive", ThreadName());
                return incomingQueue.Dequeue();
            }
            return null;
        }


        public void Send(Envelope outgoingEnvelope)
        {
            // For Doer
            if (outgoingEnvelope != null && outgoingEnvelope.ReceiversEP != null && outgoingEnvelope.Message != null)
            {
                log.InfoFormat("Send", ThreadName());
                outgoingQueue.Enqueue(outgoingEnvelope);
            }
            else
                throw new ApplicationException("Try to send a NULL message");
        }
        #endregion

        #region Private Methods
        protected override void Process()
        {
            while (keepGoing)
            {
                if (myUdpClient.Available > 0)
                    ReceiveMessage();
                if (outgoingQueue.Length() > 0)
                    SendMessage();
                Thread.Sleep(0);
            }
        }

        private void SendMessage()
        {
            try
            {
                log.InfoFormat("SendMessage", ThreadName());
                Envelope outgoingEnvelope = outgoingQueue.Dequeue();
                ByteList myByteList = new ByteList();
                outgoingEnvelope.Message.Encode(myByteList);
                byte[] bytes = myByteList.GetBytes(myByteList.Length);
                myUdpClient.Send(bytes, bytes.Length, outgoingEnvelope.ReceiversEP);
            }
            catch (Exception e)
            {
                log.InfoFormat("SendMessage", ThreadName());
                Console.WriteLine(e.Message);
            }
        }

        private void ReceiveMessage()
        {
            try
            {
                log.InfoFormat("ReceiveMessage", ThreadName());
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = myUdpClient.Receive(ref remoteEP);
                ByteList myByteList = new ByteList(bytes);
                Envelope incomingMessage = Envelope.CreateIncomingEnvelope(Message.Create(myByteList), remoteEP);
                incomingQueue.Enqueue(incomingMessage);
            }
            catch (Exception e)
            {
                log.Fatal("Aborted exception caught - Communicator - ReceiveMessage", e);
                Console.WriteLine(e.Message);
            }
        }

        private void SetupClient()
        {
            if (myUdpClient != null)
                myUdpClient.Close();
            if (myIPEndPoint == null || myIPEndPoint.ToString() == "0.0.0.0:0")
                myIPEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
            myUdpClient = new UdpClient(myIPEndPoint);
            myIPEndPoint = myUdpClient.Client.LocalEndPoint as IPEndPoint;
        }
        #endregion
    }
}
