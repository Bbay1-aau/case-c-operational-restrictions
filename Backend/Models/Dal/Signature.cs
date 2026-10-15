using Backend.Models.Enums;

namespace Backend.Models.Entities;

// MODEL - ENTITY (MVCS):
// Represents database tables managed and mapped by EF Core to PostgreSQL
public class Signature
{
    public Guid SignatureId { get; set; }
    public Guid UserId { get; set; }
    public SignatureType Type { get; set; }
    public DateTime SignedAt { get; set; } = DateTime.Now;
    public string Comment { get; set; } = string.Empty;
}
