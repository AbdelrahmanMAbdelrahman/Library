namespace Library.Domain.Books;

public sealed class BookErrors
{
    internal static Error EmptyTitle=> Error.Validation("BookErros.EmptyTitle","Provide a valid title");
    internal static Error EmptyISBN => Error.Validation("BookErros.EmptyISBN", "Provide a valid ISBN");
    internal static Error InvalidPublicationDate => Error.Validation("BookErros.EmptyPublication date", "Provide a valid publication date");
    internal static Error EmptyGenere => Error.Validation("BookErros.EmptyGenere", "Provide a valid Genere");

    public static Error NotValidId=>Error.Validation("BookErrors.NotValidId","Provide a valid book id");
}
