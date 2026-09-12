namespace Library.Domain.Copies;

internal sealed class CopyErrors
{
    internal static Error InvalidBookId => Error.Validation("CopyErrors.", "Provide a valid book id");
    internal static Error InvalidCopyId=>Error.Validation("CopyErrors.","Provide a valid copy id");
}
