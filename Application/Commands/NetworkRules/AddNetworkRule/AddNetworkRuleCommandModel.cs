using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public class AddNetworkRuleCommandModel
{
    [Required(ErrorMessage = "Skriv ett adressintervall.")]
    [StringLength(
        MaxLengths.Common.Ip.Prefix.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string Range { get; set; }

    [Required(ErrorMessage = "Skriv en basadress.")]
    [StringLength(
        MaxLengths.Common.Ip.Prefix.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string? BaseAddress2 { get; set; }

    [Required(ErrorMessage = "Skriv ett prefix.")]
    [Range(1, 128)]
    public int? Prefix2 { get; set; }

    public bool Blocked { get; set; }
}
