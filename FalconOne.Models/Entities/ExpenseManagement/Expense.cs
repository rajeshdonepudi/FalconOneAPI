using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityContracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FalconOne.Enumerations.ExpenseManagement;

namespace FalconOne.Models.Entities.ExpenseManagement
{
    public class Expense : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public ExpenseTypeEnum Type { get; set; } = ExpenseTypeEnum.Miscellaneous;

        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }

        public Guid CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public Guid? DeletedByUserId { get; set; }

        public virtual User LastUpdatedByUser { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }
        public virtual ExpenseCategory Category { get; set; }
    }
}
