using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Messages;

namespace Common.Communicator
{
    public class ManagerConversationList
    {
        private Dictionary<MessageNumber, ManagerConversation> conversationDictionary;
        public IPEndPoint FirstManagerEP;
        public IPEndPoint SecondManagerEP;
        public Communicator myCommunicator;

        public ManagerConversationList(Communicator communicator, IPEndPoint firstManagerEP, IPEndPoint secondManagerEP)
        {
            conversationDictionary = new Dictionary<MessageNumber, ManagerConversation>();
            myCommunicator = communicator;
            FirstManagerEP = firstManagerEP;
            SecondManagerEP = secondManagerEP;
        }

        public ManagerConversation AddNewConversation(Envelope messsage)
        {
            ManagerConversation newConversation = new ManagerConversation(messsage, myCommunicator, FirstManagerEP, SecondManagerEP);
            if (!IsExist(messsage.Message.ConversationId))
            {
                conversationDictionary.Add(newConversation.ConversationID, newConversation);
                return newConversation;
            }
            return null;
        }

        public bool IsExist(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, ManagerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return true;
            return false;
        }

        public ManagerConversation FindConversation(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, ManagerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return pair.Value;
            return null;
        }

        public void cleanConversationList()
        {
            foreach (KeyValuePair<MessageNumber, ManagerConversation> pair in conversationDictionary)
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
