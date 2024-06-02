using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models
{
    public class Cascade
    {

        public IEnumerable<SelectListItem> GemiList { get; set; }
        public IEnumerable<SelectListItem> KalkisList { get; set; }

    }
}
