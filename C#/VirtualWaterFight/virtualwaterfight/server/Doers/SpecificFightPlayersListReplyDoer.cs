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

namespace Server
{
    public class SpecificFightPlayersListReplyDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        private enum possibleStates
        {
            SpecificFightPlayersListRequestReceived = 1,
            FirstPacketizedListSent = 2,
            SecondPacketizedListSent = 3,
            ThirdPacketizedListSent = 4,
            AckNakListReceived = 5
        }
        private Int16 counter = 0;
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private SpecificFightPlayersListRequest incomingRequest;
        private PacketizedPlayersListReply outGoingPacketizedList;
        private MessageNumber firstMessageNrSent;
        private MessageNumber lastMessageNrSent;
        private MessageNumber lastMessageNr;
        private AckNakList incomingAckNakList;
        private static int SubListLength = 10;
        Int16[] playerList;
        #endregion

        #region Public Methods
        public SpecificFightPlayersListReplyDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
            //targetEP = new IPEndPoint(IPAddress.Loopback, base.settings.P);
        }

        public override string ThreadName()
        {
            return "SpecificFightPlayersListReplyDoer";
        }
        /*
        public override bool MessageAvailable()
        {
            if (base.SpecificFightPlayersListRequestQueue.Length() > 0)
                return true;
            return false;
        }
        */
        #endregion

        #region Private Methods
        protected override void Process()
        {
            conversationState = new Dictionary<possibleStates, Message>();
            counter = 0;
            while (keepGoing)
            {/*
                if (!suspended)
                {
                    incomingRequest = RetrieveRequest();
                    if (incomingRequest != null)
                    {
                        Int16 num = MakeLists();
                        int i;
                        for (i = 0; i <= (num / SubListLength); i++)
                        {
                            SendList(i * SubListLength, (i * SubListLength) + SubListLength);
                            if (counter == 2)
                            {
                                incomingAckNakList = getOutOfAckNakList();
                                if(incomingAckNakList != null)
                                    ProcessAckNakList();
                            }
                            counter = (Int16)(++counter % 3);
                        }
                        SendList(i * SubListLength, num % SubListLength);
                        incomingAckNakList = getOutOfAckNakList();
                        if (incomingAckNakList != null)
                            ProcessAckNakList();
                    }
                }
              * */
                Thread.Sleep(0);
            }
        }
        /*
        private SpecificFightPlayersListRequest RetrieveRequest()
        {
            Envelope incomingEnvelope;
            if (MessageAvailable())
            {
                incomingEnvelope = CurrentPlayersListRequestQueue.Dequeue();
                targetEP = incomingEnvelope.SendersEP;
                lastMessageNr = incomingEnvelope.Message.MessageNr;

                //Update Conversation State
                conversationState.Add(possibleStates.SpecificFightPlayersListRequestReceived, incomingEnvelope.Message);
                return (SpecificFightPlayersListRequest)incomingEnvelope.Message;
            }
            return null;
        }

        private AckNakList getOutOfAckNakList()
        {
            //Search AckNakList for a message from Fight Manager
            foreach (Envelope env in AckNakList)
            {
                if (env.SendersEP == targetEP && env.Message.ConversationId == incomingRequest.ConversationId)
                {            
                    AckNakList.Remove(env);
                    lastMessageNr = env.Message.MessageNr;
                    return (AckNakList)env.Message;
                }
            }
            return null;
        }

        private Int16 MakeLists()
        {
            WaterFightGame fight = base.MyFightManager.FindFight(incomingRequest.FightID);
            Int16 i = 0;
            playerList = new Int16[SubListLength];
            
            foreach (Player player in fight.PlayerList)
                playerList[i++] = player.PlayerID;
            return i;
        }

        private void SendList(int start, int end)
        {
            Int16[] subList = new Int16[SubListLength];
            for (int i = start, j = 0; i < end; i++, j++)
                subList[j] = playerList[i];
            
            SpecificFightPlayersListReply outGoingPacketizedList = new SpecificFightPlayersListReply(subList, Reply.PossibleStatus.Valid, "In-process Fight Sublist");

            //Set ConversationID and MessageID
            outGoingPacketizedList.ConversationId = incomingRequest.ConversationId;
            outGoingPacketizedList.MessageNr = MessageNumber.Create(incomingRequest.ConversationId.ProcessId, Convert.ToInt16(lastMessageNr.SeqNumber + 1));
            base.Send((Message)outGoingPacketizedList, targetEP);
            lastMessageNr = outGoingPacketizedList.MessageNr;

            switch (counter)
            {
                case 0:
                    firstMessageNrSent = outGoingPacketizedList.MessageNr;
                    conversationState.Add(possibleStates.FirstPacketizedListSent, outGoingPacketizedList);
                    break;
                case 1:
                    conversationState.Add(possibleStates.SecondPacketizedListSent, outGoingPacketizedList);
                    break;
                case 2:
                    lastMessageNrSent = outGoingPacketizedList.MessageNr;
                    conversationState.Add(possibleStates.ThirdPacketizedListSent, outGoingPacketizedList);
                    break;
            }
        }


        private void ProcessAckNakList()
        {
            switch (incomingAckNakList.Status)
            {
                case Reply.PossibleStatus.Valid:
                    //TODO
                    break;
                case Reply.PossibleStatus.InvalidLocation:
                    // TODO: Notify player to choose another location
                    break;
                case Reply.PossibleStatus.Invalid:
                    // TODO: ????
                    break;
            }

            //Update Conversation State
            conversationState.Add(possibleStates.AckNakListReceived, incomingAckNakList);
        }
         */ 
        #endregion
    }
}
