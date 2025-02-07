using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Common
{
    [Table("Images")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public byte[]? Data { get; set; }
        public string Base64 => Data != null ? Convert.ToBase64String(Data) : string.Empty;
    }
}
