using System;
using System.Security.Principal;
namespace OSBOX.Common.Controllers
{
   public  interface ICurrentUser
    {
        string DisplayName { get; }
        bool IsAuthenticated { get; }
        string UserID { get; }
        //bool isCustomer { get; }
        bool IsInRole(string role);
    }
}
