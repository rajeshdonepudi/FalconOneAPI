using System.ComponentModel.DataAnnotations;

namespace FalconOne.Enumerations.Settings
{
    public enum SettingTypeEnum
    {
        [Display(Name = "Predefined")]
        Predefined = 1,

        [Display(Name = "Custom")]
        Custom = 2
    }
}
