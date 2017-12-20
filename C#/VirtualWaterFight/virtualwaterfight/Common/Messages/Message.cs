using System;
using System.Collections.Generic;
using System.Text;
using Common;

using Objects;

namespace Common.Messages
{
    abstract public class Message : IComparable
    {
        #region Data Members and Getters/Setters
        private static Int16 myClassId = 1;

        public bool IsARequest { get; set; }
        public MessageNumber MessageNr { get; set; }
        public MessageNumber ConversationId { get; set; }
        #endregion

        #region Constructors and Factory Methods
        /// <summary>
        /// Default message constructor, used by Factory methods (i.e., the Create methods)
        /// </summary>
        protected Message()
        {
            MessageNr = MessageNumber.Empty;
            ConversationId = MessageNumber.Empty;
        }
        
        /// <summary>
        /// Constructor used by all specializations
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="isARequest"></param>
        protected Message(bool isARequest)
        {
            IsARequest = isARequest;
            MessageNr = MessageNumber.Empty;
            ConversationId = MessageNumber.Empty;
        }

        /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        /// <returns>A new message of the right specialization</returns>
        public static Message Create(ByteList messageBytes)
        {
            Message result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");

            Int16 msgType = messageBytes.PeekInt16();
            if (msgType > 100 && msgType <= 199)
                result = Request.Create(messageBytes);
            else if (msgType > 200 && msgType <= 299)
                result = Reply.Create(messageBytes);
            else
                throw new ApplicationException("Invalid Message Type");

            return result;
        }
        #endregion

        #region Encoding and Decoding methods

        /// <summary>
        /// This method encodes
        /// </summary>
        /// <param name="messageBytes"></param>
        virtual public void Encode(ByteList messageBytes)
        {
            messageBytes.Add(ClassId());                            // Write out the class type

            Int16 lengthPos = messageBytes.CurrentWritePosition;    // Get the current write position, so we
                                                                    // can write the length here later

            messageBytes.Add((Int16)0);                             // Write out a place holder for the length

            messageBytes.Add(IsARequest);
            MessageNr.Encode(messageBytes);
            ConversationId.Encode(messageBytes);

            Int16 length = Convert.ToInt16(messageBytes.CurrentWritePosition - lengthPos - 2);
            messageBytes.WriteInt16To(lengthPos, length);           // Write out the length of this object        
        }

        /// <summary>
        /// This method decodes a message from a byte list
        /// </summary>
        /// <param name="messageBytes"></param>
        virtual public void Decode(ByteList messageBytes)
        {
            Int16 objType = messageBytes.GetInt16();
            if (objType != ClassId())
                throw new ApplicationException("Invalid byte array for Message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            IsARequest = messageBytes.GetBool();
            MessageNr.Decode(messageBytes);
            ConversationId.Decode(messageBytes);

            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion

        #region Comparison Methods and Operators
        public static int Compare(Message a, Message b)
        {
            int result = 0;

            if (!System.Object.ReferenceEquals(a, b))
            {
                if (((object)a == null) && ((object)b != null))
                    result = -1;
                else if (((object)a != null) && ((object)b == null))
                    result = 1;
                else if (a.MessageNr < b.MessageNr)
                    result = -1;
                else if (a.MessageNr > b.MessageNr)
                    result = 1;
            }
            return result;
        }

        public static bool operator ==(Message a, Message b)
        {
            return (Compare(a, b) == 0);
        }

        public static bool operator !=(Message a, Message b)
        {
            return (Compare(a, b) != 0);
        }

        public static bool operator <(Message a, Message b)
        {
            return (Compare(a, b) < 0);
        }

        public static bool operator >(Message a, Message b)
        {
            return (Compare(a, b) > 0);
        }

        public static bool operator <=(Message a, Message b)
        {
            return (Compare(a, b) <= 0);
        }

        public static bool operator >=(Message a, Message b)
        {
            return (Compare(a, b) >= 0);
        }
        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return Compare(this, obj as Message);
        }

        public override bool Equals(object obj)
        {
            return (Compare(this, obj as Message)==0);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
