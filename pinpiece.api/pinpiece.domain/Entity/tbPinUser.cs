namespace pinpiece.domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("geogo.tbPinUser")]
    public partial class tbPinUser
    {
        public long Id { get; set; }

        public long? UserId { get; set; }

        public long PinId { get; set; }

        [Column(TypeName = "tinytext")]
        [StringLength(255)]
        public string MobileNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsInviteSent { get; set; }

        public DateTime? DateInviteSent { get; set; }

        public int? InviteSentCounter { get; set; }

        public DateTime? DateInviteAccepted { get; set; }

        public virtual tbPin tbPin { get; set; }

        public virtual tbUser tbUser { get; set; }
    }
}
