namespace Betalish.Common.Validation;

public class EnumValidator
{
    public static void EnsureUniqueValues<T>() where T : Enum
    {
        var enumValues = Enum
            .GetValues(typeof(T))
            .Cast<int>()
            .ToList();

        if (enumValues.Count != enumValues.Distinct().Count())
        {
            throw new Exception($"{typeof(T).Name} contains duplicate values.");
        }
    }
}
