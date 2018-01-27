using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Utilities
{
    public class KeyValidatorService
    {
        public static bool IsValidAndToken(string storedKey, string token, string appMetaName)
        {
            return IsValidKey(storedKey, appMetaName) && IsValidToken(token);
        }


        public static bool IsValidKey(string storedKey, string appMetaName)
        {

            // Check Unregisted application
            if (storedKey != null && storedKey.Equals(Str_RegFreeNumber + Str_RegFreeNumber + Str_RegFreeNumber + Str_RegFreeNumber))
            {
                return true;
            }

            throw new Exception("Invalid Registration Key!");
        }

        private static bool IsValidToken(string token)
        {
            return true;
        }


        static char[] hexDigits = {	  '0', '1', '2', '3', '4', '5', '6', '7',
									  '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        private static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
                return "";
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        }

    }
}
