using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;

using Common;
using Common.Messages;
using Common.Threading;
using Common.Communicator;
using Objects;

namespace WaterManager
{
    public class WaterReplyDoer : Doer
    {
        #region Data Members
        private WaterManager MyWaterManager;
        private WaterRequest incomingRequest;
        private AckNak playerReply = null;
        private WaterReply managerReply = null;
        private Int16 amountOfWater;
        private ManagerConversation currentConversation;
        #endregion
        
        #region Public Methods
        public WaterReplyDoer(Communicator communicator, WaterManager myWaterManager)
            : base(communicator)
        {
            MyWaterManager = myWaterManager;
        }

        public override string ThreadName()
        {
            return "WaterReplyDoer";
        }

        public void DoProtocol(Envelope message, ManagerConversation conversation)
        {
            currentConversation = conversation;
            incomingRequest = message.Message as WaterRequest;
            
            amountOfWater = MyWaterManager.FillBalloon(incomingRequest.PlayerID, incomingRequest.BalloonID, 
                                                        incomingRequest.PercentFilled, incomingRequest.BalloonSize);
            if (amountOfWater != 0)
            {
                DoAckNakReply(true, 1);
                DoWaterReply(2);
            }
            else
                DoAckNakReply(false, 1);
            currentConversation.SendReply(playerReply, managerReply);
        }
        #endregion

        #region Private Methods
        private void DoAckNakReply(bool isValid, int addToSeqNum)
        {
            //AckNak newReply;
            if(isValid)
                playerReply = new AckNak(Reply.PossibleStatus.Valid, "Water");
            else
                playerReply = new AckNak(Reply.PossibleStatus.Invalid, "Water");

            playerReply.ConversationId = incomingRequest.ConversationId;
            playerReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            //currentConversation.SendReply(newReply, null);
            
        }

        private void DoWaterReply(int addToSeqNum)
        {
            //WaterReply newReply;
            managerReply = new WaterReply(incomingRequest.PlayerID, incomingRequest.BalloonID, amountOfWater, Reply.PossibleStatus.Valid, "Water");
            managerReply.ConversationId = incomingRequest.ConversationId;
            managerReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + addToSeqNum));

            //currentConversation.SendReply(null, newReply);
        }
        #endregion
        
    }
}
