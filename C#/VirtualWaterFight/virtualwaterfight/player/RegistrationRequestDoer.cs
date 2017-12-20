using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(RegistrationRequestDoer));
        private enum possibleStates
        {
            RegistrationRequestSent = 1,
            AckNakReceived = 2
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private AckNak incomingAckNak;
        #endregion

        #region Public Methods
        public RegistrationRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            targetEP = MyPlayer.FightManagerEP;
            conversationState = new Dictionary<possibleStates, Message>();
        }

        public override string ThreadName()
        {
            return "RegistrationRequestDoer";
        }

        public void SendRequest()
        {
            log.InfoFormat("Send", ThreadName());
            string name = MyPlayer.Name;
            Int16 age = MyPlayer.Age;
            bool gender = MyPlayer.Gender;
            Location location = MyPlayer.GetCurrentLocation();
            RegistrationRequest newRequest = new RegistrationRequest(name,age , gender, location);
            base.Send((Message)newRequest, targetEP);
            
            //Update Conversation State
            conversationState.Add(possibleStates.RegistrationRequestSent, (Message)newRequest);
        }

        public override void DoProtocol(Envelope message)
        {
            // Unpacking incoming message
            if (message.SendersEP.Equals(targetEP))      // From Fight Manager
                if (conversationState.Count > 0 && conversationState.Last().Key != possibleStates.AckNakReceived)
                {
                    //Search AckNakList for the AckNak of its Registration Request
                    foreach (KeyValuePair<possibleStates, Message> kvp in conversationState)
                        if ((kvp.Key == possibleStates.RegistrationRequestSent) && (message.Message.MessageNr.SeqNumber - kvp.Value.MessageNr.SeqNumber) >= 1)
                        {
                            log.InfoFormat("DoProtocol", ThreadName());
                            incomingAckNak = (AckNak)message.Message;

                            switch (incomingAckNak.Status)
                            {
                                case Reply.PossibleStatus.Valid:
                                    log.InfoFormat("processAckNak: Valid", ThreadName());
                                    log.InfoFormat("setPlayerID", ThreadName());
                                    MyPlayer.PlayerID = incomingAckNak.ConversationId.ProcessId;
                                    break;
                                case Reply.PossibleStatus.InvalidLocation:
                                    // TODO: Notify player to choose another location
                                    log.InfoFormat("processAckNak: InvalidLocation", ThreadName());
                                    break;
                                case Reply.PossibleStatus.Invalid:
                                    log.InfoFormat("processAckNak: Invalid", ThreadName());
                                    break;
                            }
                            //Update Conversation State
                            conversationState.Add(possibleStates.AckNakReceived, incomingAckNak);
                            break;
                        }
                }
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
