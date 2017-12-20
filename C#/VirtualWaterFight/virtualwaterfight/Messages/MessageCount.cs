using System;
using System.Collections.Generic;
using System.Text;

namespace MessageObjects
{
    [ClassSerializationCode(16)]
    public class MessageCount : MessagePayload
    {
        private Int16 count = 0;
        private Int16 windowSize = 4;
        private Int32 mStateCount = 0;

        public MessageCount() { }

        public MessageCount(Int16 count, Int16 windowSize, Int32 mStateCount)
        {
            this.count = count;
            this.windowSize = windowSize;
            this.mStateCount = mStateCount;
        }

        [PropertySerializationCode(2)]
        public Int16 Count
        {
            get { return count; }
            set { count = value; }
        }

        [PropertySerializationCode(3)]
        public Int16 WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        [PropertySerializationCode(4)]
        public Int32 MStateCount
        {
            get { return mStateCount; }
            set { mStateCount = value; }
        }

    }
}
