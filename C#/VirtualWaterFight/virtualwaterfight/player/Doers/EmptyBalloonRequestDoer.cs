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
    public class EmptyBalloonRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private enum possibleStates
        {
            EmptyBalloonRequestSent = 1,
            EmptyBalloonReplyReceived = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private EmptyBalloonReply incomingReply;
        private WaterBalloon newBalloon;
        #endregion

        #region Public Methods
        public EmptyBalloonRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.BalloonManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "EmptyBalloonRequestDoer";
        }

        public void SendRequest(WaterBalloon.PossibleSize size, WaterBalloon.PossibleColor color)
        {
            EmptyBalloonRequest newRequest = new EmptyBalloonRequest(MyPlayer.PlayerID, size, color);
            newBalloon = new WaterBalloon(size, color);
            
            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId = MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.EmptyBalloonRequestSent, (Message)newRequest);
        }

        public override void DoProtocol(Envelope message)
        {
            incomingReply = message.Message as EmptyBalloonReply;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    newBalloon.BalloonID = incomingReply.BalloonID;
                    MyPlayer.GetNewBalloon(newBalloon);
                    break;
                case Reply.PossibleStatus.Invalid:
                    newBalloon = null;
                    break;
            }
            //Update Conversation State
            conversationState.Add(possibleStates.EmptyBalloonReplyReceived, incomingReply);
        }

        #endregion

        
    }
}
