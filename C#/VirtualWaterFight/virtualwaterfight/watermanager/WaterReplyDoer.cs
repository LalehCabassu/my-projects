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
        private enum possibleStates
        {
            WaterRequestReceived = 1,
            ReplysSent = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private WaterRequest incomingRequest;
        private IPEndPoint targetEP;
        private Int16 amountOfWater;
        #endregion
        
        #region Public Methods
        public WaterReplyDoer(Communicator communicator, WaterManager myWaterManager)
            : base(communicator)
        {
            MyWaterManager = myWaterManager;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "WaterReplyDoer";
        }

        public override void DoProtocol(Envelope message)
        {
            incomingRequest = message.Message as WaterRequest;
            targetEP = message.SendersEP;
            
            amountOfWater = MyWaterManager.FillBalloon(incomingRequest.PlayerID, incomingRequest.BalloonID, 
                                                        incomingRequest.PercentFilled, incomingRequest.BalloonSize);
            if (amountOfWater != 0)
            {
                DoAckNakReply(true);
                DoWaterReply();
            }
            else
                DoAckNakReply(false);            
        }
        #endregion

        #region Private Methods
        private void DoAckNakReply(bool isValid)
        {
            AckNak newReply;
            if(isValid)
                newReply = new AckNak(Reply.PossibleStatus.Valid, "Water");
            else
                newReply = new AckNak(Reply.PossibleStatus.Invalid, "Water");
            
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, targetEP);
        }

        private void DoWaterReply()
        {
            WaterReply newReply;
            newReply = new WaterReply(incomingRequest.PlayerID, incomingRequest.BalloonID, amountOfWater, Reply.PossibleStatus.Valid, "Water");
            
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingRequest.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, MyWaterManager.FightManagerEP);
        }

        protected override void Process()
        {
            while (keepGoing)
            {/*
                if (!suspended)
                {
                    
                }
              */ 
                Thread.Sleep(0);
               
            }
        }
        #endregion
        
    }
}
