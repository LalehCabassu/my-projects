using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Communicator;
using Common.Messages;

namespace Player.Conversations
{
    public class RegistrationRequestConversation : Conversation
    {
        public enum PossibleStates
        {
            RequestSent = 1, 
            Registered = 2,
            NotRegistered = 3,
            RequestResent = 4
        }
        public PossibleStates State;

        public RegistrationRequestConversation(Message message, IPEndPoint messageEP)
            : base(message, messageEP)
        {
            State = PossibleStates.RequestSent;
        }

        public void UpdateState(PossibleStates nState)
        {
            LastUpdateTime = DateTime.Now;
            State = nState;
            NumberOfRetry++;
        }

        public void UpdateState(PossibleStates nState, Envelope nMessage)
        {
            LastUpdateTime = DateTime.Now;
            if (State != nState)
            {
                State = nState;
                Message = nMessage;
                NumberOfRetry = 1;
            }
        }


    }
}
