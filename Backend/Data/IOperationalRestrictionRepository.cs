using Backend.Models.Entities;
using Backend.Models.Enums;

namespace Backend.Data;

public interface IOperationalRestrictionRepository
{
    void Add(OperationalRestriction restriction);
    void Update(OperationalRestriction restriction);
    OperationalRestriction? GetById(Guid id);
    List<OperationalRestriction> GetAll(Func<OperationalRestriction, bool> predicate);
    List<OperationalRestriction> GetAll();
}