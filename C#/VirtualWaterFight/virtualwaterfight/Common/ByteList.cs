using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using log4net;

namespace Common
{
    public class ByteList
    {
        #region Data Members
        private static readonly ILog log = LogManager.GetLogger(typeof(ByteList));

        private const int SECTION_SIZE = 1024;

        private List<byte[]> _sections = new List<byte[]>();
        private Int16 _addCurrentSection = 0;
        private Int16 _addCurrentOffset = 0;
        private Int16 _readCurrentPosition = 0;
        private Stack<Int16> _readLimitStack = new Stack<Int16>();
        #endregion

        #region Constructors
        public ByteList()
        {
            _sections.Add(new byte[SECTION_SIZE]);
        }

        public ByteList(params object[] items)
        {
            _sections.Add(new byte[SECTION_SIZE]);
            AddObjects(items);
        }
        #endregion

        #region Other Public Methods

        public void Clear()
        {
            _sections.Clear();
            _readLimitStack.Clear();
            _addCurrentSection = 0;
            _addCurrentOffset = 0;
            _readCurrentPosition = 0;
        }

        public void Log()
        {
            for (int i = 0; i < this.Length; i++)
                log.Debug(i.ToString() + "\t= " + this[i].ToString());
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ApplicationException();

                byte[] section = GetSection(ref index);
                return section[index];
            }
            set
            {
                Grow(index);
                byte[] section = GetSection(ref index);
                section[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return _addCurrentSection * SECTION_SIZE + _addCurrentOffset;
            }
        }

        public bool IsMore()
        {
            int tmpMax = (_readLimitStack.Count == 0) ? this.Length : _readLimitStack.Peek();
            return (_readCurrentPosition >= tmpMax) ? false : true;
        }

        #endregion

        #region Write and Add Methods

        public Int16 CurrentWritePosition
        {
            get { return Convert.ToInt16(_addCurrentSection * SECTION_SIZE + _addCurrentOffset); }
        }

        public void WriteInt16To(Int32 writePosition, Int16 length)
        {
            if (writePosition >= 0 && writePosition < Length - 2)
            {
                int sectionIdx = writePosition / SECTION_SIZE;
                int sectionOffset = writePosition - sectionIdx * SECTION_SIZE;

                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(length));
                Buffer.BlockCopy(bytes, 0,                               // Source
                                    _sections[sectionIdx], sectionOffset,   // Destination
                                    sizeof(Int16));                         // Length

            }
        }

        public void FromBytes(byte[] bytes)
        {
            Clear();
            Add(bytes);
        }

        public void AddObjects(params object[] items)
        {
            foreach (object item in items)
                AddObject(item);
        }

        public void AddObject(object item)
        {
            if (item != null)
            {
                Type type = item.GetType();

                if (type.Equals(typeof(ByteList)))
                    Add((ByteList)item);
                if (type.Equals(typeof(bool)))
                    Add((bool)item);
                else if (type.Equals(typeof(byte)))
                    Add((byte)item);
                else if (type.Equals(typeof(char)))
                    Add((char)item);
                else if (type.Equals(typeof(short)) || type.Equals(typeof(Int16)))
                    Add((Int16)item);
                else if (type.Equals(typeof(int)) || type.Equals(typeof(Int32)))
                    Add((Int32)item);
                else if (type.Equals(typeof(long)) || type.Equals(typeof(Int64)))
                    Add((Int64)item);
                else if (type.Equals(typeof(double)))
                    Add((double)item);
                else if (type.Equals(typeof(float)))
                    Add((float)item);
                else if (type.Equals(typeof(string)))
                    Add((string)item);
                else if (item is byte[])
                    Add((byte[])item);
                else
                    throw new NotImplementedException();
            }
        }

        public void Add(ByteList value)
        {
            if (value != null)
            {
                for (int i = 0; i <= value._addCurrentSection; i++)
                {
                    if (i < value._addCurrentSection)
                        Add(value._sections[i], 0, SECTION_SIZE);
                    else
                        Add(value._sections[i], 0, value._addCurrentOffset);
                }
            }
        }

        public void Add(byte value)
        {
            Add(new byte[] { value });
        }

        public void Add(bool value)
        {
            if (value)
                Add(new byte[] { 1 });
            else
                Add(new byte[] { 0 });
        }

        public void Add(char value)
        {
            Add(BitConverter.GetBytes(value));
        }

