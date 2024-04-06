using CSharpFunctionalExtensions;

namespace ArticleService.Domain.ValueObjects;

public class PublicationTime : ValueObject
{
    private PublicationTime(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }

    public static Result<PublicationTime> Create(DateTime start, DateTime end)
    {
        if (start.ToUniversalTime() < DateTime.UtcNow)
            return Result.Failure<PublicationTime>("fff");
        var time = new PublicationTime(start, end);
        return Result.Success<PublicationTime>(time);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}