using System.ComponentModel.DataAnnotations;

namespace WebCiro.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}
