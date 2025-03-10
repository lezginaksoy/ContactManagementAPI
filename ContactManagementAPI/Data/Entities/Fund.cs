using System.ComponentModel.DataAnnotations;

namespace ContactManagementAPI.Data.Entities
{
    public class Fund
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<FundContact> FundContacts { get; set; } = new();
    }

}
