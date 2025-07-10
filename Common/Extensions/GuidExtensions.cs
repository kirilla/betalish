using Betalish.Common.Exceptions;

namespace Betalish.Common.Extensions;

public static class GuidExtensions
{
    public static void AssertValid(this Guid? guid)
    {
        if (guid == null)
            throw new NullGuidException();

        if (guid == Guid.Empty)
            throw new EmptyGuidException();
    }
}
