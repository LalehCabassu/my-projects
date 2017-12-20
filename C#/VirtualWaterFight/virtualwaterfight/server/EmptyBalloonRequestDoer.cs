using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace Server
{
    public class EmptyBalloonRequestDoer : BalloonManagerDoer
    {
        #region Data members and Getter/Setter
        private enum possibleStates
        {
            EmptyBalloonsRequestReceived = 1,
            AckNakSent = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private EmptyBalloonRequest incomingRequest;
        private bool flag;
        #endregion

        #region Public Methods
        public EmptyBalloonRequestDoer()
            : base()
        {
            //targetEP = new IPEndPoint(IPAddress.Loopback, base.settings.P);
        }

        public override string ThreadName()
        {
            return "EmptyBalloonRequestDoer";
        }

        public override bool MessageAvailable()
        {
            if (base.EmptyBalloonRequestQueue.Length() > 0)
                return true;
            return false;
        }
        
        #endregion

        #region Private Methods
        protected override void Process()
        {
            conversationState = new Dictionary<possibleStates, Message>();
            flag = false;
            while (keepGoing)
            {
                if (!suspended)
                {
                    incomingRequest = RetrieveRequest();
                    if (incomingRequest != null)
                    {
                        processRequest();
                        SendReply();
                    }
                }
                Thread.Sleep(0);
            }
        }

        private EmptyBalloonRequest RetrieveRequest()
        {
            Envelope incomingEnvelope;
            if (MessageAvailable())
            {
                incomingEnvelope = base.EmptyBalloonRequestQueue.Dequeue();
                targetEP = incomingEnvelope.SendersEP;

                //Update Conversation State
                conversationState.Add(possibleStates.EmptyBalloonsRequestReceived, incomingEnvelope.Message);
                return (EmptyBalloonRequest)incomingEnvelope.Message;
            }
            return null;
        }

        private void processRequest()
        {
            WaterBalloon newBalloon = new WaterBalloon(incomingRequest.Size, incomingRequest.Color);
            Player currentPlayer = base.MyBalloonManager.FindPlayer(incomingRequest.PlayerID);
            if (currentPlayer == null)
            {
                currentPlayer = new Player(incomingRequest.PlayerID);
                base.MyBalloonManager.AddNewPlayer(targetEP, currentPlayer);
            }
            flag = currentPlayer.GetNewBalloon(newBalloon);

        }
        private void SendReply()
        {
            AckNak newReply;
            if(flag)
                 newReply = new AckNak(Reply.PossibleStatus.Valid, "Empty Balloon Request");
            else
                newReply = new AckNak(Reply.PossibleStatus.Invalid, "Empty Balloon Request");

            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1)); 
            base.Send((Message)newReply, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.AckNakSent, (Message)newReply);
        }
        #endregion

    }
}
