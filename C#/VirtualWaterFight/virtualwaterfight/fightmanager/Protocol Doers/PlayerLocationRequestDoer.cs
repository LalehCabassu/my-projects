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

namespace FightManager
{
    public class PlayerLocationRequestDoer : Doer
    {
        #region Data members and Getter/Setter
        private FightManager MyFightManager;
        public DateTime [] PlayerLocationRequestTimeList;
        private PlayerLocationReply incomingReply;
        private DateTime receivedDateTime;
        private IPEndPoint targetEP;
        #endregion

        #region Public Methods
        public PlayerLocationRequestDoer(Communicator communicator, FightManager myFightManager)
            : base(communicator)
        {
            MyFightManager = myFightManager;
            
            PlayerLocationRequestTimeList = new DateTime[Int16.MaxValue];
        }

        public override string ThreadName()
        {
            return "PlayerLocationRequestDoer";
        }

        public void SendRequest()
        {
            PlayerLocationRequest newRequest;
            foreach (KeyValuePair<IPEndPoint, Player> p in MyFightManager.PlayerList)
            {
                newRequest = new PlayerLocationRequest();
                MessageNumber.LocalProcessId = p.Value.PlayerID;
                newRequest.ConversationId = MessageNumber.Create();
                newRequest.MessageNr = newRequest.ConversationId;

                Send((Message)newRequest, p.Key);
                PlayerLocationRequestTimeList[p.Value.PlayerID] = DateTime.Now;
            }
        }

        public override void DoProtocol(Envelope message)
        {
            incomingReply = message.Message as PlayerLocationReply;
            targetEP = message.SendersEP;
            receivedDateTime = DateTime.Now;
            Int16 period = TimeDistance();
            double speed;
            
            if (period > -1 && period <= 10) //If it is not late
            {
                speed = PlayerSpeed(period);
                Player player = MyFightManager.FindPlayer(incomingReply.PlayerID);
                if (speed <= 10 && player != null)   // Player movement speed should be less than 15 m/s
                    player.MoveToNewLocation(incomingReply.Location, receivedDateTime);
                else
                    SendDeregister();
            }
            else
                SendDeregister();
        }
        #endregion

        #region Private Methods
        private Int16 TimeDistance()
        {
            DateTime sentDateTime = PlayerLocationRequestTimeList[incomingReply.ConversationId.ProcessId];
            if (sentDateTime.Date == receivedDateTime.Date && sentDateTime.Hour == receivedDateTime.Hour &&
                sentDateTime.Minute == receivedDateTime.Minute &&
                (receivedDateTime.Second - sentDateTime.Second) >= 0 &&
                (receivedDateTime.Second - sentDateTime.Second) <= 10)
                return (Int16)(receivedDateTime.Second - sentDateTime.Second);
            return -1;
        }

        private double PlayerSpeed(Int16 period)
        {
            Location newLocation = incomingReply.Location;
            Location previousLocation = MyFightManager.FindPlayer(incomingReply.PlayerID).GetCurrentLocation();
            double distance = Math.Sqrt(Math.Pow(newLocation.Y - previousLocation.Y, 2) + Math.Pow(newLocation.X - previousLocation.X, 2));
            return distance / period;
        }

        private void SendDeregister()
        {
            Player curPlayer = MyFightManager.FindPlayer(targetEP);
            MyFightManager.RemovePlayer(targetEP);
            
            AckNak newReply = new AckNak(Reply.PossibleStatus.Valid, "Deregistered");
            newReply.ConversationId = incomingReply.ConversationId;
            newReply.MessageNr = MessageNumber.Create(incomingReply.ConversationId.ProcessId, Convert.ToInt16(incomingReply.MessageNr.SeqNumber + 1));
            Send((Message)newReply, targetEP);
            Send((Message)newReply, MyFightManager.BalloonManagerEP);
            Send((Message)newReply, MyFightManager.WaterManagerEP);
        }
        #endregion
    }
}
