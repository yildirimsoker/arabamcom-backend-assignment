namespace Arabam.Com.Domain.Entities
{
    public class AdvertVisits : BaseEntity
    {
        public int AdvertId { get; set; }
        public string? IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public virtual Adverts? Advert { get; set; }
    }
}
