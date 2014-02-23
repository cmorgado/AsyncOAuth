﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncOAuth
{
    internal static class Utility
    {
        static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long ToUnixTime(this DateTime target)
        {
            return (long)(target - unixEpoch).TotalSeconds;
        }

        ///  <summary>Escape RFC3986 String</summary>
     
        /// <param name="stringToEscape"></param>
        /// <returns></returns>

        public static string UrlEncode(this string stringToEscape)
        {
            return Uri.EscapeDataString(stringToEscape)
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27")
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace(" ", "%20")
                ;
        }
        /**/



     

        public static string UrlDecode(this string stringToUnescape)
        {
            stringToUnescape = stringToUnescape.Replace("+", " ");
            return Uri.UnescapeDataString(stringToUnescape)
                .Replace("%21", "!")
                .Replace("%2A", "*")
                .Replace("%27", "'")
                .Replace("%28", "(")
                .Replace("%29", ")");
        }

        public static List<string> ParseUrlSegments (string path)
        {
            var segments = path.Split('/');
            List<string> encodedSegments = new List<string>();
            foreach (var item in segments)
            {
                encodedSegments.Add(item.UrlEncode());
            }
            return encodedSegments;
        }

        public static string EncodedPath(List<string> urlSegments)
        {
            StringBuilder p = new StringBuilder();
            foreach (var item in urlSegments)
            {
                p.AppendFormat("/{0}", item);
            }
            return p.ToString();
        }

        public static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string query)
        {
            var queryParams = query.TrimStart('?').Split('&')
               .Where(x => x != "")
               .Select(x =>
               {
                   var xs = x.Split('=');
                   return new KeyValuePair<string, string>(xs[0].UrlDecode(), xs[1].UrlDecode());
               });

            return queryParams;
        }

        public static string Wrap(this string input, string wrapper)
        {
            return wrapper + input + wrapper;
        }

        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}