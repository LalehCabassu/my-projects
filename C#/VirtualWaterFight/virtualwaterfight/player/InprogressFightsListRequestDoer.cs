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
    public class InprogressFightsListRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private Player MyPlayer;
        private PlayerSettings settings;
        private enum possibleStates
        {
            InprogressFightsListRequestSent = 1,
            FirstPacketizedList = 2,
            SecondPacketizedList = 3,
            ThirdPacketizedList = 4,
            AckNakListSent = 5
        }
        private Dictionary<possibleStates, Message> conversationState;
        private IPEndPoint targetEP;
        private Int16 counter = 0;
        private PacketizedFightsListReply incomingPacketizedList;
        private MessageNumber firstMessageNr;
        private MessageNumber lastMessageNr;
        private AckNakList outgoingAckNak;
        private bool flag;                //True: A new request sent and no new list received
        private int [] fightList;
        private int listCounter;
        #endregion

        #region Public Methods
        public InprogressFightsListRequestDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            settings = new PlayerSettings();
            //targetEP = new IPEndPoint(IPAddress.Loopback, settings.FightManagerPort);
            targetEP = new IPEndPoint(IPAddress.Loopback, 12001);
            flag = false;
            listCounter = 0;
        }

        public override string ThreadName()
        {
            return "InprogressFightsListRequestDoer";
        }

        public void Send(Int16 playerID)
        {
            InprocessFightsListRequest newRequest = new InprocessFightsListRequest();

            //Set ConversationID and MessageID
            MessageNumber.LocalProcessId = MyPlayer.PlayerID;
            newRequest.ConversationId= MessageNumber.Create();
            newRequest.MessageNr = newRequest.ConversationId;

            base.Send((Message)newRequest, targetEP);
            
            conversationState.Add(possibleStates.InprogressFightsListRequestSent, (Message)newRequest);
            flag = true;
        }

        public void SendReply()
        {
            AckNakList newReply = new AckNakList(firstMessageNr, lastMessageNr, Reply.PossibleStatus.Valid, "Inprocess Fight Location List");

            //Set ConversationID and MessageID
            newReply.ConversationId = incomingPacketizedList.ConversationId;
            newReply.MessageNr = MessageNumber.Create(MyPlayer.PlayerID, Convert.ToInt16(incomingPacketizedList.MessageNr.SeqNumber + 1));
            base.Send((Message)newReply, targetEP);

            //Update Conversation State
            conversationState.Add(possibleStates.AckNakListSent, (Message)newReply);
        }
        /*
        public override bool MessageAvailable()
        {
            if (base.MyPlayer.PacketizedFightListReplyQueue.Length() > 0)
                return true;
            return false;
        }
        */
        #endregion

        #region Private Methods
        protected override void Process()
        {
            conversationState = new Dictionary<possibleStates, Message>();
            fightList = new int[int.MaxValue];
            while (keepGoing)
            {/*
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
              */ 
                Thread.Sleep(0);
            }
        }
        /*
        private PacketizedFightsListReply RetrieveReply()
        {
            if (MessageAvailable())
                return (PacketizedFightsListReply)base.MyPlayer.PacketizedFightListReplyQueue.Dequeue().Message;
            return null;
        }

        private void processPacketizedList()
        {
            if (!conversationState.ContainsValue(incomingPacketizedList) && 
                conversationState[possibleStates.InprogressFightsListRequestSent].ConversationId == incomingPacketizedList.ConversationId)
            {
                if (flag)
                {
                    base.MyPlayer.emptyInprocessFightsList();
                    counter = 0;
                    flag = false;
                }

                base.MyPlayer.addToInprocessFightsList(incomingPacketizedList.FightsSubList);
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
