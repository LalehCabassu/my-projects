using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Messages;
using Common.Communicator;

namespace Common.Threading
{
    public class ManagerConversationList
    {
        public Dictionary<MessageNumber, ManagerConversation> conversationDictionary;
        public IPEndPoint FirstManagerEP;
        public IPEndPoint SecondManagerEP;
        public Communicator.Communicator myCommunicator;

        public ManagerConversationList(Communicator.Communicator communicator, IPEndPoint firstManagerEP, IPEndPoint secondManagerEP)
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

        public void CleanConversationList()
        {
            List<MessageNumber> oldConvesations = new List<MessageNumber>();

            foreach (KeyValuePair<MessageNumber, ManagerConversation> pair in conversationDictionary)
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
