using System.ComponentModel.DataAnnotations;

namespace FalconOne.Enumerations.Mail
{
    public enum MailRecipientTypeEnum
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "To")]
        To = 1,

        [Display(Name = "CC")]
        CC,

        [Display(Name = "BCC")]
        BCC
    }
}
