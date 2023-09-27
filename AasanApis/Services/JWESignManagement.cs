using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace AasanApis.Services
{
    public static class JWESignManagement
    {
        public static AsymmetricKeyParameter Readkey(string keyPath)
        {
            var fileStream = File.OpenText(keyPath);
            var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(fileStream);
            var key = (AsymmetricKeyParameter)pemReader.ReadObject();
            return key;
        }
        public static bool VerifySignature(ICipherParameters pubKey, string signature, string msg)
        {
            try
            {
                byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
                byte[] sigBytes = Convert.FromBase64String(signature);

                ISigner signer = SignerUtilities.GetSigner("SHA-256withRSA");
                signer.Init(false, pubKey);
                signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
                return signer.VerifySignature(sigBytes);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SignData(string msg, ICipherParameters privKey)
        {
            try
            {
                byte[] msgBytes = Encoding.UTF8.GetBytes(msg);

                ISigner signer = SignerUtilities.GetSigner("SHA-256withRSA");
                signer.Init(true, privKey);
                signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
                byte[] sigBytes = signer.GenerateSignature();

                return Convert.ToBase64String(sigBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #region MyRegion



        //public static async Task<string> ReadPublicKeyFromFile(string filePath)
        //{
        //    using (StreamReader reader = File.OpenText(filePath))
        //    {
        //        string publicKey = await reader.ReadToEndAsync();
        //        return publicKey;
        //    }
        //}

        //public static String RsaEncyptionData(string publicKey, PaymentReq paymentReq)
        //{
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
        //    rsa.FromXmlString(publicKey);
        //    var contentBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(paymentReq));
        //    var jsonEncrypt = rsa.Encrypt(contentBytes, false);
        //    return Convert.ToBase64String(jsonEncrypt) ?? "";

        //}
        #endregion


    }
}
