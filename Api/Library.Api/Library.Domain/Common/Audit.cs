namespace Library.Domain.Common
{
    public class Audit:Entity
    {
        public  string? CreatedBy { get; set; }
        public  string? ModifiedBy { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public Audit(){}
        public Audit(Guid id):base(id){}


    }
}
