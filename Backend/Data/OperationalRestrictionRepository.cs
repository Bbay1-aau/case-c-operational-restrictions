using Backend.Models.Entities;
using Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class OperationalRestrictionRepository : IOperationalRestrictionRepository
{
    private readonly AppDbContext _context;

    public OperationalRestrictionRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(OperationalRestriction restriction)
    {
        _context.OperationalRestrictions.Add(restriction);
        _context.SaveChanges();
    }

    public void Update(OperationalRestriction restriction)
    {
        _context.OperationalRestrictions.Update(restriction);
        _context.SaveChanges();
    }

    public OperationalRestriction? GetById(Guid id)
    {
        return _context.OperationalRestrictions.FirstOrDefault(r => r.RestrictionId == id);
    }

    public List<OperationalRestriction> GetAll(Func<OperationalRestriction, bool> predicate)
    {
        return _context.OperationalRestrictions.Where(predicate).ToList();
    }

    public List<OperationalRestriction> GetAll()
    {
        return _context.OperationalRestrictions.ToList();
    }
}