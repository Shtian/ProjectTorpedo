using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using log4net;
using log4net.Repository.Hierarchy;

namespace ProjectTorpedo.Util
{
    public static class Global
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string _gravatarURL = "http://www.gravatar.com/avatar.php?gravatar_id="; 
        private const string _projectName = "{Project Torpedo}";
        public static string ProjectName { get { return _projectName; }}

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input)){
                log.Debug("FirstCharToUpper(): input is null or empty.");
                throw new ArgumentException("FirstCharToUpper(): input is null or empty.");
            }
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static string GetGravatarURL(string userEmail, int size, string rating = "na")
        {
            if (size < 1 || size > 600)
            {
                log.Debug("GetGravatarURL(): size must be between 1 and 600.");
                throw new ArgumentOutOfRangeException("GetGravatarURL(): size must be between 1 and 600.");
            }

            rating = rating.ToLower();
            if(rating != "g" && rating != "pg" && rating != "r" && rating != "x" && rating != "na")
            {
                log.Debug("GetGravatarURL(): A specified rating must be g, pg, r, or x.");
                throw new ArgumentOutOfRangeException("GetGravatarURL(): A specified rating must be g, pg, r, or x.");
            }

            // If the rating is the default parameter, the rating is not to be used in the URL.
            if(rating.Equals("na"))
            {
                rating = "";
            }
            else
            {
                rating = "&r=" + rating;
            }

            if(String.IsNullOrEmpty(userEmail))
            {
                log.Debug("GetGravatarUrl(): UserEmail is null or empty.");
                return "No Gravatar Found";
            }
            userEmail = userEmail.ToLower();

            var md5Id = getMD5Hash(userEmail);
            
            return _gravatarURL + md5Id + "&s=" +size.ToString() +rating;
        }

        public static string getMD5Hash(string input)
        {
            // Create a new instance of the MD5 Hasher
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();

            // Format each byte as a Hexadecimal value
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}