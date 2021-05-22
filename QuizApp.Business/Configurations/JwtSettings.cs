using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Business.Configurations
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }

        public string WebAppName { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Lifetime { get; set; }
    }
}
