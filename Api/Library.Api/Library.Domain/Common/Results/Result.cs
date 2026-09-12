namespace Library.Domain.Common.Results;

public static class Result 
{
    public static Success Success = default; 
    public static Failure Failure = default; 
    public static Created Created = default; 
    public static Updated Updated = default; 
    public static Deleted Deleted = default; 
}
public sealed class Result<T> : IResult<T>
{
    private readonly T? _value = default!;
    private readonly List<Error>? _errors = null;
    public bool IsSuccess {get;}
    public T Value => _value!;
    public bool IsError => !IsSuccess;
    public List<Error> Errors =>IsError? _errors! : [];
    public Error? TopError =>Errors.Count>0? _errors![0]:default;
    public Result(T value,List<Error>errors,bool isSuccess)
    {
        if (isSuccess)
        {

            this._value = value??throw new ArgumentNullException("value is null");
            this._errors = [];
            IsSuccess = true;
        }
        else {
            if(errors is null||errors.Count == 0) { throw new InvalidOperationException(); }
            this._value = default!;
            this._errors = errors;
            IsSuccess = false;
        }
    }
    public Result(T Value)
    {
        if (Value is null) throw new ArgumentNullException();
        _value = Value;
        IsSuccess = true;
    }
    public Result(Error error) {
        _errors = [error];
        IsSuccess = false;
    }
    public Result(List<Error>errors)
    {
        if (errors.Count == 0) throw new ArgumentNullException();
        _errors = errors;
        IsSuccess = false;
    }
    public TNextValue Match<TNextValue>(Func<T, TNextValue> OnValue, Func<List<Error>, TNextValue> OnError)
    {
        return IsSuccess ? OnValue(Value) : OnError(Errors);
    }
    public static implicit operator Result<T>(T value)=>new Result<T>(value);
    public static implicit operator Result<T>(Error error)=>new Result<T>(error);
    public static implicit operator Result<T>(List<Error> errors) => new Result<T>(errors);
}

public readonly record struct Success;
public readonly record struct Failure;
public readonly record struct Created;
public readonly record struct Updated;
public readonly record struct Deleted;

