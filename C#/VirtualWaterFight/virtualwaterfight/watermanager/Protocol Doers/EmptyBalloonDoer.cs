﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace WaterManager
{
    public class EmptyBalloonDoer : Doer
    {
        #region Data members and Getter/Setter
        private WaterManager MyWaterManager;
        #endregion

        #region Public Methods
        public EmptyBalloonDoer(Communicator communicator, WaterManager myWaterManager)
            : base(communicator)
        {
            MyWaterManager = myWaterManager;
        }

        public override string ThreadName()
        {
            return "EmptyBalloonDoer";
        }
        
        public override void DoProtocol(Envelope message)
        {
            EmptyBalloonReply incomingReply = message.Message as EmptyBalloonReply;
            switch (incomingReply.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyWaterManager.AddBalloon(incomingReply.PlayerID, incomingReply.BalloonID);
                    break;
                case Reply.PossibleStatus.Invalid:
                    // TODO: ????
                    break;
            }
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Balloon");
            newReply.ConversationId = incomingReply.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingReply.ConversationId.ProcessId, Convert.ToInt16(incomingReply.MessageNr.SeqNumber + 1));
            Send(newReply, MyWaterManager.BalloonManagerEP);
        }
        #endregion
    }
}