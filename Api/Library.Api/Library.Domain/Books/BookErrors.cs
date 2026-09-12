namespace Library.Domain.Books;

internal sealed class BookErrors
{
    internal static Error InvalidPublicationDate=> 
        Error.Validation("BookErrors.InvalidPublicationDate", "Provide a valid Publication Date");

    internal static Error NotValidFileId=> Error.Validation("BookErrors.NotValidFileId", "Provide a valid file id");

    internal static Error EmptyTitle=> Error.Validation("BookErros.EmptyTitle","Provide a valid title");
    internal static Error EmptyISBN => Error.Validation("BookErros.EmptyISBN", "Provide a valid ISBN");
    internal static Error InvalidinternalationDate => Error.Validation("BookErros.Emptyinternalation date", "Provide a valid internalation date");
    internal static Error EmptyGenere => Error.Validation("BookErros.EmptyGenere", "Provide a valid Genere");

    internal static Error NotValidId=>Error.Validation("BookErrors.NotValidId","Provide a valid book id");
}
