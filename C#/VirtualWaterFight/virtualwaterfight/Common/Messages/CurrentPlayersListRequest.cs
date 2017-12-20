using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Common.Messages
{
    public class CurrentPlayersListRequest : Request
    {
        #region Data Members and Getters/Setters

        private static Int16 myClassId = 104;

        #endregion

        #region Constructors and Factories

        /// <summary>
        /// Constructor used by factory methods, which is in turn used by the receiver of a message
        /// </summary>
        public CurrentPlayersListRequest() : base(PossibleTypes.CurrentPlayersList) { }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        /// <returns>A new message of the right specialization</returns>
        new public static CurrentPlayersListRequest Create(ByteList messageBytes)
        {
            CurrentPlayersListRequest result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new CurrentPlayersListRequest();
                result.Decode(messageBytes);
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
                throw new ApplicationException("Invalid byte array for CurrentPlayersListRequest message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion
    }
}
