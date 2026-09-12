namespace Library.Api.Requests;

public sealed class FileValidator:AbstractValidator<BookReq>
{
    public FileValidator()
    {
        RuleFor(file => file.Image)
            .Must(i=>i.Length<15000000)
            .WithMessage("File Too Large")
            .Must(f => {
            BinaryReader binaryReader = new BinaryReader(f.OpenReadStream());
            byte[] bytes=binaryReader.ReadBytes(2);
            string Extn=BitConverter.ToString(bytes);   
            return LibrarySettings.AllowedExtns.Contains(Extn);
        }).WithMessage("File Not Allowed")
        .When(f=>f is not null);
    }
}
