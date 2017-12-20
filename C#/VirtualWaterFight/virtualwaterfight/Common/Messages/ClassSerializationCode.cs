using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Messages
{
    /// <summary>
    /// An attribute used to mark a class as serializable
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ClassSerializationCode : Attribute
    {
        int code;

        public ClassSerializationCode(int code)
        {
            this.code = code;
        }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        static public Int16 Find(Type t)
        {
            Int16 serializationCode = 0;
            object[] attributes = t.GetCustomAttributes(false);
            foreach (object a in attributes)
            {
                if (a.GetType() == typeof(ClassSerializationCode))
                {
                    serializationCode = (Int16) (a as ClassSerializationCode).Code;
                    break;
                }
            }
            return serializationCode;
        }

    }
}
