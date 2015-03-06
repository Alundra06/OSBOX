using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using OSBOX.Data.ViewModels;
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
using OSBOX.Common.Controllers;



namespace OSBOX.Common.Controllers
{
    
    
    [Authorize]
    public class AccountController : Controller
    {

        private ApplicationDbContext UserDB = new ApplicationDbContext();
        private IEmployeeContext EmployeeDB;
        private ICustomerContext CustomerDB;
        private IFolderContext FolderDB;
        private IFolderController FolderController;
        private IEmailController emailcontroller;
        private IAddressContext AddressDB;
        
        public AccountController(UserManager<ApplicationUser> userManager, IEmployeeContext EmployeeDBContext,IFolderContext FolderDBContext,ICustomerContext CustomerDBContext,IFolderController foldercontroller,IEmailController emailcontroller,IAddressContext AddressContextDB)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            EmployeeDB = EmployeeDBContext;
            FolderDB = FolderDBContext;
            CustomerDB = CustomerDBContext;
            FolderController = foldercontroller;//we need it to create a folder for the new registered user
            emailcontroller = emailcontroller;
            AddressDB = AddressContextDB;
        }
       
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
                        //send an email to the admin for log
                       // emailcontroller.SendEmailUsingGoogleSMTP(user.UserName + " loggedin", "A user just logged in", "ouassim.sadellah@gmail.com");
                       
                        
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

                        //send an email to the admin for log
                        //emailcontroller.SendEmailUsingGoogleSMTP(user.UserName + " loggedin", "A user just logged in", "ouassim.sadellah@gmail.com");
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
                    addRoleToUser(user.Id, "Customer");
                    System.Web.Security.Roles.DeleteCookie();//http://stackoverflow.com/questions/2008618/refresh-asp-net-role-provider
                    //Add to the customer table
                    
                    CustomerModel customer = new CustomerModel();
                    customer.First_Name = model.First_Name;
                    customer.Last_Name = model.Last_Name;
                    customer.Business_Name = model.Business_Name;
                    customer.UserId = user.Id;
                    //TODO i need to look at this and use .getallcustomers instead of customers
                    CustomerDB.Customers.Add(customer);
                    
                    CustomerDB.Add(customer);
                    CustomerDB.Commit();
                   

                    string ParentFolderID = FolderDB.GetAllFolders.Where(s=>s.Type=="Root").First().FolderID ;

                    // Create a Folder in the server for the new customer with his name and the business name
                    FolderController.CreateFolderOnServer(user.UserName, ParentFolderID);
                    FolderController.CreateFolderOnDropBox(user.UserName, ParentFolderID);


                    //sign in the current customer
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Customer", "Home");
          
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
        [Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            CustomerRegisterViewModel cr = LoadStatesToTheList();
            
            return View(cr);
        }

