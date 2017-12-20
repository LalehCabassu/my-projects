using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Communicator;
using Common.Messages;

namespace Server.Conversations
{
    public class RegistrationReplyConversation : Conversation
    {
        public enum PossibleStates
        {
            RequestReceived = 1, 
            AckNakSent = 2,
            AckNakResent = 3
        }

        public PossibleStates State;

        public RegistrationReplyConversation(Message message, IPEndPoint messageEP)
            : base(message, messageEP)
        {
            State = PossibleStates.RequestReceived;
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
