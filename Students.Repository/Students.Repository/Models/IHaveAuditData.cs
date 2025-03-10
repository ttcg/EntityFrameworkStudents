namespace Students.Repository.Models
{
    public interface IHaveAuditData
    {
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
