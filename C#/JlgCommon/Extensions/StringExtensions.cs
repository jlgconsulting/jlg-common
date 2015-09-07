using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace JlgCommon.Extensions
{
    public static class StringExtensions
    {
        public static string ToSqlValidTableOrColumnName(this string str)
        {
            while (str.IndexOf("  ") >= 0)
            {
                str = str.Replace("  ", " ");
            }
            
            var modifiedStr = Regex.Replace(str.Trim(), "[^a-zA-Z0-9_.]+", "_", RegexOptions.Compiled);
            if (modifiedStr.Length > 110)
            {
                modifiedStr = modifiedStr.Substring(0, 110);
            }
            return modifiedStr.ToLower();
        }

        public static bool IsEmail(this string value)
        {
            var validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regexPattern = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

            return regexPattern.IsMatch(value);
        }

        public static string LowerCaseAndIgnoreSpaces(this string value)
        {
            var comparableString = value.Replace(" ", string.Empty);
            return comparableString.ToLower();
        }

        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str)
                || string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }

            var specialCharacters = new List<string>() { "/", "\\", ".", ";", "," };
            str = str.ToLower().Trim();
            foreach (var specialCharacter in specialCharacters)
            {
                str = str.Replace(specialCharacter, string.Format("{0} ", specialCharacter));
            }

            var words = str.Split(' ');
            var capitalezedPhrase = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length == 0)
                {
                    continue;
                }

                var wordForEdit = new StringBuilder(word);
                var firstLeterCapitalized = wordForEdit[0].ToString().ToUpper();
                wordForEdit.Remove(0, 1);
                wordForEdit.Insert(0, firstLeterCapitalized);

                capitalezedPhrase.Append(wordForEdit);
                capitalezedPhrase.Append(" ");
            }

            capitalezedPhrase.Remove(capitalezedPhrase.Length - 1, 1);

            var capitalized = capitalezedPhrase.ToString();

            foreach (var specialCharacter in specialCharacters)
            {
                capitalized = capitalized.Replace(string.Format("{0} ", specialCharacter), specialCharacter);
            }

            while (capitalized.Contains("  "))
            {
                capitalized = capitalized.Replace("  ", " ");
            }

            return capitalized;
        }             

        private static Dictionary<Type, TypeConverter> _typeConverters = new Dictionary<Type, TypeConverter>();

        public static T ParseEntry<T>(string entry)
        {
            TypeConverter conv;
            var type = typeof(T);
            if (_typeConverters.ContainsKey(type))
            {
                conv = _typeConverters[type];
            }
            else
            {
                conv = TypeDescriptor.GetConverter(type);
                _typeConverters.Add(type, conv);
            }

            return (T)conv.ConvertFromString(entry);
        }
        
        public static string Stringify<T>(List<T> list)
        {
            var sb = new StringBuilder();

            if (list.Count > 0)
            {
                sb.Append(",");
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public static List<T> StringifyRevert<T>(string stringigiedList)
        {
            var listElements = stringigiedList.Split(',');
            var list = new List<T>();
            foreach (var element in listElements)
            {
                if (!string.IsNullOrEmpty(element))
                {
                    list.Add(ParseEntry<T>(element));
                }
            }

            return list;
        }
    }
}
