using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IOnSaveFormatter
{
    void Format(ChangeTracker changeTracker);
}
