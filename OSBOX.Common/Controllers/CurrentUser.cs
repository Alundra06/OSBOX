using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Security;





namespace OSBOX.Common.Controllers
{
    class Currentuser : ICurrentUser
    {
        public Currentuser(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            IsAuthenticated = principal.Identity.IsAuthenticated;
            DisplayName = principal.Identity.GetUserName();
            UserID = principal.Identity.GetUserId();
            //IsAuthenticated = identity.IsAuthenticated;
            //DisplayName = identity.GetUserName();

            _systemPrincipal = principal;
            

          

        }
       
        private readonly IPrincipal _systemPrincipal;

        public string DisplayName { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string UserID { get; private set; }
       // public bool isCustomer { get; private set; }

        public bool IsInRole(string role)
        {
            return _systemPrincipal.IsInRole(role);
            //return true;
        }
    }
}
