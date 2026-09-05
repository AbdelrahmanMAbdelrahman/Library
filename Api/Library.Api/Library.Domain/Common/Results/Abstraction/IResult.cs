namespace Library.Domain.Common.Results.Abstraction;

public interface IResult
{
     List<Error>? Errors { get; }
     bool IsSuccess { get; }  
}
public interface IResult<out T>:IResult
{
     T Value { get; }
}
