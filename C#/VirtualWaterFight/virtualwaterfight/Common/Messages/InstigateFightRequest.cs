using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

using Objects;

namespace Common.Messages
{
    public class InstigateFightRequest : Request
    {
        #region Data Members and Getters/Setters

        private static Int16 myClassId = 107;

        public Int16 PlayerID { get; set; }
        public Location PlayerLocation { get; set;}
        public Int16 AmountOfWater { get; set;}
        public int BalloonID;
        public 
 
        #endregion

        #region Constructors and Factories

        /// <summary>
        /// Constructor used by factory methods, which is in turn used by the receiver of a message
        /// </summary>
        InstigateFightRequest() : base(PossibleTypes.InstigateFight) 
        {
            PlayerLocation = new Location();
        }

        public InstigateFightRequest(PossibleTypes type) : base(type)
        {
            PlayerLocation = new Location();
        }

        /// <summary>
        /// Constructor used by senders of a message
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public InstigateFightRequest(Int16 playerID, Location playerLocation, Int16 waterAmount, int balloonID)
            : base(PossibleTypes.InstigateFight)
        {
            this.PlayerID = playerID;
            this.PlayerLocation = playerLocation;
            this.AmountOfWater = waterAmount;
            this.BalloonID = balloonID;
        }

        public InstigateFightRequest(PossibleTypes type, Int16 playerID, Location playerLocation, Int16 waterAmount, int balloonID)
            : base(type)
        {
            this.PlayerID = playerID;
            this.PlayerLocation = playerLocation;
            this.AmountOfWater = waterAmount;
            this.BalloonID = balloonID;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes">A byte list from which the message will be decoded</param>
        /// <returns>A new message of the right specialization</returns>
        new public static InstigateFightRequest Create(ByteList messageBytes)
        {
            InstigateFightRequest result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new InstigateFightRequest();
                result.Decode(messageBytes);
            }

            return result;
        }

        #endregion

        #region Encoding and Decoding methods

        override public void Encode(ByteList messageBytes)
        {
            messageBytes.Add(ClassId());                            // Write out this class id first

            Int16 lengthPos = messageBytes.CurrentWritePosition;   // Get the current write position, so we
                                                                    // can write the length here later
            messageBytes.Add((Int16)0);                            // Write out a place holder for the length


            base.Encode(messageBytes);                              // Encode stuff from base class

            messageBytes.Add(this.PlayerID);
            messageBytes.Add(this.PlayerLocation.X);
            messageBytes.Add(this.PlayerLocation.Y);
            messageBytes.Add(AmountOfWater);
            messageBytes.Add(BalloonID);

            Int16 length = Convert.ToInt16(messageBytes.CurrentWritePosition - lengthPos - 2);
            messageBytes.WriteInt16To(lengthPos, length);          // Write out the length of this object        
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        override public void Decode(ByteList messageBytes)
        {
            Int16 objType = messageBytes.GetInt16();
            if (objType != ClassId())
                throw new ApplicationException("Invalid byte array for InstigateRequest message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            PlayerID = messageBytes.GetInt16();
            PlayerLocation.X = messageBytes.GetInt32();
            PlayerLocation.Y = messageBytes.GetInt32();
            AmountOfWater = messageBytes.GetInt16();
            BalloonID = messageBytes.GetInt32();
            
            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion
    }
}
