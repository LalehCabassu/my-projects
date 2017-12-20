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

namespace Server
{
    public class FightManagerDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager myFightManager;
        private Envelope incomingMessage;
        private ManagerConversation currentConversation;
        private ManagerConversationList myConversationList;
        
        public string resultWFSS = "";
        public RegistrationReplyDoer MyRegistrationReplyDoer;
        public DeregistrationReplyDoer MyDeregistrationReplyDoer;
        public EmptyBalloonDoer MyEmptyBalloonDoer;
        public WaterReplyDoer MyWaterReplyDoer;
        public InstigateFightReplyDoer MyInstigateFightReplyDoer;
        public JoinFightReplyDoer MyJoinFightReplyDoer;
        public PlayerLocationRequestDoer MyPlayerLocationRequestDoer;
        public RecentLocationReplyDoer MyRecentLocationReplyDoer;
        public CurrentPlayersListReplyDoer MyPacketizedPlayerListReplyDoer;
        public InprocessFightsListReplyDoer MyPacketizedFightListReplyDoer;
        public PlayersOfSpecificFightReplyDoer MySpecificFightPlayersListReplyDoer;
        #endregion

        #region Public Methods
        public FightManagerDoer() {}

        public FightManagerDoer(Communicator communicator, FightManager fightManager)
            : base(communicator)
        {
            myFightManager = fightManager;
            myConversationList = new ManagerConversationList(communicator, myFightManager.BalloonManagerEP, myFightManager.WaterManagerEP);

            // Reliable protocols
            MyRegistrationReplyDoer = new RegistrationReplyDoer(communicator, myFightManager);
            MyDeregistrationReplyDoer = new DeregistrationReplyDoer(communicator, myFightManager);
            MyInstigateFightReplyDoer = new InstigateFightReplyDoer(communicator, myFightManager);
            MyJoinFightReplyDoer = new JoinFightReplyDoer(communicator, myFightManager);
            
            // Unreliable protocols
            MyPacketizedFightListReplyDoer = new InprocessFightsListReplyDoer(communicator, myFightManager);
            MyPacketizedPlayerListReplyDoer = new CurrentPlayersListReplyDoer(communicator, myFightManager);
            MyPlayerLocationRequestDoer = new PlayerLocationRequestDoer(communicator, myFightManager);
            MyRecentLocationReplyDoer = new RecentLocationReplyDoer(communicator, myFightManager);
            MySpecificFightPlayersListReplyDoer = new PlayersOfSpecificFightReplyDoer(communicator, myFightManager);
            MyEmptyBalloonDoer = new EmptyBalloonDoer(communicator, myFightManager);
            MyWaterReplyDoer = new WaterReplyDoer(communicator, myFightManager);
        }

        public override string ThreadName()
        {
            return "FightManagerDoer";
        }

        public override void Start()
        {
            // Get a proxy object for the Webservice
            wfssWebAPI.WFStatsSoapClient service = new wfssWebAPI.WFStatsSoapClient();
            resultWFSS = service.Register(myFightManager.FightManagerGUID.ToString(), myFightManager.Name, myFightManager.OperatorName, myFightManager.OperatorEmail);
            base.Start();
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
                            case Request.PossibleTypes.Registration:
                                if(isRequestValid())
                                    MyRegistrationReplyDoer.DoProtocol(incomingMessage, myConversationList);
                                break;
                            case Request.PossibleTypes.Deregistration:
                                if (isRequestValid())
                                {
                                    currentConversation = myConversationList.AddNewConversation(incomingMessage);
                                    MyDeregistrationReplyDoer.DoProtocol(incomingMessage, currentConversation);
                                }
                                break;
                            case Request.PossibleTypes.InstigateFight:
                                if (isRequestValid())
                                {
                                    currentConversation = myConversationList.AddNewConversation(incomingMessage);
                                    MyInstigateFightReplyDoer.DoProtocol(incomingMessage, currentConversation);
                                }
                                break;
                            case Request.PossibleTypes.JoinFight:
                                if (isRequestValid())
                                {
                                    currentConversation = myConversationList.AddNewConversation(incomingMessage);
                                    MyJoinFightReplyDoer.DoProtocol(incomingMessage, currentConversation);
                                }
                                break;
                            case Request.PossibleTypes.RecentLocations:
                                MyRecentLocationReplyDoer.DoProtocol(incomingMessage);
                                break;
                            case Request.PossibleTypes.CurrentPlayersList:
                                MyPacketizedPlayerListReplyDoer.DoProtocol(incomingMessage);
                                break;
                            case Request.PossibleTypes.InprocessFightsList:
                                MyPacketizedFightListReplyDoer.DoProtocol(incomingMessage);
                                break;
                            case Request.PossibleTypes.PlayersOfSpecificFight:
                                MyPacketizedPlayerListReplyDoer.DoProtocol(incomingMessage);
                                break;
                        }
                    }
                    else
                    {
                        switch (GetReplyType(incomingMessage))
                        {
                            case Reply.PossibleTypes.AckNak:
                                if (isReplyValid())
                                {
                                    currentConversation.ReceiveAckNak(incomingMessage.SendersEP, true);
                                    /*
                                    switch (GetReplyNote(incomingMessage))
                                    {
                                        case "Registered":
                                            break;
                                        case "Deregistered":
                                            //MyDeregistrationReplyDoer.DoAchNak(incomingMessage);
                                            break;
                                        //case "Balloon":
                                        //  MyEmptyBalloonRequestDoer.DoProtocol(incomingMessage);
                                        //break;
                                        //case "Water":
                                        //  MyWaterRequestDoer.DoProtocol(incomingMessage);
                                        //break;
                                    }
                                     */ 
                                }
                                break;
                            case Reply.PossibleTypes.Ack:
                                MyPacketizedPlayerListReplyDoer.DoProtocol(incomingMessage);
                                MyPacketizedFightListReplyDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.PlayerLocation:
                                MyPlayerLocationRequestDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.EmptyBalloon:
                                MyEmptyBalloonDoer.DoProtocol(incomingMessage);
                                break;
                            case Reply.PossibleTypes.Water:
                                MyWaterReplyDoer.DoProtocol(incomingMessage);
                                break;
                            default:
                                //Log
                                break;
                        }
                    }
                }
                myConversationList.cleanConversationList();
                myCommunicator.incomingQueue.StateChanged.WaitOne(1000);
            }
        }

        private bool isRequestValid()
        {
            currentConversation = myConversationList.FindConversation(incomingMessage.Message.ConversationId);
            if (currentConversation != null)
            {
                if (isDuplicate() && currentConversation.IsReplyExist())
                {
                    currentConversation.SendReplyOnce();
                    return false;
                }
                else
                    return true;
            }
            return true;
        }

        private bool isReplyValid()
        {
            currentConversation = myConversationList.FindConversation(incomingMessage.Message.ConversationId);
            if (currentConversation != null)
                if (!currentConversation.IsFinished() && !isDuplicate())
                    return true;
            return false;
        }

        private bool isDuplicate()
        {
            if (currentConversation.IsDuplicateMessage(incomingMessage))
                    return true;
            return false;
        }

        private bool isOld()
        {
            if (currentConversation.IsOldMessage(incomingMessage))
                    return true;
            return false;
        }
        #endregion
    }
}
