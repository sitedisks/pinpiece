namespace pinpiece.domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("geogo.tbUser")]
    public partial class tbUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbUser()
        {
            tbPins = new HashSet<tbPin>();
            tbPinComments = new HashSet<tbPinComment>();
            tbPinUsers = new HashSet<tbPinUser>();
        }

        public long Id { get; set; }

        [StringLength(45)]
        public string DisplayName { get; set; }

        [Column(TypeName = "tinytext")]
        [StringLength(255)]
        public string Gender { get; set; }

        [StringLength(45)]
        public string Password { get; set; }

        [Column(TypeName = "tinytext")]
        [StringLength(255)]
        public string MobileNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDateTime { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Column(TypeName = "tinytext")]
        [StringLength(255)]
        public string AuthToken { get; set; }

        public DateTime? AuthTokenExpiry { get; set; }

        [StringLength(45)]
        public string DeviceToken { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPin> tbPins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPinComment> tbPinComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbPinUser> tbPinUsers { get; set; }
    }
}
