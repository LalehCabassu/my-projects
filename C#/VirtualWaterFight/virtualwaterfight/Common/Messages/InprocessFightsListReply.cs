using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Common.Messages
{
    public class InprocessFightsListReply : Reply
    {
        #region Data Members and Getters/Setters
        private static Int16 myClassId = 210;

        //Fights
        public int[] FightsList;
        public static Int16 SubListLength = 10;
        #endregion

        #region Constructors and Factory Methods
        /// <summary>
        /// Default message constructor, used by Factory methods (i.e., the Create methods)
        /// </summary>

        protected InprocessFightsListReply() 
        {
          
        }

        /// <summary>
        /// Constructor used by all specializations, which in turn are used by
        /// senders of a message 
        /// </summary>
        /// <param name="conversationId">conversation id</param>
        /// <param name="status">Status of the ack/nak</status>
        /// <param name="note">error message or note</note>
        public InprocessFightsListReply(int [] fightsSubList, PossibleStatus status, string note) :
            base(Reply.PossibleTypes.InprocessFightsList, status, note) 
        {
            FightsList = fightsSubList;
        }

         /// <summary>
        /// Factor method to create a message from a byte list
        /// </summary>
        /// <param name="messageBytes">A byte list from which the message will be decoded</param>
        /// <returns>A new message of the right specialization</returns>
        new public static InprocessFightsListReply Create(ByteList messageBytes)
        {
            InprocessFightsListReply result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new InprocessFightsListReply();
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
            messageBytes.Add(ClassId());                            // Write out this class id first

            Int16 lengthPos = messageBytes.CurrentWritePosition;    // Get the current write position, so we
            // can write the length here later
            messageBytes.Add((Int16)0);                             // Write out a place holder for the length

            base.Encode(messageBytes);                              // Encode stuff from base class

            messageBytes.Add((Int16)FightsList.Length);

            for(int i = 0; i < FightsList.Length; i++)
                 messageBytes.Add(Convert.ToInt32(FightsList[i]));            

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
                throw new ApplicationException("Invalid byte array for PacketizedFightListReply message");

            Int16 objLength = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(objLength);

            base.Decode(messageBytes);

            Int16 numberOfFights = messageBytes.GetInt16();
            FightsList = new int[numberOfFights];

            for (int i = 0; i < FightsList.Length; i++)
                FightsList[i] = messageBytes.GetInt32();

            messageBytes.RestorePreviosReadLimit();
        }


        private static Int16 ClassId()
        {
            return myClassId;
        }
        #endregion
    }
}
