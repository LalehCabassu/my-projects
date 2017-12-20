using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Timers;
using log4net;
using log4net.Config;

using Common;
using Common.Communicator;
using Common.Messages;
using Common.Threading;
using Objects;

namespace Player
{
    public class RegistrationRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private PlayerConversationList myConversationList;
        private PlayerConversation currentConversation;
        #endregion

        #region Public Methods
        public RegistrationRequestDoer(Communicator communicator, Player myPlayer, PlayerConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            myConversationList = conversationList;
        }

        public override string ThreadName()
        {
            return "RegistrationRequestDoer";
        }

        public void SendRequest()
        {
            string name = MyPlayer.Name;
            Int16 age = MyPlayer.Age;
            bool gender = MyPlayer.Gender;
            Location location = MyPlayer.GetCurrentLocation();
            RegistrationRequest newRequest = new RegistrationRequest(name, age, gender, location);
            currentConversation = myConversationList.AddNewConversation(newRequest, MyPlayer.FightManagerEP);
            currentConversation.SendRequest();    
        }

        public override void DoProtocol(Envelope message)
        {
            AckNak incomingAckNak = (AckNak)message.Message;
            switch (incomingAckNak.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyPlayer.PlayerID = incomingAckNak.ConversationId.ProcessId;
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
        }
        #endregion

    }
}
