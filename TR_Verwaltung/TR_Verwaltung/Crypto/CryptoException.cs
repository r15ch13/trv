using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Crypto
{
    public class CryptoException : Exception
    {
        public CryptoException()
            : base()
        {
        }

        public CryptoException(string str)
            : base(str)
        {
        }

        public CryptoException(string str, Exception innerException)
            : base(str, innerException)
        {
        }
    }
}
