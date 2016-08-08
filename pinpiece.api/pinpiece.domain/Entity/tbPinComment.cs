namespace pinpiece.domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("geogo.tbPinComment")]
    public partial class tbPinComment
    {
        public long Id { get; set; }

        public long PinId { get; set; }

        public long UserId { get; set; }

        [Required]
        [StringLength(45)]
        public string Comment { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDateTime { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public virtual tbPin tbPin { get; set; }

        public virtual tbUser tbUser { get; set; }
    }
}
