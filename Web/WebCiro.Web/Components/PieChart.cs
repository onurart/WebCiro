using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Web.Components
{
    public class PieChart:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
