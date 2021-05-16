namespace RuilWinkelVaals.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfileData")]
    public partial class ProfileData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfileData()
        {
            AccountData = new HashSet<AccountData>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Voornaam { get; set; }

        [StringLength(50)]
        public string Achternaam { get; set; }

        public int? Balans { get; set; }

        public int? AccountType { get; set; }

        public int? Ledenpas { get; set; }

        [StringLength(50)]
        public string Straat { get; set; }

        [StringLength(10)]
        public string Huisnummer { get; set; }

        [StringLength(50)]
        public string Woonplaats { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Geboortedatum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountData> AccountData { get; set; }

        public virtual AccountType_LT AccountType_LT { get; set; }

        public virtual Ledenpas_LT Ledenpas_LT { get; set; }
    }
}
