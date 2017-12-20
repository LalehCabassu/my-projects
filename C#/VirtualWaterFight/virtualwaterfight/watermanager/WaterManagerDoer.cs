using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Common;
using Common.Messages;
using Common.Threading;
using Common.Communicator;
using Objects;

namespace WaterManager
{
    public class WaterManagerDoer : Doer
    {
        #region Data members and Getter/Setter
        public DecrementNumberOfBalloonDoer MyDecrementNumberOfBalloonDoer;
        public WaterReplyDoer MyWaterReplyDoer;
        public DeregistrationDoer MyDeregistrationDoer;
        public RegistrationDoer MyRegistrationDoer;
        public EmptyBalloonDoer MyEmptyBalloonDoer;

        public WaterManager myWaterManager;
        private Envelope incomingMessage;
        private ManagerConversation currentConversation;
        private ManagerConversationList myConversationList;
        #endregion

        #region Public Methods
        public WaterManagerDoer(Communicator communicator, WaterManager waterManager)
            : base(communicator)
        {
            myWaterManager = waterManager;
            myConversationList = new ManagerConversationList(communicator, myWaterManager.FightManagerEP, myWaterManager.BalloonManagerEP);

            // Reliable protocols
            MyWaterReplyDoer = new WaterReplyDoer(communicator, myWaterManager);

            // Unreliable protocols
            MyDecrementNumberOfBalloonDoer = new DecrementNumberOfBalloonDoer(communicator, myWaterManager);
            MyDeregistrationDoer = new DeregistrationDoer(communicator, myWaterManager);
            MyRegistrationDoer = new RegistrationDoer(communicator, myWaterManager);
            MyEmptyBalloonDoer = new EmptyBalloonDoer(communicator, myWaterManager);
        }

        public override string ThreadName()
        {
            return "WaterManagerDoer";
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
                           case Request.PossibleTypes.DecrementNumberOfBalloons:
                               MyDecrementNumberOfBalloonDoer.DoProtocol(incomingMessage);
                               break;
                           case Request.PossibleTypes.Water:
                               if (isRequestValid())
                               {
                                   currentConversation = myConversationList.AddNewConversation(incomingMessage);
                                   MyWaterReplyDoer.DoProtocol(incomingMessage, currentConversation);
                               }
                               break;
                           default:
                               //Log
                               break;
                       }
                   }
                   else 
                   {
                       switch (GetReplyType(incomingMessage))
                       {
                           case (Reply.PossibleTypes.AckNak):
                               switch (GetReplyNote(incomingMessage))
                               {
                                   case "Registered":
                                       MyRegistrationDoer.DoProtocol(incomingMessage);
                                       break;
                                   case "Deregistered":
                                       MyDeregistrationDoer.DoProtocol(incomingMessage);
                                       break;
                                   case "Water":
                                       if (isReplyValid())
                                            currentConversation.ReceiveAckNak(incomingMessage.SendersEP, false);
                                       break;
                                   default:
                                       break;
                               }
                               break;
                           case Reply.PossibleTypes.EmptyBalloon:
                               MyEmptyBalloonDoer.DoProtocol(incomingMessage);
                               break;
                           default:
                               break;
                       }
                    }
                }
                myConversationList.CleanConversationList();
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

        
        #endregion
    }
}
