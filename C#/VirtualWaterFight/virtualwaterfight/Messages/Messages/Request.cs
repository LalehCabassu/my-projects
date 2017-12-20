using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Messages
{
    abstract public class Request : Message
    {
        #region Data Members and Getters/Setters
        private static Int16 myClassId = 100;

        public enum PossibleTypes
        {
            Registration = 1,
            Deregistration= 2,
            PlayerLocation = 3,
            CurrentPlayersList = 4,
            RecentLocations = 5,
            DecrementNumberOfBalloons = 6,
            InstigateFight = 7,
            JoinFight = 8,
            InprocessFightsList = 9,
            SpecificFightPlayersList = 10,
            EmptyBalloon = 11,
            NumberOfFights = 12,
            NumberOfEmptyBalloons = 13,
            Water = 14
        }                       // More will be defined later

        public PossibleTypes RequestType { get; set; }
        // public int SessionId { get; set; }

        #endregion

        #region Constructors and Factory Methods
        /// <summary>
        /// Default message constructor, used by Factory methods (i.e., the Create methods)
        /// </summary>
        public Request() : base(true) { }

        /// <summary>
        /// Constructor used by specializations
        /// </summary>
        /// <param name="type">Type of request that being created</param>
        public Request(PossibleTypes type)
            : base(true)
        {
            RequestType = type;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        /// <returns>A new message of the right specialization</returns>
        new public static Request Create(ByteList messageBytes)
        {
            Request result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");

            Int16 msgType = messageBytes.PeekInt16();
            switch (msgType)
            {
                case 101:
                    result = RegistrationRequest.Create(messageBytes);
                    break;
                case 102:
                    result = DeregistrationRequest.Create(messageBytes);
                    break;
                case 103:
                    result = PlayerLocationRequest.Create(messageBytes);
                    break;
                case 104:
                    result = CurrentPlayersListRequest.Create(messageBytes);
                    break;
                case 105:
                    result = RecentLocationsRequest.Create(messageBytes);
                    break;
                case 106:
                    result = DecrementNumberOfBalloonsRequest.Create(messageBytes);
                    break;
                case 107:
                    result = InstigateFightRequest.Create(messageBytes);
                    break;
                case 108:
                    result = JoinFightRequest.Create(messageBytes);
                    break;
                case 109:
                    result = InprocessFightsListRequest.Create(messageBytes);
                    break;
                case 110:
                    result = SpecificFightPlayersListRequest.Create(messageBytes);
                    break;
                case 111:
                    result = EmptyBalloonRequest.Create(messageBytes);
                    break;
                case 112:
                    result = NumberOfFightsRequest.Create(messageBytes);
                    break;
                case 113:
                    result = NumberOfEmptyBalloonsRequest.Create(messageBytes);
                    break;
                case 114:
                    result = WaterRequest.Create(messageBytes);
                    break;
                default:
                    throw new ApplicationException("Invalid Message Type");
            }

            return result;
        }
        #endregion

        #region Encoding and Decoding methods

        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="messageBytes"></param>
        override public void Encode(ByteList messageBytes)
        {
            messageBytes.Add(ClassId());                           // Write out this class id first

            Int16 lengthPos = messageBytes.CurrentWritePosition;   // Get the current write position, so we
            // can write the length here later
            messageBytes.Add((Int16)0);                           // Write out a place holder for the length

            base.Encode(messageBytes);                             // Encode stuff from base class

            messageBytes.Add(Convert.ToByte(RequestType));         // Write out a place holder for the length
           // messageBytes.Add(SessionId);

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
                throw new ApplicationException("Invalid byte array for Request message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            RequestType = (PossibleTypes)Convert.ToInt32(messageBytes.GetByte());
            
            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion


    }
}
