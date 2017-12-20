using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

using Common.Messages;
using Common.Communicator;

namespace Common.Threading
{
    public class ManagerConversation
    {
        public enum PossibleStates
        {
            RequestReceived = 1, 
            ReplySent = 2,
            FirstAckNakReceived = 3,
            Finished = 4
        }
        public MessageNumber ConversationID;
        public PossibleStates State;
        public Message Request;
        public Message PlayerReply;
        public Message ManagersReply;
        public DateTime LastUpdateTime;
        public static Int16 RetryLimit = 3;
        public static Int16 Timeout = 10000;
        public Int16 NumberOfRetry = 0;
        public IPEndPoint PlayerEP;
        public IPEndPoint FirstManagerEP;
        public IPEndPoint SecondManagerEP;
        public Timer myTimer = null; 

        private Communicator.Communicator myCommunicator;
        
        public ManagerConversation(Envelope request, Communicator.Communicator communicator, IPEndPoint firstManager, IPEndPoint secondManager)
        {
            Request = request.Message;
            ConversationID = request.Message.ConversationId;
            myCommunicator = communicator;
            FirstManagerEP = firstManager;
            SecondManagerEP = secondManager;
            PlayerEP = request.SendersEP;
            State = PossibleStates.RequestReceived;
        }

        public void SendReply(Message playerMessage, Message managerMessage)
        {
            PlayerReply = playerMessage;
            ManagersReply = managerMessage;
            NumberOfRetry = 0;
            myTimer = new Timer(ResendReply, null, 0, Timeout);
        }

        private void ResendReply(object state)
        {
            Envelope reply;
            if (State != PossibleStates.Finished && NumberOfRetry < RetryLimit)
            {
                if (PlayerReply != null)
                {
                    reply = Envelope.CreateOutgoingEnvelope(PlayerReply, PlayerEP);
                    myCommunicator.Send(reply);
                }
                if (ManagersReply != null)
                {
                    reply = Envelope.CreateOutgoingEnvelope(ManagersReply, FirstManagerEP);
                    myCommunicator.Send(reply);

                    reply = Envelope.CreateOutgoingEnvelope(ManagersReply, SecondManagerEP);
                    myCommunicator.Send(reply);
                }
                State = PossibleStates.ReplySent;
                LastUpdateTime = DateTime.Now;
                NumberOfRetry++;
            }
            else
            {
                if (myTimer != null)
                {
                    myTimer.Dispose();
                    myTimer = null;
                }
            }
        }

        public void SendReplyOnce()
        {
            //When a duplicate request received
            if (IsFinished())
            {
                Envelope reply = Envelope.CreateOutgoingEnvelope(PlayerReply, PlayerEP);
                myCommunicator.Send(reply);
            }
            else if(NumberOfRetry > RetryLimit)
            {
                NumberOfRetry = 0;
                SendReply(PlayerReply, null);
            }
        }
        
        public void ReceiveAckNak(IPEndPoint senderEP, bool areTwoManagers)
        {
            if (senderEP == FirstManagerEP || senderEP == SecondManagerEP)
            {
                if (areTwoManagers)
                {
                    if (State == PossibleStates.ReplySent)
                    {
                        State = PossibleStates.FirstAckNakReceived;
                        LastUpdateTime = DateTime.Now;
                    }
                    else if (State == PossibleStates.FirstAckNakReceived)
                    {
                        State = PossibleStates.Finished;
                        LastUpdateTime = DateTime.Now;
                    }

                }
                else
                {
                    if (State == PossibleStates.ReplySent)
                    {
                        State = PossibleStates.Finished;
                        LastUpdateTime = DateTime.Now;
                    }
                }
            }
        }

        public bool IsFinished()
        {
            if (State == PossibleStates.Finished)
                return true;
            return false;
        }

        public bool IsReplyExist()
        {
            if (State == PossibleStates.ReplySent)
                return true;
            return false;
        }

        public bool IsDuplicateMessage(Envelope message)
        {
            if (message.Message.ConversationId == ConversationID && message.Message.MessageNr == Request.MessageNr)
                return true;
            return false;
        }

    }
}
