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
    public class PlayerLocationReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private PlayerSettings settings;
        private enum possibleStates
        {
            LocationRequestReceived = 1,
            LocationReplySent = 2,
            Deregistered = 3
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private PlayerLocationRequest incomingRequest;
        private AckNak incomingAckNak;
        #endregion

        #region Public Methods
        public PlayerLocationReplyDoer(Communicator communicator, Player myPlayer) 
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "PlayerLocationReplyDoer";
        }

        public void SendRequest()
        {
            PlayerLocationReply newReply = new PlayerLocationReply(MyPlayer.PlayerID, MyPlayer.GetCurrentLocation(),
                                                                    Reply.PossibleStatus.Valid, "Location");
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(MyPlayer.PlayerID, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.LocationReplySent, (Message)newReply);
        }
        #endregion

        #region Private Methods
        protected override void Process()
        {
            while (keepGoing)
            {
                Thread.Sleep(0);
            }
        }
        #endregion
    }
}
