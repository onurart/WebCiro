﻿using Microsoft.AspNetCore.Mvc;

namespace WebCiro.Components
{
    public class GetOrdersPendingApproval:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
