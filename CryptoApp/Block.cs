using System.Security.Cryptography;
using System.Text;

namespace SimpleBlockchain
{
    public class Block
    {
        public int Index { get; }
        public DateTime TimeStamp { get; }
        public string PreviousHash { get; }
        public string Hash { get; }
        public List<Transaction> Transactions { get; }

        public Block(int index, DateTime timestamp, string previousHash, string hash, List<Transaction> transactions)
        {
            Index = index;
            TimeStamp = timestamp;
            PreviousHash = previousHash;
            Hash = hash;
            Transactions = transactions;
        }

        public Block(int index, string previousHash, List<Transaction> transactions)
        {
            Index = index;
            TimeStamp = DateTime.Now;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = GenerateHash();
        }

        // Generate hash of current block
        public string GenerateHash()
        {
            using SHA256 sha256 = SHA256.Create();
            StringBuilder sb = new StringBuilder();

            sb.Append(Index);
            sb.Append(TimeStamp.Ticks);
            sb.Append(PreviousHash);
            foreach (Transaction tx in Transactions)
            {
                byte[] txBytes = tx.ToByte();
                sb.Append(BitConverter.ToString(txBytes).Replace("-", ""));
            }

            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
