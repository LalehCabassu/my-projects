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
using Player.Conversations;
using Objects;

namespace Player
{
    public class RegistrationRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        //private IPEndPoint targetEP;
        //private RegistrationRequest newRequest;
        private RegistrationRequestConversation myConversation;
        private System.Timers.Timer myTimer;
        #endregion

        #region Public Methods
        public RegistrationRequestDoer(Communicator communicator, Player myPlayer, ConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            //targetEP = MyPlayer.FightManagerEP;
            MyConversationList = conversationList;
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
            base.Send((Message)newRequest, MyPlayer.FightManagerEP);

            myConversation = new RegistrationRequestConversation((Message)newRequest, MyPlayer.FightManagerEP);
            MyConversationList.AddNewConversation(myConversation);

            // Handle message lost
            while (myConversation.State != RegistrationRequestConversation.PossibleStates.Registered && myConversation.NumberOfRetry <= myConversation.RetryLimit)
            {
                myTimer = new System.Timers.Timer(myConversation.Timeout);
                myTimer.Elapsed += new ElapsedEventHandler(ResendRequest);
                myTimer.Interval = myConversation.Timeout;
                myTimer.Enabled = true;
                myConversation.UpdateState(RegistrationRequestConversation.PossibleStates.RequestResent);
            }
        }

        private void ResendRequest(object source, ElapsedEventArgs e)
        {
            base.Send(myConversation.Message);
        }

        public override void DoProtocol(Envelope message)
        {
            // Handle Duplicata Messages
            if (myConversation.State == RegistrationRequestConversation.PossibleStates.RequestSent ||
                myConversation.State == RegistrationRequestConversation.PossibleStates.RequestResent)
            {
                AckNak incomingAckNak = (AckNak)message.Message;
                switch (incomingAckNak.Status)
                {
                    case Reply.PossibleStatus.Valid:
                        MyPlayer.PlayerID = incomingAckNak.ConversationId.ProcessId;
                        myConversation.UpdateState(RegistrationRequestConversation.PossibleStates.Registered, message);
                        break;
                    case Reply.PossibleStatus.Invalid:
                        myConversation.UpdateState(RegistrationRequestConversation.PossibleStates.NotRegistered, message);
                        break;
                }
            }
        }
        #endregion

    }
}
