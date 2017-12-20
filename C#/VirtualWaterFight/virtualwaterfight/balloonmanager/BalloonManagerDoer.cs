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

namespace BalloonManager
{
    public class BalloonManagerDoer : Doer
    {
        #region Data members and Getter/Setter
        public DecrementNumberOfBalloonDoer MyDecrementNumberOfBalloonDoer;
        public EmptyBalloonReplyDoer MyEmptyBalloonReplyDoer;
        public DeregistrationDoer MyDeregistrationDoer;
        public RegistrationDoer MyRegistrationDoer;
        public BalloonManager myBalloonManager;
        
        private Envelope incomingMessage;
        private ManagerConversation currentConversation;
        private ManagerConversationList myConversationList;
        #endregion

        #region Public Methods
        public BalloonManagerDoer(Communicator communicator, BalloonManager balloonManager)
            : base(communicator)
        {
            myBalloonManager = balloonManager;
            myConversationList = new ManagerConversationList(communicator, myBalloonManager.FightManagerEP, myBalloonManager.WaterManagerEP);

            // Reliable protocols
            MyEmptyBalloonReplyDoer = new EmptyBalloonReplyDoer(communicator, myBalloonManager);

            // UnrReliable protocols
            MyDecrementNumberOfBalloonDoer = new DecrementNumberOfBalloonDoer(communicator, myBalloonManager);
            MyDeregistrationDoer = new DeregistrationDoer(communicator, myBalloonManager);
            MyRegistrationDoer = new RegistrationDoer(communicator, myBalloonManager);
        }

        public override string ThreadName()
        {
            return "BalloonManagerDoer";
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
                           case Request.PossibleTypes.EmptyBalloon:
                               if (isRequestValid())
                               {
                                   currentConversation = myConversationList.AddNewConversation(incomingMessage);
                                   MyEmptyBalloonReplyDoer.DoProtocol(incomingMessage, currentConversation);
                               }
                               break;
                           default:
                               //Log
                               break;
                       }
                   }
                   else if (GetReplyType(incomingMessage) == Reply.PossibleTypes.AckNak)
                    {
                        switch (GetReplyNote(incomingMessage))
                        {
                            case "Registered":
                                MyRegistrationDoer.DoProtocol(incomingMessage);
                                break;
                            case "Deregistered":
                                MyDeregistrationDoer.DoProtocol(incomingMessage);
                                break;
                            case "Balloon":
                                if (isReplyValid())
                                    currentConversation.ReceiveAckNak(incomingMessage.SendersEP, true);
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
                    //currentConversation.SendReplyOnce();
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
