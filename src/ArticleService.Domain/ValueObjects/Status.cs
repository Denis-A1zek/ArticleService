using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace ArticleService.Domain.ValueObjects;

public class Status : ValueObject
{
    public static readonly Status Approved = new(nameof(Approved));
    public static readonly Status Rejected = new(nameof(Rejected));
    public static readonly Status Pending = new(nameof(Pending));

    private static readonly Status[] _all = [Approved, Rejected, Pending];
    
    public string Value { get; }
    private Status(string value)
    {
        Value = value;
    }

    public static Result<ValueObject> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result.Failure<ValueObject>("Value is requiered");

        var status = input.Trim().ToLower();
        
        if(_all.Any(a => a.Value.ToLower() == status) == false)
            return Result.Failure<ValueObject>("Value is invalid");

        return new Status(status);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Approved;
        yield return Rejected;
        yield return Pending;
    }
}