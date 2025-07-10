using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces
{
    public interface ICreatedDateTimeSetter
    {
        void SetCreated(IEnumerable<EntityEntry> entries);
    }
}
