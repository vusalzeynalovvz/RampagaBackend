using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class SettingPostVM
{

    [MaxLength(128)]
    public string Key { get; set; } = null!;
    [MaxLength(256)]
    public string Value { get; set; } = null!;
}
