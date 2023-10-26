using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Components
{
    public class PieChart:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
