using System.Security.Cryptography;
using System.Text.Json;

namespace SimpleBlockchain
{
    public class Transaction
    {
        public long TimeStamp { get; }
        public string Sender { get; }
        public string Recipient { get; }
        public double Amount { get; }
        public double Fee { get; }
        public byte[] Signature { get; }

        public Transaction(long timeStamp, string sender, string recipient, double amount, double fee, byte[] signature)
        {
            TimeStamp = timeStamp;
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Fee = fee;
            Signature = signature;
        }

        public Transaction(string sender, string recipient, double amount, double fee, RSAParameters privatekey)
        {
            TimeStamp = DateTime.Now.Ticks;

            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Fee = fee;
            Signature = SignTransaction(privatekey);
        }

        public byte[] SignTransaction(RSAParameters privatekey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportParameters(privatekey);
            return rsa.SignData(ToByteWithoutSignature(), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public bool VerifySignature(RSAParameters publickey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportParameters(publickey);
            return rsa.VerifyData(ToByteWithoutSignature(), Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public bool VerifySignature()
        {
            RSA rsa = RSA.Create();
            rsa.ImportParameters(new RSAParameters { Modulus = Convert.FromBase64String(Sender), Exponent = Globals.DefaultExponent });
            return rsa.VerifyData(ToByteWithoutSignature(), Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public byte[] ToByte() => Globals.Encoding.GetBytes(JsonSerializer.Serialize(this));
        public byte[] ToByteWithoutSignature()
        {
            var data = new {
                TimeStamp, Sender, Recipient, Amount, Fee
            };
            return Globals.Encoding.GetBytes(JsonSerializer.Serialize(data));
        }
    }
}