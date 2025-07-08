using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.NetworkRules.EditNetworkRule;

public class EditNetworkRuleCommandModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Skriv en basadress.")]
    [StringLength(
        MaxLengths.Common.Ip.Prefix.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string? BaseAddress { get; set; }

    [Required(ErrorMessage = "Skriv ett prefix.")]
    [Range(1, 128)]
    public int? PrefixLength { get; set; }

    public bool Blocked { get; set; }
}
