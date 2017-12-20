using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Common.Threading;
using Common.Messages;
using Common.Communicator;
using Objects;

namespace Player
{
    public class PlayerDoer : Doer
    {
        #region Data members and Getter/Setter
        public Player MyPlayer {get; set;}
        public CurrentPlayersListRequestDoer MyCurrentPlayersListRequestDoer;
        public DeregistrationRequestDoer MyDeregistrationRequestDoer;
        public EmptyBalloonRequestDoer MyEmptyBalloonRequestDoer;
        public InprocessFightsListRequestDoer MyInprogressFightsListRequestDoer;
        public InstigateFightRequestDoer MyInstigateFightRequestDoer;
        public JointFightRequestDoer MyJointFightRequestDoer;
        public PlayerLocationReplyDoer MyPlayerLocationReplyDoer;
        public RecentLocationsRequestDoer MyRecentLocationsRequestDoer;
        public RegistrationRequestDoer MyRegistrationRequestDoer;
        public PlayersOfSpecificFightRequestDoer MyPlayersOfSpecificFightRequestDoer;
        public WaterRequestDoer MyWaterRequestDoer;

        private Envelope incomingMessage;
        private PlayerConversationList MyConversationList;
        private PlayerConversation currentConversation;
        #endregion

        #region Public Methods
        public PlayerDoer()
        {
        }

        public PlayerDoer(Communicator communicator, Player myPlayer)
            : base(communicator)
        {
            MyPlayer = myPlayer;
            MyConversationList = new PlayerConversationList(communicator);
            
            // Reliable protocols
            MyRegistrationRequestDoer = new RegistrationRequestDoer(communicator, myPlayer, MyConversationList);
            MyDeregistrationRequestDoer = new DeregistrationRequestDoer(communicator, myPlayer, MyConversationList);
            MyInstigateFightRequestDoer = new InstigateFightRequestDoer(communicator, myPlayer, MyConversationList);
            MyJointFightRequestDoer = new JointFightRequestDoer(communicator, myPlayer, MyConversationList);
            MyEmptyBalloonRequestDoer = new EmptyBalloonRequestDoer(communicator, myPlayer, MyConversationList);
            MyWaterRequestDoer = new WaterRequestDoer(communicator, myPlayer, MyConversationList);

            // Unreliable protocols
            MyCurrentPlayersListRequestDoer = new CurrentPlayersListRequestDoer(communicator, myPlayer);
            MyInprogressFightsListRequestDoer = new InprocessFightsListRequestDoer(communicator, myPlayer);
            MyPlayerLocationReplyDoer = new PlayerLocationReplyDoer(communicator, myPlayer);
            MyRecentLocationsRequestDoer = new RecentLocationsRequestDoer(communicator, myPlayer);
            MyPlayersOfSpecificFightRequestDoer = new PlayersOfSpecificFightRequestDoer(communicator, myPlayer);
        }

        public override string ThreadName()
        {
            return "PlayerDoer";
        }

        #endregion

        #region Private Methods
        protected override void Process()
        {
            while (keepGoing)
            {
                if (keepGoing && !suspended && MessageAvailable())
                {
                    incomingMessage = Receive();
                    if (IsRequest(incomingMessage))
                    {
                        switch (GetRequestType(incomingMessage))
                        {
                            case Request.PossibleTypes.PlayerLocation:
                                MyPlayerLocationReplyDoer.DoProtocol(incomingMessage);
                                break;
                        }
                    }
                    else 
                    {
                        switch (GetReplyType(incomingMessage))
                        {
                            case Reply.PossibleTypes.AckNak:
                                switch (GetReplyNote(incomingMessage))
                                {
                                    case "Registered":
                                        if (isReplyValid())
                                        {
                                            currentConversation.ReceiveReply(incomingMessage);
                                            MyRegistrationRequestDoer.DoProtocol(incomingMessage);
                                        }
                                        break;
                                    case "Deregistered":
                                        MyDeregistrationRequestDoer.DoProtocol(incomingMessage);
                                        break;
                                    case "Water":
                                        if (isReplyValid())
                                        {
                                            currentConversation.ReceiveReply(incomingMessage);
                                            MyWaterRequestDoer.DoProtocol(incomingMessage);
                                        }
                                        break;
                                }
                                break;
                            case Reply.PossibleTypes.PlayersList:
                                MyCurrentPlayersListRequestDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.RecentLocations:
                                MyRecentLocationsRequestDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.NotHit:
                                   if (GetReplyNote(incomingMessage).Contains("InstigateFight"))
                                        MyInstigateFightRequestDoer.DoNotHit(incomingMessage);
                                    else if (GetReplyNote(incomingMessage).Contains("JoinFight"))
                                        MyJointFightRequestDoer.DoNotHit(incomingMessage);
                                break;
                            case Reply.PossibleTypes.Hit:
                                if (GetReplyNote(incomingMessage).Contains("InstigateFight"))
                                    MyInstigateFightRequestDoer.DoHit(incomingMessage);
                                else if (GetReplyNote(incomingMessage).Contains("JoinFight"))
                                    MyJointFightRequestDoer.DoHit(incomingMessage);
                                break;
                            case Reply.PossibleTypes.NotHitThrower:
                                if (isReplyValid())
                                    currentConversation.ReceiveReply(incomingMessage);
                                break;
                            case Reply.PossibleTypes.HitThrower:
                                if (isReplyValid())
                                {
                                    currentConversation.ReceiveReply(incomingMessage);
                                    if (GetReplyNote(incomingMessage).Contains("InstigateFight"))
                                        MyInstigateFightRequestDoer.DoHitThrower(incomingMessage);
                                    else if (GetReplyNote(incomingMessage).Contains("JoinFight"))
                                        MyJointFightRequestDoer.DoHitThrower(incomingMessage);
                                }
                                break;
                            case Reply.PossibleTypes.InprocessFightsList:
                                MyInprogressFightsListRequestDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.PlayersOfSpecificFight:
                                MyPlayersOfSpecificFightRequestDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.EmptyBalloon:
                                if (isReplyValid())
                                {
                                    currentConversation.ReceiveReply(incomingMessage);
                                    MyEmptyBalloonRequestDoer.DoProtocol(incomingMessage);
                                }
                                break;
                        }
                    }
                }
                MyConversationList.CleanConversationList();
                myCommunicator.incomingQueue.StateChanged.WaitOne(1000);
            }
        }

        private bool isReplyValid()
        {
            if (GetReplyType(incomingMessage) == Reply.PossibleTypes.AckNak && GetReplyNote(incomingMessage) == "Registered")
            {
                currentConversation = MyConversationList.FindConversation(MessageNumber.Empty);
                if (currentConversation != null)
                    return true;
                return false;
            }
            else
            {
                currentConversation = MyConversationList.FindConversation(incomingMessage.Message.ConversationId);
                if (currentConversation != null)
              //  {
                    if (!currentConversation.IsFinished() && !isDuplicate())
                        return true;
                //}
               // else if (GetReplyType(incomingMessage) == Reply.PossibleTypes.AckNak &&
                 //      GetReplyNote(incomingMessage) == "Deregistered" &&
                   //    incomingMessage.Message.ConversationId.ProcessId == MyPlayer.PlayerID)
                   // return true;
                return false;
            }
        }

        private bool isDuplicate()
        {
            if (currentConversation.IsDuplicateMessage(incomingMessage))
                return true;
            return false;
        }

        #endregion
    }
}