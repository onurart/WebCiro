using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Components
{
    public class ThoseWaitingforShipmentlGrid:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
