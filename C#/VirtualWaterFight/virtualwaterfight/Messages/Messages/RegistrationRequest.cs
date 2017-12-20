using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Messages
{
    [ClassSerializationCode(30)]
    public class RegistrationRequest : Request
    {
        #region Data Members and Getters/Setters

        private static Int16 myClassId = 101;

        public string Name { get; set; }
        public Int16 Age { get; set; }
        public bool Gender { get; set; }
        public int Location { get; set; }
        
        #endregion

        #region Constructors and Factories

        /// <summary>
        /// Constructor used by factory methods, which is in turn used by the receiver of a message
        /// </summary>
        public RegistrationRequest() : base(PossibleTypes.Registration) { }

        /// <summary>
        /// Constructor used by senders of a message
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public RegistrationRequest(string name, Int16 age, bool gendar, int location)
            : base(PossibleTypes.Registration)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gendar;
            this.Location = location;
        }

        new public static RegistrationRequest Create(ByteList messageBytes)
        {
            RegistrationRequest result = null;

            if (messageBytes == null || messageBytes.Length < 6)
                throw new ApplicationException("Invalid message byte array");
            if (messageBytes.PeekInt16() != ClassId())
                throw new ApplicationException("Invalid message type");
            else
            {
                result = new RegistrationRequest();
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

            messageBytes.Add(Name);
            messageBytes.Add(Age);
            messageBytes.Add(Gender);
            messageBytes.Add(Location);
            
            Int16 length = Convert.ToInt16(messageBytes.CurrentWritePosition - lengthPos - 2);
            messageBytes.WriteInt16To(lengthPos, length);          // Write out the length of this object        
        }

        override public void Decode(ByteList messageBytes)
        {

            Int16 objType = messageBytes.GetInt16();
            if (objType != ClassId())
                throw new ApplicationException("Invalid byte array for RegistrationRequest message");

            Int16 length = messageBytes.GetInt16();

            messageBytes.SetNewReadLimit(length);

            base.Decode(messageBytes);

            Name = messageBytes.GetString();
            Age = messageBytes.GetInt16();
            Gender = messageBytes.GetBool();
            Location = messageBytes.GetInt32();
            
            messageBytes.RestorePreviosReadLimit();
        }

        private static Int16 ClassId()
        {
            return myClassId;
        }

        #endregion
    }
}
