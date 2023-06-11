using FluentValidation.Results;

namespace Domain.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string property, string message)
        : this()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure(property, message)
        };

        Errors = ToErrorDictionary(failures);
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = ToErrorDictionary(failures);
    }

    public IDictionary<string, string[]> Errors { get; }

    private IDictionary<string, string[]> ToErrorDictionary(IEnumerable<ValidationFailure> failures)
    {
        return failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
