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
        private PlayerConversationList myConversationList; 
        private WaterBalloon curBalloon;
        private Int16 PercentFilled;
        #endregion

        #region Public Methods
        public WaterRequestDoer(Communicator communicator, Player myPlayer, PlayerConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            myConversationList = conversationList;
        }

        public override string ThreadName()
        {
            return "WaterRequestDoer";
        }

        public void SendRequest(int balloonID, Int16 percentFilled)
        {
            curBalloon = MyPlayer.FindBalloon(balloonID);
            PercentFilled = percentFilled;

            if (curBalloon != null)
            {
                WaterRequest newRequest = new WaterRequest(MyPlayer.PlayerID, curBalloon.BalloonID, percentFilled, curBalloon.Size);
                MessageNumber.LocalProcessId = MyPlayer.PlayerID;
                newRequest.ConversationId = MessageNumber.Create();
                newRequest.MessageNr = newRequest.ConversationId;

                PlayerConversation currentConversation = myConversationList.AddNewConversation(newRequest, MyPlayer.WaterManagerEP);
                currentConversation.SendRequest();
            }
        }

        public override void DoProtocol(Envelope message)
        {
            AckNak incomingReply = message.Message as AckNak;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    curBalloon.IncreasePercentFilled(PercentFilled);
                    break;
                case Reply.PossibleStatus.Invalid:
                    curBalloon = null;
                    PercentFilled = 0;
                    break;
            }
        }
        #endregion
    }
}
