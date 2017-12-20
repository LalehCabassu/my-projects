using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

using Common.Communicator;
using Common.Messages;

namespace Server
{
    public class FightManagerConversation
    {
        public enum PossibleStates
        {
            RequestReceived = 1, 
            ReplySent = 2,
            AckNakBMReceived = 3,
            AckNakWMReceived = 4,
            Finished = 5
        }
        public MessageNumber ConversationID;
        public PossibleStates State;
        public Message Request;
        public Message Reply;
        public DateTime LastUpdateTime;
        public Int16 RetryLimit = 3;
        public Int16 Timeout = 10000;
        public Int16 NumberOfRetry = 0;
        public IPEndPoint PlayerEP;
        public IPEndPoint BalloonManagerEP;
        public IPEndPoint WaterManagerEP;

        private Timer myTimer = null;
        private Communicator myCommunicator;
        
        public FightManagerConversation(Envelope request, Communicator communicator, IPEndPoint balloonManager, IPEndPoint waterManager)
        {
            Request = request.Message;
            ConversationID = request.Message.ConversationId;
            myCommunicator = communicator;
            BalloonManagerEP = balloonManager;
            WaterManagerEP = waterManager;
            PlayerEP = request.SendersEP;
            State = PossibleStates.RequestReceived;
        }

        public void SendReply(Message message)
        {
            Envelope reply;
            if (State == PossibleStates.RequestReceived)
            {
                reply = Envelope.CreateOutgoingEnvelope(message, PlayerEP);
                myCommunicator.Send(reply);

                reply = Envelope.CreateOutgoingEnvelope(message, BalloonManagerEP);
                myCommunicator.Send(reply);

                reply = Envelope.CreateOutgoingEnvelope(message, WaterManagerEP);
                myCommunicator.Send(reply);
                
                LastUpdateTime = DateTime.Now;
                State = PossibleStates.ReplySent;
                Reply = message;
                NumberOfRetry = 1;
                myTimer = new Timer(ResendReply, null, 30000, 10000);
            }
        }

        private void ResendReply(object state)
        {
            Envelope reply;
            if(State != PossibleStates.Finished && NumberOfRetry <= RetryLimit)
            {
                reply = Envelope.CreateOutgoingEnvelope(Reply, PlayerEP);
                myCommunicator.Send(reply);

                reply = Envelope.CreateOutgoingEnvelope(Reply, BalloonManagerEP);
                myCommunicator.Send(reply);

                reply = Envelope.CreateOutgoingEnvelope(Reply, WaterManagerEP);
                myCommunicator.Send(reply);

                LastUpdateTime = DateTime.Now;
                NumberOfRetry++;
            }
        }

        public void ReceiveAckNak(IPEndPoint senderEP)
        {
            if (senderEP == BalloonManagerEP)
            {
                LastUpdateTime = DateTime.Now;
                if (State == PossibleStates.ReplySent)
                    State = PossibleStates.AckNakBMReceived;
                else if (State == PossibleStates.AckNakWMReceived)
                    State = PossibleStates.Finished;
            }
            else if (senderEP == WaterManagerEP)
            {
                LastUpdateTime = DateTime.Now;
                if (State == PossibleStates.ReplySent)
                    State = PossibleStates.AckNakWMReceived;
                else if (State == PossibleStates.AckNakBMReceived)
                    State = PossibleStates.Finished;
            }
        }

        public bool IsDuplicateMessage(Envelope message)
        {
            if (message.Message.ConversationId == ConversationID && message.Message.MessageNr == Request.MessageNr)
                return true;
            return false;
        }

        public bool IsOldMessage(Envelope message)
        {
            if (message.Message.ConversationId == ConversationID && message.Message.MessageNr < Request.MessageNr)
                return true;
            return false;
        }
    }
}
