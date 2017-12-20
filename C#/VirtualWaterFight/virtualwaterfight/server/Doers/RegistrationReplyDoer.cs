using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using log4net;
using log4net.Config;

using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace Server
{
    public class RegistrationReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        #endregion

        #region Public Methods
        public RegistrationReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
        }

        public override string ThreadName()
        {
            return "RegistrationReplyDoer";
        }
        
        public void DoProtocol(Envelope message, FightManagerConversation conversation)
        {
            // Unpack the incoming message
            IPEndPoint targetEP = message.SendersEP;
            RegistrationRequest incomingRequest = (RegistrationRequest)message.Message;
            
            // Process the message
            Player newPlayer = new Player(incomingRequest.Name, incomingRequest.Age, incomingRequest.Gender, incomingRequest.Location);
            newPlayer.PlayerID = MyFightManager.AddNewPlayer(targetEP, newPlayer);
            
            //Pack reply -> AckNak
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");
            MessageNumber.LocalProcessId = newPlayer.PlayerID;
            newReply.ConversationId = MessageNumber.Create();
            newReply.MessageNr = newReply.ConversationId;
            conversation.SendReply((Message)newReply);
        }

        #endregion

    }
}
