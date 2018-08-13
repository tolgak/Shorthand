using System;
using System.Text;
using PragmaTouchUtils;

namespace Shorthand
{
    public class JenkinsOptions
    {
        public string JenkinsBaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string EncodedCredentials {
            get {
                string mergedCredentials = $"{this.Username}:{this.Password}";
                byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
                return Convert.ToBase64String(byteCredentials);
            }
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.JenkinsBaseUrl)
                    && !string.IsNullOrEmpty(this.Username)
                    && !string.IsNullOrEmpty(this.Password);
            }
        }


    }
}
