using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Messages;
using Common.Communicator;

namespace Common.Threading
{
    public class PlayerConversationList
    {
        public Dictionary<MessageNumber, PlayerConversation> conversationDictionary;
        public Communicator.Communicator myCommunicator;

        public PlayerConversationList(Communicator.Communicator communicator)
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

        public void AddNewConversation(PlayerConversation conversation)
        {
            if (!IsExist(conversation.ConversationID))
                conversationDictionary.Add(conversation.ConversationID, conversation);
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

        public void ChangeConversationID(MessageNumber conversationID, MessageNumber newConversationID)
        {
            PlayerConversation conversation = FindConversation(conversationID);
            removeConversation(conversationID);
            conversation.ConversationID = newConversationID;
            AddNewConversation(conversation);
        }

        public void CleanConversationList()
        {
            List<MessageNumber> oldConvesations = new List<MessageNumber>();

            foreach (KeyValuePair<MessageNumber, PlayerConversation> pair in conversationDictionary)
            {
                if (isConversationOld(pair.Value.LastUpdateTime))
                    oldConvesations.Add(pair.Key);
            }

            foreach (MessageNumber mn in oldConvesations)
                conversationDictionary.Remove(mn);
        }

        private bool isConversationOld(DateTime lastUpdate)
        {
            if (DateTime.Now.Date > lastUpdate.Date || DateTime.Now.Hour > lastUpdate.Hour ||
                DateTime.Now.Minute > lastUpdate.Minute || DateTime.Now.Second - lastUpdate.Second > 30)
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
