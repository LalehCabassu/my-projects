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
    public class BalloonManagerReplyDoer : BalloonManagerDoer
    {
        #region Data members and Getter/Setter
        private enum possibleStates
        {
            NumberOfEmptyBalloonsRequestReceived = 1,
            BalloonManagerReplySent = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private NumberOfEmptyBalloonsRequest incomingRequest;
        private Int16 numberOfBalloon;
        #endregion

        #region Public Methods
        public BalloonManagerReplyDoer()
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
            if (base.NumberOfEmptyBalloonsRequestQueue.Length() > 0)
                return true;
            return false;
        }
        
        #endregion

        #region Private Methods
        protected override void Process()
        {
            conversationState = new Dictionary<possibleStates, Message>();
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

        private NumberOfEmptyBalloonsRequest RetrieveRequest()
        {
            Envelope incomingEnvelope;
            if (MessageAvailable())
            {
                incomingEnvelope = base.EmptyBalloonRequestQueue.Dequeue();
                targetEP = incomingEnvelope.SendersEP;

                //Update Conversation State
                conversationState.Add(possibleStates.NumberOfEmptyBalloonsRequestReceived, incomingEnvelope.Message);
                return (NumberOfEmptyBalloonsRequest)incomingEnvelope.Message;
            }
            return null;
        }

        private void processRequest()
        {
            Player currentPlayer = base.MyBalloonManager.FindPlayer(incomingRequest.PlayerID);
            if (currentPlayer == null)
                numberOfBalloon = 0;
            else
                numberOfBalloon = currentPlayer.NumberOfCurrentBalloons;
        }
        private void SendReply()
        {
            EmptyBalloonReply newReply;
            if(numberOfBalloon > 0 && numberOfBalloon <= 4)
                newReply = new EmptyBalloonReply(incomingRequest.PlayerID,numberOfBalloon, Reply.PossibleStatus.Valid, "Number of Empty Balloon");
            else
                newReply = new EmptyBalloonReply(incomingRequest.PlayerID,numberOfBalloon, Reply.PossibleStatus.Invalid, "Number of Empty Balloon");
            
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1)); 
            base.Send((Message)newReply, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.BalloonManagerReplySent, (Message)newReply);
        }
        #endregion
    }
}
