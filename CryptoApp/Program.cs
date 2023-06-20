namespace SimpleBlockchain
{
    public class Program
    {
        // An example: Payroll
        static void Main(string[] args)
        {
            // Create Wallets
            Wallet system = new Wallet();   // Payroll System
            Wallet bob = new Wallet();      // Employee payrate: $12.3
            Wallet alice = new Wallet();    // Employee payrate: $20
            Wallet charlie = new Wallet();  // Employee payrate: $19.2
            Wallet eve = new Wallet();      // Employee payrate: $25.5
            // --------------

            // Create a new blockchain
            Blockchain blockchain = new Blockchain();

            // -- Payroll Week 1 --

            // Add some transactions to the transaction pool
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(bob.PublicKey, 12.3 * 40));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(alice.PublicKey, 20 * 37));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(charlie.PublicKey, 19.2 * 35));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(eve.PublicKey, 25.5 * 30));

            // Create a new block and clear the transaction pool
            blockchain.CreateBlock();
            blockchain.ClearPool();

            // --------------------

            // -- Payroll Week 2 --

            // Add some transactions to the transaction pool
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(bob.PublicKey, 12.3 * 37));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(alice.PublicKey, 20 * 36));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(charlie.PublicKey, 19.2 * 35));
            blockchain.AddTransactionToPool(system.CreateAndSignTransaction(eve.PublicKey, 25.5 * 30));

            // Create a new block and clear the transaction pool
            blockchain.CreateBlock();
            blockchain.ClearPool();

            // --------------------

            // Print the blockchain information
            blockchain.PrintBlocks();

            Console.WriteLine(string.Empty);

            // Print the transaction history for system
            blockchain.PrintTransactionHistory(Convert.ToBase64String(system.PublicKey));

            Console.WriteLine(string.Empty);

            // Print the balance for Eve
            blockchain.PrintBalance(Convert.ToBase64String(eve.PublicKey));

            Console.WriteLine(string.Empty);

            // Print the last block information
            blockchain.PrintLastBlock();

            Console.WriteLine(string.Empty);

            // Print the genesis block information
            blockchain.PrintGenesisBlock();
        }
    }
}