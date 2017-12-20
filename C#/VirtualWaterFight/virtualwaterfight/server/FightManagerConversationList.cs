using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Messages;
using Common.Communicator;

namespace Server
{
    public class FightManagerConversationList
    {
        private Dictionary<MessageNumber, FightManagerConversation> conversationDictionary;
        public IPEndPoint BalloonManagerEP;
        public IPEndPoint WaterManagerEP;
        public Communicator myCommunicator;

        public FightManagerConversationList(Communicator communicator, IPEndPoint balloonManagerEP, IPEndPoint waterManagerEP)
        {
            conversationDictionary = new Dictionary<MessageNumber, FightManagerConversation>();
            myCommunicator = communicator;
            BalloonManagerEP = balloonManagerEP;
            WaterManagerEP = waterManagerEP;
        }

        public FightManagerConversation AddNewConversation(Envelope messsage)
        {
            FightManagerConversation newConversation = new FightManagerConversation(messsage, myCommunicator, BalloonManagerEP, WaterManagerEP);
            if (!IsExist(messsage.Message.ConversationId))
            {
                conversationDictionary.Add(newConversation.ConversationID, newConversation);
                return newConversation;
            }
            return null;
        }

        public bool IsExist(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, FightManagerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return true;
            return false;
        }

        public FightManagerConversation FindConversation(MessageNumber conversationID)
        {
            foreach (KeyValuePair<MessageNumber, FightManagerConversation> pair in conversationDictionary)
                if (pair.Key.ProcessId == conversationID.ProcessId && pair.Key.SeqNumber == conversationID.SeqNumber)
                    return pair.Value;
            return null;
        }

        public void cleanConversationList()
        {
            foreach (KeyValuePair<MessageNumber, FightManagerConversation> pair in conversationDictionary)
            {
                if (isConversationOld(pair.Value.LastUpdateTime))
                    RemoveConversation(pair.Key);
            }
        }

        private bool isConversationOld(DateTime lastUpdate)
        {
            if (lastUpdate.Date == DateTime.Now.Date && lastUpdate.Hour == DateTime.Now.Hour &&
                lastUpdate.Minute == DateTime.Now.Minute && lastUpdate.Second - DateTime.Now.Second > 30)
                return true;
            return false;
        }
        
        private void RemoveConversation(MessageNumber conversationID)
        {
            if (IsExist(conversationID))
                conversationDictionary.Remove(conversationID);
        }

    }
}
