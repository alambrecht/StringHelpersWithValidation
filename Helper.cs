using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringHelpersWithValidation
{
    public static class Helper
    {
        private const string _chars = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";

        /// <summary>
        /// Remove all non-numeric characters from a string
        /// </summary>
        /// <param name="dirtyString">Alphanumeric string</param>
        /// <returns>Number string</returns>
        public static string RemoveNonNumericCharacters(this string dirtyString)
        {
            var rx = new Regex("[^0123456789.]");
            return rx.Replace(dirtyString, "");
        }

        /// <summary>
        /// Returns an alphanimeric guid without dashes and brackets
        /// </summary>
        public static string GetCleanGuid
        {
            get { return Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "").ToLower(); }
        }

        /// <summary>
        /// Returns a list of email addresses from a string that is delimited with , or ; or a space
        /// </summary>
        /// <param name="emailField">String of email addresses</param>
        /// <returns>Array of email addresses</returns>
        public static string[] GetEmailsFromString(this string emailField)
        {
            var delimiters = new[] { ',', ';', ' ' };
            return emailField.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a formatted phone number from a numeric phone number
        /// </summary>
        /// <param name="phone">Numeric phone number</param>
        /// <returns>Formatted phone number</returns>
        public static string FormatPhoneNumber(this string phone)
        {
            var phoneNumber = phone.RemoveNonNumericCharacters();
            if (phoneNumber.Length == 10 || phoneNumber.Length == 7 || phoneNumber.Length == 11 ||
                phoneNumber.Length == 12)
            {
                switch (phoneNumber.Length)
                {
                    case 11:
                        phoneNumber = String.Format("{0:+0# (###) ###-####}", decimal.Parse(phoneNumber));
                        break;
                    case 12:
                        phoneNumber = String.Format("{0:+## (###) ###-####}", decimal.Parse(phoneNumber));
                        break;
                    case 10:
                        phoneNumber = String.Format("{0:(###) ###-####}", decimal.Parse(phoneNumber));
                        break;
                    case 7:
                        phoneNumber = String.Format("{0:###-####}", decimal.Parse(phoneNumber));
                        break;
                }
            }
            else
            {
                throw new ArgumentException("The phone number must either be 7, 10, 11 or 12 digits long");
            }
            return phoneNumber;
        }

        /// <summary>
        /// Generate a list of random strings
        /// </summary>
        /// <param name="size">Length of each string</param>
        /// <param name="quantity">Number of random strings to return</param>
        /// <param name="characters">Characters to randomize</param>
        /// <returns>List of randomized strings</returns>
        public static List<string> RandomString(int size, int quantity, string characters)
        {
            var ret = new List<string>();
            var rng = new Random();
            while (ret.Count < quantity)
            {
                var buffer = new char[size];
                for (var i = 0; i < size; i++)
                {
                    buffer[i] = characters[rng.Next(characters.Length)];
                }
                var item = new string(buffer);
                if (!ret.Contains(item))
                    ret.Add(item);
            }
            return ret;
        }

        /// <summary>
        /// Generate a list of random strings
        /// </summary>
        /// <param name="size">Length of each string</param>
        /// <param name="quantity">Number of random strings to return</param>
        /// <returns>List of randomized strings</returns>
        public static List<string> RandomString(int size, int quantity)
        {
            return RandomString(size, quantity, _chars);
        }

        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <param name="size">Length of string</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size)
        {
            return RandomString(size, 1, _chars)[0];
        }

        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <param name="size">Length of string</param>
        /// <param name="characters">Characters to randomize</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size, string characters)
        {
            return RandomString(size, 1, characters)[0];
        }

        /// <summary>
        /// Convert a string to a byte array
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns>Byte array</returns>
        public static byte[] ToByteArray(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        /// Validate string is a valid email address
        /// </summary>
        /// <param name="email">Email address to validate</param>
        /// <returns>True or False</returns>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            email = email.Trim().ToLower();
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Validate password is HIPAA compliant password
        /// </summary>
        /// <param name="data">Password to validate</param>
        /// <returns>True or False</returns>
        public static bool ValidateHipaaPassword(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            if (data.Length < 8) return false;
            if (!data.Any(char.IsUpper)) return false;
            if (!data.Any(char.IsLower)) return false;
            if (!data.Any(char.IsNumber)) return false;
            return !data.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Split a string based on delimiters
        /// </summary>
        /// <param name="data">String to split</param>
        /// <param name="delimiters">Delimiters to check for</param>
        /// <returns>Ienumerable of split string</returns>
        public static IEnumerable<string> SplitString(this string data, string[] delimiters)
        {
            var temp = data.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return temp.Select(item => item.Trim()).ToList();
        }
    }
}
