using System.Security.Cryptography;

namespace SimpleBlockchain
{
    // Just for testing purpose
    public class Wallet
    {
        private RSA rsa;
        public byte[] PublicKey => rsa.ExportParameters(false).Modulus;

        public Wallet()
        {
            rsa = RSA.Create(512);
        }

        public Transaction CreateAndSignTransaction(byte[] recipient, double amount, double fee)
        {
            return new Transaction(Convert.ToBase64String(PublicKey), Convert.ToBase64String(recipient), amount, fee, rsa.ExportParameters(true));
        }

        public Transaction CreateAndSignTransaction(byte[] recipient, double amount)
        {
            return new Transaction(Convert.ToBase64String(PublicKey), Convert.ToBase64String(recipient), amount, Globals.Gas, rsa.ExportParameters(true));
        }
    }
}
