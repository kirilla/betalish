using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.NetworkRules.AddNetworkRule;

public class AddNetworkRuleCommandModel
{
    [Required(ErrorMessage = "Skriv ett adressintervall.")]
    [StringLength(
        MaxLengths.Common.Ip.Prefix.IPv6,
        ErrorMessage = "Skriv kortare.")]
    public string Range { get; set; }

    public bool Blocked { get; set; }
}