        private static CustomerRegisterViewModel LoadStatesToTheList()
        {
            CustomerRegisterViewModel cr = new CustomerRegisterViewModel();
            cr.States =
                new List<System.Web.Mvc.SelectListItem>()
    {
        new System.Web.Mvc.SelectListItem() {Text="Alabama", Value="AL"},
        new System.Web.Mvc.SelectListItem() { Text="Alaska", Value="AK"},
        new System.Web.Mvc.SelectListItem() { Text="Arizona", Value="AZ"},
        new System.Web.Mvc.SelectListItem() { Text="Arkansas", Value="AR"},
        new System.Web.Mvc.SelectListItem() { Text="California", Value="CA"},
        new System.Web.Mvc.SelectListItem() { Text="Colorado", Value="CO"},
        new System.Web.Mvc.SelectListItem() { Text="Connecticut", Value="CT"},
        new System.Web.Mvc.SelectListItem() { Text="District of Columbia", Value="DC"},
        new System.Web.Mvc.SelectListItem() { Text="Delaware", Value="DE"},
        new System.Web.Mvc.SelectListItem() { Text="Florida", Value="FL"},
        new System.Web.Mvc.SelectListItem() { Text="Georgia", Value="GA"},
        new System.Web.Mvc.SelectListItem() { Text="Hawaii", Value="HI"},
        new System.Web.Mvc.SelectListItem() { Text="Idaho", Value="ID"},
        new System.Web.Mvc.SelectListItem() { Text="Illinois", Value="IL"},
        new System.Web.Mvc.SelectListItem() { Text="Indiana", Value="IN"},
        new System.Web.Mvc.SelectListItem() { Text="Iowa", Value="IA"},
        new System.Web.Mvc.SelectListItem() { Text="Kansas", Value="KS"},
        new System.Web.Mvc.SelectListItem() { Text="Kentucky", Value="KY"},
        new System.Web.Mvc.SelectListItem() { Text="Louisiana", Value="LA"},
        new System.Web.Mvc.SelectListItem() { Text="Maine", Value="ME"},
        new System.Web.Mvc.SelectListItem() { Text="Maryland", Value="MD"},
        new System.Web.Mvc.SelectListItem() { Text="Massachusetts", Value="MA"},
        new System.Web.Mvc.SelectListItem() { Text="Michigan", Value="MI"},
        new System.Web.Mvc.SelectListItem() { Text="Minnesota", Value="MN"},
        new System.Web.Mvc.SelectListItem() { Text="Mississippi", Value="MS"},
        new System.Web.Mvc.SelectListItem() { Text="Missouri", Value="MO"},
        new System.Web.Mvc.SelectListItem() { Text="Montana", Value="MT"},
        new System.Web.Mvc.SelectListItem() { Text="Nebraska", Value="NE"},
        new System.Web.Mvc.SelectListItem() { Text="Nevada", Value="NV"},
        new System.Web.Mvc.SelectListItem() { Text="New Hampshire", Value="NH"},
        new System.Web.Mvc.SelectListItem() { Text="New Jersey", Value="NJ"},
        new System.Web.Mvc.SelectListItem() { Text="New Mexico", Value="NM"},
        new System.Web.Mvc.SelectListItem() { Text="New York", Value="NY"},
        new System.Web.Mvc.SelectListItem() { Text="North Carolina", Value="NC"},
        new System.Web.Mvc.SelectListItem() { Text="North Dakota", Value="ND"},
        new System.Web.Mvc.SelectListItem() { Text="Ohio", Value="OH"},
        new System.Web.Mvc.SelectListItem() { Text="Oklahoma", Value="OK"},
        new System.Web.Mvc.SelectListItem() { Text="Oregon", Value="OR"},
        new System.Web.Mvc.SelectListItem() { Text="Pennsylvania", Value="PA"},
        new System.Web.Mvc.SelectListItem() { Text="Rhode Island", Value="RI"},
        new System.Web.Mvc.SelectListItem() { Text="South Carolina", Value="SC"},
        new System.Web.Mvc.SelectListItem() { Text="South Dakota", Value="SD"},
        new System.Web.Mvc.SelectListItem() { Text="Tennessee", Value="TN"},
        new System.Web.Mvc.SelectListItem() { Text="Texas", Value="TX"},
        new System.Web.Mvc.SelectListItem() { Text="Utah", Value="UT"},
        new System.Web.Mvc.SelectListItem() { Text="Vermont", Value="VT"},
        new System.Web.Mvc.SelectListItem() { Text="Virginia", Value="VA"},
        new System.Web.Mvc.SelectListItem() { Text="Washington", Value="WA"},
        new System.Web.Mvc.SelectListItem() { Text="West Virginia", Value="WV"},
        new System.Web.Mvc.SelectListItem() { Text="Wisconsin", Value="WI"},
        new System.Web.Mvc.SelectListItem() { Text="Wyoming", Value="WY"}
    };
            return cr;
        }



