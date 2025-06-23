using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Betalish.Application.Interfaces;

public interface IOnSaveValidator
{
    void Validate(ChangeTracker changeTracker);
}
