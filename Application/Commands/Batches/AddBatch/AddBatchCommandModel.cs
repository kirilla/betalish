using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.Batches.AddBatch;

public class AddBatchCommandModel
{
    [RegularExpression(Pattern.Common.SomeContent)]
    [Required(ErrorMessage = "Ange namn.")]
    [StringLength(
        MaxLengths.Domain.Batch.Name, 
        ErrorMessage = "Skriv kortare.")]
    public string? Name { get; set; }
}
