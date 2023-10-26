using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Components
{
    public class ReportBranchDataGrid : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
