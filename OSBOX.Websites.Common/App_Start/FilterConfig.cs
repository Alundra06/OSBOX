﻿using System.Web;
using System.Web.Mvc;

namespace OSBOX.Websites.Common
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
