using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FalconOne.Models.EntityContracts;
using FalconOne.Models.Entities.Users;

namespace FalconOne.Models.Entities.ExpenseManagement
{
    public class ExpenseCategory : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public Guid? DeletedByUserId { get; set; }


        public virtual User CreatedByUser { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
    }
}
