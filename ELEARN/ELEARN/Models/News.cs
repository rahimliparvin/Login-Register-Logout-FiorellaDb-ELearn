using System.Diagnostics.SymbolStore;

namespace ELEARN.Models
{
    public class News : BaseEntity
    {
       public string? Title { get; set; }
       public string? Author { get; set; }

        public string? Image { get; set; }

    }
}
