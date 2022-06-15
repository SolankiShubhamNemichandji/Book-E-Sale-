using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public partial class Orderdmst
    {
        public Orderdmst()
        {
            Orderdtls = new HashSet<Orderdtl>();
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public TimeOnly Orderdate { get; set; }
        public decimal Totalprice { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Orderdtl> Orderdtls { get; set; }
    }
}
