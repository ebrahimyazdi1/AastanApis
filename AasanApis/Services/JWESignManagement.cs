﻿using JsonWebToken;
using Newtonsoft.Json;

namespace AasanApis.Services
{
    public static class JWESignManagement
    {
        public static async Task<string> Readkey(string filePath)
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                string publicKey = await reader.ReadToEndAsync();
                return publicKey;
            }
        }

        public static string GetEncryptedToken(string inputData, int inputIat, string publicKeyStringShahkar)
        {
            var payload = new { data = inputData, iat = inputIat };
            string jsonPayload = JsonConvert.SerializeObject(payload);
            var asymmetricJwkKey = AsymmetricJwk.FromPem(publicKeyStringShahkar);
            var asyDescriptorPlainText = new PlaintextJweDescriptor(asymmetricJwkKey,
                KeyManagementAlgorithm.EcdhEsA256KW, EncryptionAlgorithm.A256Gcm)
            {
                Payload = jsonPayload
            };
               var writer = new JwtWriter();
            var token= writer.WriteTokenString(asyDescriptorPlainText);
            return token;
        }

        //public static bool VerifySignature(ICipherParameters pubKey, string signature, string msg)
        //{
        //    try
        //    {
        //        byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
        //        byte[] sigBytes = Convert.FromBase64String(signature);

        //        ISigner signer = SignerUtilities.GetSigner("SHA-256withRSA");
        //        signer.Init(false, pubKey);
        //        signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
        //        return signer.VerifySignature(sigBytes);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public static string SignData(string msg, ICipherParameters privKey)
        //{
        //    try
        //    {
        //        byte[] msgBytes = Encoding.UTF8.GetBytes(msg);

        //        ISigner signer = SignerUtilities.GetSigner("SHA-256withRSA");
        //        signer.Init(true, privKey);
        //        signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
        //        byte[] sigBytes = signer.GenerateSignature();

        //        return Convert.ToBase64String(sigBytes);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
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
