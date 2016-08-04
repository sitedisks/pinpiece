using System;

namespace pinpiece.api.Models.Dto
{
    public class dtoUser
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string AuthToken { get; set; }
        public DateTime AuthTokenExpiry { get; set; }
    }
}