using AKCPA.Data.DAL;
using AKCPA.Data.Models;
using AKCPA.Data.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace VSSystem.BaseModel.WebSites.Controllers
{
    
    
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationDbContext UserDB = new ApplicationDbContext();
        private IEmployeeContext EmployeeDB;
        //public AccountController()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())), )
        //{
        //}
        public AccountController(UserManager<ApplicationUser> userManager, IEmployeeContext EmployeeDBContext)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            EmployeeDB = EmployeeDBContext;
        }
        //public AccountController(IEmployeeContext EmployeeDBContext, UserManager<ApplicationUser> userManager)
        //{
           
        //    EmployeeDB = EmployeeDBContext;
        //    UserManager = userManager;
        //}
        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult EmployeeLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    //Check if the user is a customer
                    IList<string> GetUserRoles = UserManager.GetRoles(user.Id);
                    String iscustomer = GetUserRoles[0].ToString();
                    if (iscustomer == "Customer")
                    {
                        await SignInAsync(user, model.RememberMe);

                        return RedirectToAction("Customer", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                    //return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }



            // If we got this far, something failed, redisplay form
           
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeLogin(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                     
                    IList<string> GetUserRoles = UserManager.GetRoles(user.Id);
                    String iscustomer = GetUserRoles[0].ToString();
                    if (iscustomer != "Customer")
                    {
                        await SignInAsync(user, model.RememberMe);
                        return RedirectToAction("SystemIndex", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form

            return View(returnUrl);
        }




        //Customer registration short form
        // GET: /Account/Register
        //[Authorize(Roles = "Administrator")]
                [AllowAnonymous]
        public ActionResult Register()
        {
                    return View();
        }



         //Customer registration short form
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
       // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Register(CustomerRegisterViewModel model,FormCollection form,string Raction, string Rcontroller)
        {
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser() 
                { 
                    UserName = model.UserName
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    //Add to the customer table
                    CustomerContext DBCus = new CustomerContext();
                    CustomerModel customer = new CustomerModel();
                    customer.First_Name = model.First_Name;
                    customer.Last_Name = model.Last_Name;
                    customer.Business_Name = model.Business_Name;
                    customer.UserId = user.Id;
                    DBCus.Customers.Add(customer);
                    DBCus.SaveChanges();

                    await SignInAsync(user, isPersistent: false);

                    //Add The role Customer to this new created user
                    addRoleToUser(user.Id,"Customer");



                    //Create a DropBox folder for the costumer
                    //return RedirectToAction("CreateFolder", "DropBox", new { FolderName = user.UserName, taction = Raction, tcontroller = Rcontroller }); 
                    string Customers_Folder_id = "dc3382b6-b3f9-4b75-b352-d3acac1f1f6a";

                    // Create a Folder in the server for the new customer with his name and the business name
                    return RedirectToAction("Create_Folder", "Folder", new { Folder_Name = user.UserName, Parent_Folder = Customers_Folder_id, Folder_Full_Path = "~/Uploads/AKCPA File System/Customers", taction = Raction, tcontroller = Rcontroller }); 
          
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        //Customer registration complete form
        // GET: /Account/Register
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            return View();
        }



        //Customer registration complete form 
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> RegisterCustomer(CustomerRegisterViewModel model, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    UserName = model.UserName
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //Add to the customer table
                    CustomerContext DBCus = new CustomerContext();
                    CustomerModel customer = new CustomerModel();
                    customer.First_Name = model.First_Name;
                    customer.Last_Name = model.Last_Name;
                    customer.Business_Name = model.Business_Name;
                    customer.UserId = user.Id;
                    DBCus.Customers.Add(customer);
                    DBCus.SaveChanges();
                    //Add The role Customer to this new created user
                    addRoleToUser(user.Id, "Customer");
                    
                    
                    //Create a DropBox folder for the costumer
                    //return RedirectToAction("CreateFolder", "DropBox", new { FolderName = user.UserName, taction = "Index", tcontroller = "Customer" });

                    string Customers_Folder_id = "dc3382b6-b3f9-4b75-b352-d3acac1f1f6a";

                    // Create a Folder in the server for the new customer with his name and the business name
                    return RedirectToAction("Create_Folder", "Folder", new { Folder_Name = user.UserName, Parent_Folder = Customers_Folder_id, Folder_Full_Path = "~/Uploads/AKCPA File System/Customers" }); 


                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }








        //get a list of all roles
        private void GetRoles()
        {
            IEnumerable<SelectListItem> Employeess =
            (from Roles in UserDB.Roles.ToArray()
             select new SelectListItem
             {
                 Text = Roles.Name,
                 Value = Roles.Id
             }).ToArray();
           
            var Templist = Employeess.ToList();
            ViewBag.Roles = Templist.ToArray();


        }


        //Employee registration
        // GET: /Account/Register
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        public ActionResult EmployeeRegister()
        {
            GetRoles();
            return View();
        }



        //Customer registration
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
         //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EmployeeRegister(EmployeeRegisterViewModel model, FormCollection form)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    UserName = model.UserName
                };

                //Create the object for the Employee
                //var employeeDB = new EmployeeContext();
                //var newEmployee = employeeDB.Employees.Create();
                
                
                
                
                //
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    //Get the role name from the role ID
                    string RoleName = UserDB.Roles.Where(a => a.Id == model.Role).Single().Name;
                    addRoleToUser(user.Id, RoleName);

                    //Add to the employee table
                    EmployeeModel employee = new EmployeeModel();
                    employee.Created_Date = model.Created_Date;
                    employee.UserId= user.Id;
                    employee.FirstName = model.FirstName;
                    employee.Hire_Date = model.Hire_Date;
                    employee.LastName = model.LastName;
                    employee.Manager_ID = model.Manager_ID;
                    employee.Modified_Date = model.Modified_Date;
                    employee.Salary = model.Salary;
                    employee.Salary_Type = model.Salary_Type;
                    employee.SickLeave_Hrs = model.SickLeave_Hrs;
                    employee.Title = model.Title;
                    employee.Vacation_Hrs = model.Vacation_Hrs;
                    EmployeeDB.Employees.Add(employee);
                    EmployeeDB.Commit();




                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public void addRoleToUser(string UserId,string Myrole)
        {
            //add the role



            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(UserId,Myrole);
            userManager.Dispose();
            userStore.Dispose();
        }
        private void AddRole(String RoleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var role = new IdentityRole();
            role.Name = RoleName;
            roleManager.Create(role);
        }
        
        
        

        
        
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        

                        await SignInAsync(user, isPersistent: false);
                        //Add The role Customer to this new created user
                        addRoleToUser(user.Id, "Customer");
                        //Create a DropBox folder for the costumer
                        return RedirectToAction("CreateFolder", "DropBox", new { FolderName = user.UserName, action = "Home", controller = "Index" });
                       // return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


    

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
              
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}