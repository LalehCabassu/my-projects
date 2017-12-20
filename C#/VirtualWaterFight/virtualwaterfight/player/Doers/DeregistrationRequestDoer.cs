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
        private enum possibleStates
        {
            DeregistrationRequestSent = 1,
            AckNakReceived = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public DeregistrationRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
            conversationState = new Dictionary<possibleStates,Message>();

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
            
            base.Send((Message)newRequest, targetEP);
            
            //Update Conversation State
            conversationState.Add(possibleStates.DeregistrationRequestSent, (Message)newRequest);
        }

        public override void DoProtocol(Envelope message)
        {
            AckNak incomingAckNak = (AckNak) message.Message;
            // Unpacking incoming message
            if (message.SendersEP.Equals(targetEP))      // From Fight Manager
                if (conversationState.Count > 0 && conversationState.Last().Key != possibleStates.AckNakReceived)
                {
                    //Search AckNakList for the AckNak of its Registration Request
                    foreach (KeyValuePair<possibleStates, Message> kvp in conversationState)
                        if ((incomingAckNak.ConversationId == kvp.Value.ConversationId))
                        {
                            switch (incomingAckNak.Status)
                            {
                                case Reply.PossibleStatus.Valid:
                                    MyPlayer.removePlayerID();
                                    break;
                                case Reply.PossibleStatus.Invalid:
                                    // TODO: ????
                                    break;
                            }
                            //Update Conversation State
                            conversationState.Add(possibleStates.AckNakReceived, incomingAckNak);
                            break;
                        }
                }
        }
        #endregion

        
    }
}
