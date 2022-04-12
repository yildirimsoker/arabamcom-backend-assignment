namespace Arabam.Com.Application.Adverts.Queries.GetAdvertsWithPagination
{
    public class GetAdvertsWithPaginationDTO
    {
        public int Id { get; set; }
        public string? ModelName { get; set; }
        public string? CategoryName { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public int KM { get; set; }
        public string? Color { get; set; }
        public string? Gear { get; set; }
        public string? Fuel { get; set; }
        public string? FirstPhoto { get; set; }
    }
}
