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
    public class BalloonManagerDoer : Doer
    {
        #region Data members and Getter/Setter
        public Objects.BalloonManager MyBalloonManager;
        public MessageQueue DecrementNumberOfBalloonsRequestQueue;
        public MessageQueue EmptyBalloonRequestQueue;
        public MessageQueue NumberOfEmptyBalloonsRequestQueue;
        #endregion

        #region Public Methods
        public BalloonManagerDoer()
        {
        }

        public BalloonManagerDoer(Communicator communicator, Objects.BalloonManager myBalloonManager)
            : base(communicator)
        {
            MyBalloonManager = myBalloonManager;
        }

        public override string ThreadName()
        {
            return "BalloonManagerDoer";
        }

        
        #endregion

        #region Private Methods
        protected override void Process()
        {
            DecrementNumberOfBalloonsRequestQueue = new MessageQueue ();
            EmptyBalloonRequestQueue = new MessageQueue ();
            NumberOfEmptyBalloonsRequestQueue = new MessageQueue ();
            Envelope incomingMessage;

            while (keepGoing)
            {
                if (!suspended)
                {
                    if (MessageAvailable())
                    {
                        incomingMessage = Receive();
                        if (incomingMessage != null)
                        {
                            if (IsRequest(incomingMessage))
                            {
                                switch (getRequestType(incomingMessage))
                                {
                                    case Request.PossibleTypes.DecrementNumberOfBalloons:
                                        DecrementNumberOfBalloonsRequestQueue.Enqueue(incomingMessage);
                                        break;
                                    case Request.PossibleTypes.EmptyBalloon:
                                        EmptyBalloonRequestQueue.Enqueue(incomingMessage);
                                        break;
                                    //case Request.PossibleTypes.NumberOfEmptyBalloons:
                                      //  NumberOfEmptyBalloonsRequestQueue.Enqueue(incomingMessage);
                                        //break;
                                    default:
                                        //Log
                                        break;
                                }
                            
                            }
                        }
                    }
                }
                Thread.Sleep(0);
            }
        }
        #endregion
    }
}
