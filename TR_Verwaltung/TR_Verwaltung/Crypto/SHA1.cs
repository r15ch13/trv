using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Utils.Crypto
{
    public static class SHA1
    {
        public static string GetString(FileInfo file)
        {
            try 
            {
                using (SHA1Managed sha = new SHA1Managed())
                {
                    byte[] hash = sha.ComputeHash(File.ReadAllBytes(file.Info.FullName));
                    return Utils.StringHelper.ByteArrayToHexString(hash);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message, ex);
            }
        }

        public static string GetString(string str, Encoding enc = null)
        {
            try
            {
                using (SHA1Managed sha = new SHA1Managed())
                {
                    byte[] hash = (enc == null) ? sha.ComputeHash(Encoding.UTF8.GetBytes(str)) : sha.ComputeHash(enc.GetBytes(str));
                    return Utils.StringHelper.ByteArrayToHexString(hash);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message, ex);
            }
        }

        public static string GetBase64(string str, Encoding enc = null)
        {
            try
            {
                using (SHA1Managed sha = new SHA1Managed())
                {
                    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(str));
                    return Convert.ToBase64String(hash);
                }
            }
            catch (Exception ex)
            {
                throw new CryptoException(ex.Message, ex);
            }
        }
    }
}
