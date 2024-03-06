using Rampage.Database.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace Rampage.Database.DomainModels;

public class Setting:BaseEntity,IEntity
{
    [MaxLength(128)]
    public string Key { get; set; } = null!;
    [MaxLength(256)]
    public string Value { get; set; }=null!;
}
