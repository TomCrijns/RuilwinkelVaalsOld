namespace RuilWinkelVaals.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountData")]
    public partial class AccountData
    {
        public int Id { get; set; }

        public int? ProfileId { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }

        public bool? Blocked { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateBlocked { get; set; }

        public virtual ProfileData ProfileData { get; set; }
    }
}
