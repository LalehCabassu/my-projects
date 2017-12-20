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
        private static readonly ILog log = LogManager.GetLogger(typeof(RegistrationReplyDoer));
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
        
        public override void DoProtocol(Envelope message)
        {
            log.InfoFormat("DoProtocol", ThreadName());
            
            // Unpack the incoming message
            IPEndPoint targetEP = message.SendersEP;
            RegistrationRequest incomingRequest = (RegistrationRequest)message.Message;
            
            // Process the message
            Player newPlayer = new Player(incomingRequest.Name, incomingRequest.Age, incomingRequest.Gender, incomingRequest.Location);
            //Int16 playerID = MyFightManager.AddNewPlayer(targetEP, newPlayer);
            newPlayer.PlayerID = MyFightManager.AddNewPlayer(targetEP, newPlayer);
            
            //Pack reply -> AckNak
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Registered");

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = newPlayer.PlayerID;  
            newReply.ConversationId = MessageNumber.Create();
            newReply.MessageNr = newReply.ConversationId;
            base.Send((Message)newReply, targetEP);
            newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, MyFightManager.BalloonManagerEP);
            newReply.MessageNr = MessageNumber.Create(message.Message.ConversationId.ProcessId, Convert.ToInt16(message.Message.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, MyFightManager.WaterManagerEP);
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
