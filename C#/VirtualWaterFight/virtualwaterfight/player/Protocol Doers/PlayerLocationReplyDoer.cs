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
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public PlayerLocationReplyDoer(Communicator communicator, Player myPlayer) 
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
        }

        public override string ThreadName()
        {
            return "PlayerLocationReplyDoer";
        }

        public override void DoProtocol(Envelope message)
        {
            {
                PlayerLocationRequest incomingRequest = message.Message as PlayerLocationRequest;
                PlayerLocationReply newReply = new PlayerLocationReply(MyPlayer.PlayerID, MyPlayer.GetCurrentLocation(),
                                                                        Reply.PossibleStatus.Valid, "Location");
                newReply.ConversationId = incomingRequest.ConversationId;
                newReply.MessageNr = MessageNumber.Create(MyPlayer.PlayerID, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                Send((Message)newReply, targetEP);
            }
        }
        #endregion
    }
}
