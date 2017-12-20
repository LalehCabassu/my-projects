using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Messages;
using Common.Communicator;

namespace Player
{
    public class PlayerConversationList
    {
        private Dictionary<MessageNumber, PlayerConversation> conversationDictionary;
        public Communicator myCommunicator;

        public PlayerConversationList(Communicator communicator)
        {
            conversationDictionary = new Dictionary<MessageNumber, PlayerConversation>();
            myCommunicator = communicator;
        }

        public PlayerConversation AddNewConversation(Message message, IPEndPoint managerEP)
        {
            Envelope request = Envelope.CreateOutgoingEnvelope(message, managerEP);
            PlayerConversation newConversation = new PlayerConversation(request, myCommunicator);
            if (!IsExist(message.ConversationId))
            {
                conversationDictionary.Add(newConversation.ConversationID, newConversation);
                return newConversation;
            }
            return null;
        }

        public bool IsExist(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, PlayerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return true;
            return false;
        }

        public PlayerConversation FindConversation(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, PlayerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return pair.Value;
            return null;
        }

        public void CleanConversationList()
        {
            foreach (KeyValuePair<MessageNumber, PlayerConversation> pair in conversationDictionary)
            {
                if (isConversationOld(pair.Value.LastUpdateTime))
                    removeConversation(pair.Key);
            }
        }

        private bool isConversationOld(DateTime lastUpdate)
        {
            if (lastUpdate.Date == DateTime.Now.Date && lastUpdate.Hour == DateTime.Now.Hour &&
                lastUpdate.Minute == DateTime.Now.Minute && lastUpdate.Second - DateTime.Now.Second > 30)
                return true;
            return false;
        }
        
        private void removeConversation(MessageNumber conversationID)
        {
            if (IsExist(conversationID))
                conversationDictionary.Remove(conversationID);
        }

    }
}