        public void Add(Int16 value)
        {
            Add(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Add(Int32 value)
        {
            Add(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Add(Int64 value)
        {
            Add(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void Add(double value)
        {
            Add(BitConverter.GetBytes(value));
        }

        public void Add(float value)
        {
            Add(BitConverter.GetBytes(value));
        }

        public void Add(string value)
        {
            if (value != null)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(value);
                Add((Int16)bytes.Length);
                Add(bytes);
            }
            else
                Add((Int16)0);
        }

        public void Add(byte[] value)
        {
            if (value != null)
                Add(value, 0, value.Length);
        }

        public void Add(byte[] value, int offset, int length)
        {
            if (value != null)
            {
                int additionalBytesNeeded = _addCurrentOffset + Length - SECTION_SIZE;
                Grow(additionalBytesNeeded);

                int cnt = 0;
                while (cnt < length)
                {
                    Int16 blockSize = (Int16)Math.Min(SECTION_SIZE - _addCurrentOffset, length - cnt);
                    Buffer.BlockCopy(value, offset + cnt,
                                            _sections[_addCurrentSection], _addCurrentOffset,
                                            blockSize);

                    cnt += blockSize;
                    _addCurrentOffset += blockSize;
                    if (_addCurrentOffset == SECTION_SIZE)
                    {
                        _addCurrentOffset = 0;
                        _addCurrentSection++;
                    }
                }
            }
        }

        #endregion

        #region Read and Get Method

        public void ResetRead()
        {
            _readCurrentPosition = 0;
        }

        public ByteList GetByteList(int length)
        {
            ByteList result = new ByteList();
            result.FromBytes(GetBytes(length));
            return result;
        }

        public byte GetByte()
        {
            return GetBytes(1)[0];
        }

        public bool GetBool()
        {
            return (GetBytes(1)[0] == 0) ? false : true;
        }

        public char GetChar()
        {
            return BitConverter.ToChar(GetBytes(2), 0);
        }

        public Int16 GetInt16()
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(GetBytes(2), 0));
        }

        public Int16 PeekInt16()
        {
            Int16 result = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(GetBytes(2), 0));
            _readCurrentPosition -= 2;                 // Move the current read position back two bytes
            return result;
        }

        public Int32 GetInt32()
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(GetBytes(4), 0));
        }

        public long GetInt64()
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(GetBytes(8), 0));
        }

        public double GetDouble()
        {
            return BitConverter.ToDouble(GetBytes(8), 0);
        }

        public float GetFloat(ref int offset)
        {
            return BitConverter.ToSingle(GetBytes(4), 0);
        }

        public string GetString()
        {
            string result = string.Empty;
            Int16 length = GetInt16();
            if (length > 0)
                result = Encoding.Unicode.GetString(GetBytes(length));
            return result;
        }

        public byte[] GetBytes(int length)
        {
            if (_readLimitStack.Count > 0 && _readCurrentPosition + length > _readLimitStack.Peek())
                throw new ApplicationException("Attempt to read beyond read limit");

            byte[] result = new byte[length];
            int bytesRead = 0;

            while (bytesRead < length)
            {
                int sectionIndex = _readCurrentPosition / SECTION_SIZE;
                int sectionOffset = _readCurrentPosition - sectionIndex * SECTION_SIZE;

                int cnt = Math.Min(SECTION_SIZE - sectionOffset, length - bytesRead);

                Buffer.BlockCopy(_sections[sectionIndex], sectionOffset, result, bytesRead, cnt);

                sectionOffset = 0;
                _readCurrentPosition += Convert.ToInt16(cnt);
                bytesRead += cnt;
            }

            return result;
        }

        virtual public byte[] ToBytes()
        {
            int bytesRead = 0;
            byte[] bytes = new byte[Length];

            for (int i = 0; i <= _addCurrentSection; i++)
            {
                int sectionBytes = SECTION_SIZE;
                if (i == _addCurrentSection)
                    sectionBytes = _addCurrentOffset;
                Buffer.BlockCopy(_sections[i], 0, bytes, bytesRead, sectionBytes);
                bytesRead += sectionBytes;
            }

            return bytes;
        }

        #endregion

        #region Read Limit Set, Restore, and Clear Methods

        public void SetNewReadLimit(Int16 length)
        {
            _readLimitStack.Push(Convert.ToInt16(_readCurrentPosition + length));
        }

        public void RestorePreviosReadLimit()
        {
            if (_readLimitStack.Count > 0)
                _readLimitStack.Pop();
        }

        public void ClearMaxReadPosition()
        {
            _readLimitStack.Clear();
        }

        #endregion

        #region Private Methods
        private void Grow(int additionBytesNeeded)
        {
            int sectionIndex = (additionBytesNeeded / SECTION_SIZE) + 1;
            for (int i = _sections.Count - 1; i < sectionIndex; i++)
                _sections.Add(new byte[SECTION_SIZE]);
        }

        private byte[] GetSection(ref int index)
        {
            int sectionIndex = index / SECTION_SIZE;

            if (sectionIndex >= _sections.Count)
                throw new ApplicationException();

            index -= sectionIndex * SECTION_SIZE;
            return _sections[sectionIndex];
        }
        #endregion

        # region Static Byte Parsing Functions
        static public string GetString(byte[] bytes, ref int offset, bool isNullTerminated)
        {
            Int16 length = BitConverter.ToInt16(bytes, offset);
            offset += 2;
            string result = Encoding.ASCII.GetString(bytes, offset, length);
            offset += length;
            if (isNullTerminated)
                offset++;
            return result;
        }
        # endregion

    }
}
