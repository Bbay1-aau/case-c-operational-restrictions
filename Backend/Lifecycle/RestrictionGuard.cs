using Backend.Models.DTOs;

namespace Backend.Lifecycle;

public interface IRestrictionGuard
{
    string Name { get; }

    bool IsSatisfied(CreateRestrictionDto data);
}

public sealed class CreationDataIsValidGuard : IRestrictionGuard
{
    public string Name => "CreationDataIsValid";

    public bool IsSatisfied(CreateRestrictionDto data)
    {
        return data is not null
            && !string.IsNullOrWhiteSpace(data.RestrictionDescription);
    }
}
