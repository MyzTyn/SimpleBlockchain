using System.Text.Json;
using System.Transactions;

namespace SimpleBlockchain
{
    public class Blockchain
    {
        /// <summary>
        /// Hold the transactions before sumbit to a block
        /// </summary>
        public List<Transaction> TransactionPool { get; set; }

        // Just for an example but should replace to database
        public IList<Block> Blocks { get; }

        public Blockchain() 
        {
            // Just for testing purpose

            // Load the database
            Blocks = new List<Block>();
            // Ensure there is an initialization block if there no blocks
            if (Blocks.Count == 0)
                Blocks.Add(CreateGenesisBlock());

            TransactionPool = new List<Transaction>();
        }

        /// <summary>
        /// Add the transaction to the Transaction Pool
        /// </summary>
        /// <param name="transaction"></param>
        public void AddTransactionToPool(Transaction transaction)
        {
            if (!transaction.VerifySignature())
                return;

            TransactionPool.Add(transaction);
        }

        /// <summary>
        /// Clear the Transaction Pool after any block created
        /// </summary>
        public void ClearPool() => TransactionPool = new List<Transaction>();

        /// <summary>
        /// Get the last block
        /// </summary>
        /// <returns></returns>
        public Block GetLastBlock() => Blocks[Blocks.Count - 1];

        /// <summary>
        /// Create a block
        /// </summary>
        public void CreateBlock()
        {
            if (TransactionPool.Count == 0)
                return;

            Block block = new Block(GetLastBlock().Index + 1, GetLastBlock().Hash, TransactionPool);
            Blocks.Add(block);
        }

        // The first position on the blockchain
        private Block CreateGenesisBlock() => new Block(1, string.Empty, new List<Transaction>());

        internal void PrintTransactionHistory(string name)
        {
            Console.WriteLine($"====== Transaction History for {name} =====");
            
            foreach (Block block in Blocks)
            {
                var transactions = block.Transactions;
                foreach (var transaction in transactions)
                {
                    var sender = transaction.Sender;
                    var recipient = transaction.Recipient;

                    if (name.ToLower().Equals(sender.ToLower()) || name.ToLower().Equals(recipient.ToLower()))
                    {
                        Console.WriteLine("Timestamp :{0}", new DateTime(transaction.TimeStamp).ToString("G"));
                        Console.WriteLine("Sender:   :{0}", transaction.Sender);
                        Console.WriteLine("Recipient :{0}", transaction.Recipient);
                        Console.WriteLine("Amount    :{0}", transaction.Amount);
                        Console.WriteLine("Fee       :{0}", transaction.Fee);
                        Console.WriteLine("Verified  :{0}", transaction.VerifySignature());
                        Console.WriteLine("--------------");

                    }
                }
            }

            Console.WriteLine($"===========================================");
        }

        public void PrintBalance(string name)
        {
            Console.WriteLine($"==== Balance for {name} ====");
            
            double balance = 0;
            double spending = 0;
            double income = 0;

            foreach (Block block in Blocks)
            {
                var transactions = block.Transactions;

                foreach (var transaction in transactions)
                {

                    var sender = transaction.Sender;
                    var recipient = transaction.Recipient;

                    if (name.ToLower().Equals(sender.ToLower()))
                    {
                        spending += transaction.Amount + transaction.Fee;
                    }


                    if (name.ToLower().Equals(recipient.ToLower()))
                    {
                        income += transaction.Amount;
                    }

                    balance = income - spending;
                }
            }
            Console.WriteLine($"Balance :{balance}");

            Console.WriteLine($"============================");
        }

        public void PrintLastBlock() => PrintBlock(GetLastBlock());
        public void PrintGenesisBlock() => PrintBlock(Blocks[0]);

        public void PrintBlocks()
        {
            Console.WriteLine("====== Blockchain Explorer =====");

            foreach (Block block in Blocks)
            {
                PrintBlock(block);
            }

            Console.WriteLine("================================");
        }

        private void PrintBlock(Block block)
        {
            Console.WriteLine("--------------");
            Console.WriteLine("Index       :{0}", block.Index);
            Console.WriteLine("Timestamp   :{0}", block.TimeStamp.ToString("G"));
            Console.WriteLine("Prev. Hash  :{0}", block.PreviousHash);
            Console.WriteLine("Hash        :{0}", block.Hash);
            Console.WriteLine("Transactins :{0}", JsonSerializer.Serialize(block.Transactions, new JsonSerializerOptions() { WriteIndented = true }));
            Console.WriteLine("--------------");
        }
    }
}