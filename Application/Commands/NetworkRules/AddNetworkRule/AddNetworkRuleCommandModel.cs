using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public class AddNetworkRuleCommandModel
{
    [Required(ErrorMessage = "Skriv en basadress.")]
    [StringLength(
        MaxLengths.Common.Ip.Prefix.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string? BaseAddress { get; set; }

    [Required(ErrorMessage = "Skriv ett prefix.")]
    [Range(
        Limits.NetworkRule.PrefixLength.Min,
        Limits.NetworkRule.PrefixLength.Max)]
    public int? PrefixLength { get; set; }

    public bool Block { get; set; }
    public bool Log { get; set; }
}
