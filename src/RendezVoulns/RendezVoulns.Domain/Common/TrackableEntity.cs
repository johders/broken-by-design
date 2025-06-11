namespace RendezVoulns.Domain.Common;

public abstract class TrackableEntity
{
    public DateTimeOffset? UpdatedOn { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}