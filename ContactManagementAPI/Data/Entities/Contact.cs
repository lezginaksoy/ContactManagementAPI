using System.ComponentModel.DataAnnotations;

namespace ContactManagementAPI.Data.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<FundContact> FundContacts { get; set; } = new();
    }

}
