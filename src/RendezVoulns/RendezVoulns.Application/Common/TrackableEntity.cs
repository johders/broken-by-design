namespace RendezVoulns.Application.Common;

public abstract class TrackableEntity
{
    public DateTimeOffset? UpdatedOn { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
}