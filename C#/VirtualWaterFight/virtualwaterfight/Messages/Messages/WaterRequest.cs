using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Messages
{
    public class WaterRequest : Request
    {
        #region Data Members and Getters/Setters

        private static Int16 myClassId = 114;

        public Int16 PercentFilled { get; set;}
        public enum PossibleSize
        {
            XSmall = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            XLarge = 5
        }
        public PossibleSize BalloonSize { get; set; }
        
        #endregion

        #region Constructors and Factories

        /// <summary>
        /// Constructor used by factory methods, which is in turn used by the receiver of a message
        /// </summary>
        public WaterRequest() : base(PossibleTypes.Water) { }

        /// <summary>
        /// Constructor used by senders of a message
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public WaterRequest(Int16 percentFilled, PossibleSize balloonSize)
            : base(PossibleTypes.Water)
        {
            this.PercentFilled = percentFilled;
            this.BalloonSize = balloonSize;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes">A byte list from which the message will be decoded</param>
        /// <returns>A new message of the right specialization</returns>
        new public static WaterRequest Create(ByteList messageBytes)
        {
            WaterRequest result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new WaterRequest();
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

            messageBytes.Add(PercentFilled);
            messageBytes.Add(Convert.ToByte(BalloonSize));

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
                throw new ApplicationException("Invalid byte array for WaterRequest message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            PercentFilled = messageBytes.GetInt16();
            BalloonSize = (PossibleSize)Convert.ToInt32(messageBytes.GetByte());

            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion
    }
}
