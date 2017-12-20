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
    public class CurrentPlayersListRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private enum possibleStates
        {
            CurrentPlayerListRequestSent = 1,
            FirstPacketizedList = 2,
            SecondPacketizedList = 3,
            ThirdPacketizedList = 4,
            AckNakListSent = 5
        }
        private Int16 counter = 0;
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private PacketizedPlayersListReply incomingPacketizedList;
        private MessageNumber firstMessageNr;
        private MessageNumber lastMessageNr;
        private AckNakList outgoingAckNak;
        #endregion

        #region Public Methods
        private Player MyPlayer;
        private PlayerSettings settings;
        public CurrentPlayersListRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            settings = new PlayerSettings();
            //targetEP = new IPEndPoint(IPAddress.Loopback, settings.FightManagerPort);
            targetEP = new IPEndPoint(IPAddress.Loopback, 12001);
        }

        public override string ThreadName()
        {
            return "CurrentPlayerListRequestDoer";
        }

        public void Send()
        {
            CurrentPlayersListRequest newRequest = new CurrentPlayersListRequest();

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);
            
            //Update Conversation State
            conversationState.Add(possibleStates.CurrentPlayerListRequestSent, (Message)newRequest);
        }

        public void SendReply()
        {
            AckNakList newReply = new AckNakList(firstMessageNr, lastMessageNr, Reply.PossibleStatus.Valid, "Player Location List");
            
            //Set ConversationID and MessageID
            newReply.ConversationId = incomingPacketizedList.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingPacketizedList.ConversationId.ProcessId, Convert.ToInt16(incomingPacketizedList.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.AckNakListSent, (Message)newReply);
        }
        /*
        public override bool MessageAvailable()
        {
            if (base.MyPlayer.PacketizedPlayerListReplyQueue.Length() > 0)
                return true;
            return false;
        }
        */
        #endregion

        #region Private Methods
        /*
        protected override void Process()
        {
            conversationState = new Dictionary<possibleStates,Message>();
            while (keepGoing)
            {
                if (!suspended)
                {
                    if (MessageAvailable())
                    {
                        incomingPacketizedList = RetrieveReply();
                        if (incomingPacketizedList != null)
                        {
                            processPacketizedList();
                            if (counter == 2)
                                SendReply();
                        }
                    }
                }
               
                Thread.Sleep(0);
            }
        }
        /*
        private PacketizedPlayersListReply RetrieveReply()
        {
            if (MessageAvailable())
                return (PacketizedPlayersListReply)base.MyPlayer.PacketizedPlayerListReplyQueue.Dequeue().Message;
            return null;
        }

        private void processPacketizedList()
        {
            if (!conversationState.ContainsValue(incomingPacketizedList) &&
                conversationState[possibleStates.CurrentPlayerListRequestSent].ConversationId == incomingPacketizedList.ConversationId)
            {
                base.MyPlayer.updateOpponentLocationList(incomingPacketizedList.PlayersSubList);
                counter = (Int16)(++counter % 3);
            
                //Update Conversation State
                switch (counter)
                {
                    case 0:
                        firstMessageNr = incomingPacketizedList.MessageNr;
                        if (conversationState.ContainsKey(possibleStates.FirstPacketizedList))
                            conversationState[possibleStates.FirstPacketizedList] = incomingPacketizedList;
                        else
                            conversationState.Add(possibleStates.FirstPacketizedList, incomingPacketizedList);
                        break;
                    case 1:
                        if (conversationState.ContainsKey(possibleStates.SecondPacketizedList))
                            conversationState[possibleStates.SecondPacketizedList] = incomingPacketizedList;
                        else
                            conversationState.Add(possibleStates.SecondPacketizedList, incomingPacketizedList);
                        break;
                    case 2:
                        lastMessageNr = incomingPacketizedList.MessageNr;
                        if (conversationState.ContainsKey(possibleStates.ThirdPacketizedList))
                            conversationState[possibleStates.ThirdPacketizedList] = incomingPacketizedList;
                        else
                            conversationState.Add(possibleStates.ThirdPacketizedList, incomingPacketizedList);
                        break;
                }
            }
        }
         */ 
        #endregion
    }
}
