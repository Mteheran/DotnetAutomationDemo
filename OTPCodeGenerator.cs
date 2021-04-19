using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtpNet;

namespace RMR.FinancialAllocation.Automation.Tools
{
    public static class OTPCodeGenerator
    {
        public static string GetCodeFromSecretKey()
        {
            var config = ConfigutationManager.InitConfiguration();
            var otpKeyStr = config["2factorSecretKey"]; //"cxhp2gpzzy7ldh2l"; // <- this 2FA secret key.
            var otpKeyBytes = Base32Encoding.ToBytes(otpKeyStr);
            var totp = new Totp(otpKeyBytes);
            var twoFactorCode = totp.ComputeTotp();

            return twoFactorCode;
        }
    }
}