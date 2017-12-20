﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Messages
{
    public class EmptyBalloonRequest : Request
    {
         #region Data Members and Getters/Setters

        private static Int16 myClassId = 111;

        public enum PossibleSize
        {
            XSmall = 1,
            Small = 2,
            Medium = 3,
            Large = 4,
            XLarge = 5
        }
        public PossibleSize Size { get; set; }

        public enum PossibleColor
        {
            Red = 1,
            Blue = 2,
            Green = 3,
            White = 4,
            Black = 5,
            Purpule = 6,
            Orange = 7,
            Gray = 8,
            Brown = 9,
            Yellow = 10
        }
        public PossibleColor Color { get; set; }
        
        #endregion

        #region Constructors and Factories

        /// <summary>
        /// Constructor used by factory methods, which is in turn used by the receiver of a message
        /// </summary>
        public EmptyBalloonRequest() : base(PossibleTypes.EmptyBalloon) { }

        /// <summary>
        /// Constructor used by senders of a message
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EmptyBalloonRequest(PossibleSize size, PossibleColor color) : base(PossibleTypes.EmptyBalloon)
        {
            this.Size = size;
            this.Color = color;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes">A byte list from which the message will be decoded</param>
        /// <returns>A new message of the right specialization</returns>
        new public static EmptyBalloonRequest Create(ByteList messageBytes)
        {
            EmptyBalloonRequest result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new EmptyBalloonRequest();
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

            messageBytes.Add(Convert.ToByte(Size));
            messageBytes.Add(Convert.ToByte(Color));

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
                throw new ApplicationException("Invalid byte array for EmptyBalloonRequest message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            Size = (PossibleSize)Convert.ToInt32(messageBytes.GetByte());
            Color = (PossibleColor)Convert.ToInt32(messageBytes.GetByte());

            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion
    }
}