namespace Library.Application.Common.Interfaces;

public interface ICachedQuery
{
    public string Key { get;}
    public string[] Tags {  get;}
    public TimeSpan Expiration { get;}
}
public interface ICachedQuery<T>:IRequest<T>,ICachedQuery;