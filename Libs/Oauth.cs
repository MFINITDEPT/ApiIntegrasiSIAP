using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Libs
{
    public class Oauth
    {
        public string Authorization { get; set; }
        //private static string publicKey = @"MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEA5LQiM5btJ7gNMlmjBFAV
        //                            Hj1p2JzzsOk/UsfHC3HsRpc5+G1JGovecHzXBimqPhJx7I0aBwWE8XWhA5BmqwIX
        //                            QBMp1NzRn2NiEOL0zDx1BQ863zgmHk/7NTGFwmX0r7PFy9Z4FKr5l+Rs3qG4ZYdC
        //                            DJZa1Oor0cC04Q5Z48H/NmSt8xhvYFsewvI6UxAZvXFlvJUyk/yD0cBrzkeyD0g+
        //                            M95YKuSZFwwGzbXvjyqT74L36sStj9FLzJXwUlm4C6VAdhMaAMZAl/4wguK+zF8W
        //                            z5RtGZwBWXCb3vQaA4oTrDepWKSOE1caBvVPQPYSW5cPI2ch1HKLIU629kf+v+3G
        //                            ff4WSTqDR6d0kR4kGiRxqSAK2trcJS+IaErGRuFpVb7rpe3wpY1zqYkYQdiN+HM+
        //                            uyjfIeeygpwH4xeT15PVdhR8YPoIOBKdgwIX7igryYQoRZOoYhQP7vVVl24Km7DG
        //                            cUAxd8LINQcF4QYvS1Gb6/uxecKXDe4EOZGE1sRJ33rI/E74NguDhRmlWZfPWEWW
        //                            aeSaTiUP2BguAyXlGwfv+btFLKUZK0kW6z6QGr5/NmSW2srbC7a+pgvaGsjfrRUA
        //                            gdwS7aFfogln9Wsfy/hKWnEtLWMjzS0C/mgzqu9VW3tHF/K6dqhPxoduWg/3A15K
        //                            foPNG0ctSikxDRiL0sglMJ8CAwEAAQ==";

        private static string publicKey = System.IO.File.ReadAllText(AppContext.BaseDirectory + "/Libs/PublicKey/id_rsa.pub");


        public static bool IsValid(HttpContext http)
        {
            try
            {
                String bearer = http.Request.Headers["Authorization"].ToString();
                var arrString = bearer.Split(' ');
                String token = arrString[1].ToString();

                var rs256Token = publicKey;
                Validate(token, rs256Token);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private static void Validate(string token, string key)
        {
            var keyBytes = Convert.FromBase64String(key); // your key here

            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            RSAParameters rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParameters);
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = false,
                    RequireSignedTokens = true,
                    ValidateAudience = true,
                    ValidAudience = "7",
                    ValidateIssuer = false,
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };
                var handler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var result = handler.ValidateToken(token, validationParameters, out validatedToken);
            }
        }
    }
}
