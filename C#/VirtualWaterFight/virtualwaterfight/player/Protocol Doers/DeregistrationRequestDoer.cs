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
    public class DeregistrationRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private PlayerConversationList myConversationList;
        #endregion

        #region Public Methods
        public DeregistrationRequestDoer(Communicator communicator, Player myPlayer, PlayerConversationList conversationList)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            myConversationList = conversationList;
        }

        public override string ThreadName()
        {
            return "DeregistrationRequestDoer";
        }

        public void SendRequest()
        {
            DeregistrationRequest newRequest = new DeregistrationRequest();

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId = MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            PlayerConversation currentConversation = myConversationList.AddNewConversation(newRequest, MyPlayer.FightManagerEP);
            currentConversation.SendRequest();    
        }

        public override void DoProtocol(Envelope message)
        {
            AckNak incomingAckNak = (AckNak) message.Message;
            switch (incomingAckNak.Status)
            {
                case Reply.PossibleStatus.Valid:
                    MyPlayer.removePlayerID();
                    break;
                case Reply.PossibleStatus.Invalid:
                    break;
            }
        }
        #endregion
    }
}
