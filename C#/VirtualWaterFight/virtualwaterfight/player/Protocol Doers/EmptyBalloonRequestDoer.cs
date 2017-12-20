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
        private PlayerConversationList myConversationList; 
        private WaterBalloon newBalloon;
        #endregion

        #region Public Methods
        public EmptyBalloonRequestDoer(Communicator communicator, Player myPlayer, PlayerConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            myConversationList = conversationList;
        }

        public override string ThreadName()
        {
            return "EmptyBalloonRequestDoer";
        }

        public void SendRequest(WaterBalloon.PossibleSize size, WaterBalloon.PossibleColor color)
        {
            newBalloon = new WaterBalloon(size, color); 
            
            EmptyBalloonRequest newRequest = new EmptyBalloonRequest(MyPlayer.PlayerID, size, color);
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId = MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            PlayerConversation currentConversation = myConversationList.AddNewConversation(newRequest, MyPlayer.BalloonManagerEP);
            currentConversation.SendRequest();
        }

        public override void DoProtocol(Envelope message)
        {
            EmptyBalloonReply incomingReply = message.Message as EmptyBalloonReply;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    newBalloon.SetID(incomingReply.BalloonID);
                    MyPlayer.GetNewBalloon(newBalloon);
                    break;
                case Reply.PossibleStatus.Invalid:
                    newBalloon = null;
                    break;
            }
        }

        #endregion
    }
}
