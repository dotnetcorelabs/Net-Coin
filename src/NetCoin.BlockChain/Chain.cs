using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.BlockChain
{
    public class Chain
    {
        private IList<Block> _chain;

        public Chain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        private void AddGenesisBlock()
        {
            _chain.Add(CreateGenesisBlock());
        }

        private Block CreateGenesisBlock()
        {
            return new Block(DateTime.UtcNow, null, "{}");
        }

        private void InitializeChain()
        {
            _chain = new List<Block>();
        }

        public Block Latested()
        {
            return _chain.Last();
        }

        public void Add(Block block)
        {
            var latested = Latested();
            block.Index = latested.Index + 1;
            block.PreviousHash = latested.Hash;
            block.Hash = block.CalculateHash();

            _chain.Add(block);
        }


        public bool IsValid()
        {
            for (int i = 1; i < _chain.Count; i++)
            {
                Block currentBlock = _chain[i];
                Block previousBlock = _chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
