using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagementAPI.Data.Entities
{
    public class FundContact
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Fund")]
        public int FundId { get; set; }
        public Fund Fund { get; set; } = null!;
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; } = null!;
    }

}
