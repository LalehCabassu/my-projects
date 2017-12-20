using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Common.Messages
{
    public class Reply : Message
    {
        #region Data Members and Getters/Setters
        private static Int16 myClassId = 200;

        public enum PossibleTypes
        {
            AckNak = 1,
            PlayerLocation = 2,
            PlayersList = 3,
            Ack = 4,
            RecentLocations = 5,
            NotHit = 6,
            Hit = 7,
            NotHitThrower = 8,
            HitThrower = 9,
            InprocessFightsList = 10,
            FightManager = 11,
            EmptyBalloon = 12,
            PlayersOfSpecificFight = 13,
            Water = 14
        }                       // More will be defined later

        public enum PossibleStatus
        {
            Valid = 1,
            Invalid = 2,
        }

        public PossibleTypes ReplyType { get; set; }
        public PossibleStatus Status { get; set; }
        public string Note {get; set;}
        #endregion

        #region Constructors and Factory Methods
        /// <summary>
        /// Default message constructor, used by Factory methods (i.e., the Create methods)
        /// </summary>
        public Reply() { }

        /// <summary>
        /// Constructor used by all specializations, which in turn are used by
        /// senders of a message 
        /// </summary>
        /// <param name="type">Type of request that being created</param>
        /// <param name="status">Status of the ack/nak</status>
        /// <param name="note">error message or note</note>
        public Reply(PossibleTypes type, PossibleStatus status, string note)
            : base(false)
        {
            ReplyType = type;
            Status = status;
            Note = note;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes">A byte list from which the message will be decoded</param>
        /// <returns>A new message of the right specialization</returns>
        new public static Reply Create(ByteList messageBytes)
        {
            Reply result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");

            Int16 msgType = messageBytes.PeekInt16();
            switch (msgType)
            {
                case 201:
                    result = AckNak.Create(messageBytes);
                    break;
                case 202:
                    result = PlayerLocationReply.Create(messageBytes);
                    break;
                case 203:
                    result = PlayersListReply.Create(messageBytes);
                    break;
                case 204:
                    result = AckNakList.Create(messageBytes);
                    break;
                case 205:
                    result = RecentLocationsReply.Create(messageBytes);
                    break;
                case 206:
                    result = NotHitReply.Create(messageBytes);
                    break;
                case 207:
                    result = HitReply.Create(messageBytes);
                    break;
                case 208:
                    result = NotHitThrowerReply.Create(messageBytes);
                    break;
                case 209:
                    result = HitThrowerReply.Create(messageBytes);
                    break;
                case 210:
                    result = InprocessFightsListReply.Create(messageBytes);
                    break;
                case 211:
                    result = FightManagerReply.Create(messageBytes);
                    break;
                case 212:
                    result = EmptyBalloonReply.Create(messageBytes);
                    break;
                case 213:
                    result = PlayersOfSpecificFightReply.Create(messageBytes);
                    break;
                case 214:
                    result = WaterReply.Create(messageBytes);
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
            messageBytes.Add(ClassId());                            // Write out this class id first

            Int16 lengthPos = messageBytes.CurrentWritePosition;    // Get the current write position, so we
            // can write the length here later
            messageBytes.Add((Int16)0);                             // Write out a place holder for the length

            base.Encode(messageBytes);                              // Encode stuff from base class

            messageBytes.Add(Convert.ToByte(ReplyType));            
            messageBytes.Add(Convert.ToByte(Status));               
            messageBytes.Add(Note);

            Int16 length = Convert.ToInt16(messageBytes.CurrentWritePosition - lengthPos - 2);
            messageBytes.WriteInt16To(lengthPos, length);           // Write out the length of this object        
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        override public void Decode(ByteList messageBytes)
        {
            Int16 objType = messageBytes.GetInt16();
            if (objType != ClassId())
                throw new ApplicationException("Invalid byte array for Reply message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            ReplyType = (PossibleTypes)Convert.ToInt32(messageBytes.GetByte());
            Status = (PossibleStatus)Convert.ToInt32(messageBytes.GetByte());
            Note = messageBytes.GetString();

            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }
        #endregion

    }
}
