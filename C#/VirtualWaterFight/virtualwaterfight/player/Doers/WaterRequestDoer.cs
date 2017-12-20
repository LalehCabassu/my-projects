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

namespace Player
{
    public class WaterRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private enum possibleStates
        {
            WaterRequestSent = 1,
            AckNakReceived = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private AckNak incomingReply;
        private WaterBalloon curBalloon;
        private Int16 PercentFilled;
        #endregion

        #region Public Methods
        public WaterRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.WaterManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "WaterRequestDoer";
        }

        public void SendRequest(WaterBalloon balloon, Int16 percentFilled)
        {
            WaterRequest newRequest = new WaterRequest(MyPlayer.PlayerID, balloon.BalloonID, percentFilled, balloon.Size);
            curBalloon = balloon;
            PercentFilled = percentFilled;
            
            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId = MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);

            //Update Conversation State
            //conversationState.Add(possibleStates.WaterRequestSent, (Message)newRequest);
        }

        public override void DoProtocol(Envelope message)
        {
            incomingReply = message.Message as AckNak;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    curBalloon.IncreasePercentFilled(PercentFilled);
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
            //Update Conversation State
            //conversationState.Add(possibleStates.AckNakReceived, incomingReply);
        }

        #endregion

        
    }
}