        //Customer registration complete form 
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        // [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> RegisterCustomer(CustomerRegisterViewModel model, FormCollection form, bool CreateBusiness)
        {
            if (ModelState.IsValid)
            {

                //check if the ID_Code exists already
                if(CustomerDB.GetAllCustomers.Where(s=>s.ID_Code == model.ID_Code).Any())
                { 
                   
               
                    ModelState.AddModelError("ExistingID_Code", "The ID code exists already!");
                    
                    //Add the states to the model for rerendering
                    model.States = LoadStatesToTheList().States;
                    return View(model);
                }
                
                
                var user = new ApplicationUser()
                {
                    UserName = model.ID_Code //The ID_Code is the UserName , Per OSBOX request
                };
               var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Add The role Customer to this new created user
                    addRoleToUser(user.Id, "Customer");

                    //Call the Add the Customer to the database method
                    CustomerModel customer = AddCustomerRecord(model, user);

                    AddAddressRecord(model,customer.CustomerId);


                    string ParentFolderID = FolderDB.GetAllFolders.Where(s => s.Type == "Root").First().FolderID;

                    // Create a Folder in the server for the new customer with his name and the business name
                    FolderController.CreateFolderOnServer(user.UserName, ParentFolderID);
                    FolderController.CreateFolderOnDropBox(user.UserName, ParentFolderID);
                    
                    
                    
                    if(CreateBusiness)
                    {
                        TempData["CustomerID"] = customer.CustomerId;
                        TempData["NotificationMessage"] = "The customer account was created successfully!";
                        return RedirectToAction("Create", "Business");
                    }
                    else
                    {
                        //send the notification message
                        TempData["NotificationMessage"] = "The customer account was successfully created";

                        return RedirectToAction("Index", "Customer");
                    }
                    
                   

                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddAddressRecord(CustomerRegisterViewModel model,int CustomerID)
        {
            AddressModel am = new AddressModel();

            am.Addresstype = model.Addresstype;
            am.City = model.City;
            am.CustomerId = CustomerID;
            am.State = model.State;
            am.Street_Adr1 = model.Street_Adr1;
            am.Street_Adr2 = model.Street_Adr2;
            am.ZipCode = model.ZipCode;
            AddressDB.Add(am);
            AddressDB.Commit();
        }

        

        private CustomerModel AddCustomerRecord(CustomerRegisterViewModel model, ApplicationUser user)
        {
            //TODO We need to change this and implement IoC and DI
            //Add to the customer table
            //CustomerContext DBCus = new CustomerContext();
            CustomerModel customer = new CustomerModel();
            customer.ID_Code = model.ID_Code;
            customer.First_Name = model.First_Name;
            customer.Middle_name = model.Middle_name;
            customer.Last_Name = model.Last_Name;
            customer.Business_Name = model.Business_Name;
            customer.Name_Prefix = model.Name_Prefix;
            customer.LegalName = model.LegalName;
            customer.Phone_Number = model.Phone_Number;
            customer.Cell_Phone = model.Cell_Phone;
            customer.Fax = model.Fax;
            customer.Email = model.Email;
            customer.UserId = user.Id;
            //Check if there is a record for a customer in the DB
            //if (!CustomerDB.GetAllCustomers.Any())
            //    customer.CustomerId = 1;
            //else
            //    customer.CustomerId = CustomerDB.GetAllCustomers.Max(s => s.CustomerId) + 1;


            CustomerDB.Add(customer);
            CustomerDB.Commit();
            return customer;
        }



        public JsonResult AutoCompleteAccountID(string term)
        {

             
            var ListOfAccountIDs = CustomerDB.GetAllCustomers.Where(s => s.ID_Code.StartsWith(term))
                .Select(r =>new
            {
                label = r.ID_Code
            }); 
            return Json(ListOfAccountIDs, JsonRequestBehavior.AllowGet);

            //get this code from the videos on MVC asp.net

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

                    //Check if there is a record for a customer in the DB
                    if (!EmployeeDB.GetAllEmployees.Any())
                        employee.Employee_ID = 1;
                    else
                        employee.Employee_ID = EmployeeDB.GetAllEmployees.Max(s => s.Employee_ID) + 1;



                    //EmployeeDB.Employees.Add(employee);
                    EmployeeDB.Add(employee);
                    EmployeeDB.Commit();



                    TempData["NotificationMessage"] = "The employee account was created successfully!";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            GetRoles();
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

        
        //A test controller to display test fake page
        public ActionResult test()
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