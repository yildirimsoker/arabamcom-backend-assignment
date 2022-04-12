using Arabam.Com.Domain.Enums;

namespace Arabam.Com.Domain.Common
{
    public class Filter
    {
       
        public int Offset { get; set; }
        public int Next { get; set; }
        public string? OrderByColumnName { get; set; }
        public OrderByType OrderBy { get; set; }
        public IDictionary<string, object> Where { get; set; } = new Dictionary<string, object>();

    }
}
