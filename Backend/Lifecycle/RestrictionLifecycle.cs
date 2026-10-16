using Backend.Models.Enums;

namespace Backend.Lifecycle;

public enum RestrictionAction
{
    CreateDraft
}

public sealed record RestrictionTransition(
    RestrictionAction Action,
    RestrictionState? From,
    RestrictionState To,
    IReadOnlyList<string> Guards);

public static class RestrictionLifecycle
{
    public static readonly IReadOnlyDictionary<
        RestrictionAction,
        RestrictionTransition> Transitions =
        new Dictionary<RestrictionAction, RestrictionTransition>
        {
            [RestrictionAction.CreateDraft] = new(
                RestrictionAction.CreateDraft,
                From: null,
                To: RestrictionState.Draft,
                Guards: new[]
                {
                    "CreationDataIsValid"
                })
        };
}
