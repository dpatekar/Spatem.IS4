using System;
using System.Collections.Generic;

namespace Spatem.Core.Models
{
    public class Wallet
    {
        public uint WalletId { get; set; }
        public DateTime Created { get; set; }
        public uint Overdraft { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}