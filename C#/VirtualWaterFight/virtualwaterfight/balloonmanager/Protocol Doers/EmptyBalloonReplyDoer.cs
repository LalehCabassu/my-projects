using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common;
using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace BalloonManager
{
    public class EmptyBalloonReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private BalloonManager MyBalloonManager;
        private EmptyBalloonRequest incomingRequest;
        private int newBalloonID;
        private ManagerConversation currentConversation;
        #endregion

        #region Public Methods
        public EmptyBalloonReplyDoer(Communicator communicator, BalloonManager myBalloonManager)
            : base(communicator)
        {
            MyBalloonManager = myBalloonManager;
            newBalloonID = -1;
        }

        public override string ThreadName()
        {
            return "EmptyBalloonRequestDoer";
        }

        public void DoProtocol(Envelope message, ManagerConversation conversation)
        {
            incomingRequest = message.Message as EmptyBalloonRequest;
            currentConversation = conversation;
            
            Player currentPlayer = MyBalloonManager.FindPlayer(incomingRequest.PlayerID);
            if (currentPlayer != null)
            {
                newBalloonID = MyBalloonManager.AddBalloon(incomingRequest.PlayerID, incomingRequest.Size, incomingRequest.Color);
                DoEmptyBalloonReply(true);
            }
            else
                DoEmptyBalloonReply(false);            
        }
        #endregion

        #region Private Methods
        private void DoEmptyBalloonReply(bool isValid)
        {
            EmptyBalloonReply newReply;
            if (isValid)
            {
                newReply = new EmptyBalloonReply(incomingRequest.PlayerID, newBalloonID, Reply.PossibleStatus.Valid, "Balloon");
                newReply.ConversationId = incomingRequest.ConversationId;
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                currentConversation.SendReply(newReply, newReply);
            }
            else
            {
                newReply = new EmptyBalloonReply(incomingRequest.PlayerID, newBalloonID, Reply.PossibleStatus.Invalid, "Balloon");
                newReply.ConversationId = incomingRequest.ConversationId;
                newReply.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(incomingRequest.MessageNr.SeqNumber + 1));
                currentConversation.SendReply(newReply, null);
            }
        }

        
        #endregion
    }
}
