using OSBOX.Common.Controllers;

using OSBOX.Data.DAL;
using OSBOX.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;


namespace OSBOX.Common.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
      
            private IKernel kernel;
            public NinjectDependencyResolver(IKernel kernelParam)
            {
                kernel = kernelParam;
                AddBindings();
            }
            public object GetService(Type serviceType)
            {
                return kernel.TryGet(serviceType);
            }
            public IEnumerable<object> GetServices(Type serviceType)
            {
                return kernel.GetAll(serviceType);
            }
            private void AddBindings()
            {

                
                //Context DB Binding
                kernel.Bind<IFolderContext>().To<FolderContext>();
                kernel.Bind<IBusinessContext>().To<BusinessContext>();
                kernel.Bind<IInvoiceContext>().To<InvoiceContext>();
                kernel.Bind<ICustomerContext>().To<CustomerContext>();
                kernel.Bind<IFileContext>().To<FileContext>();
                kernel.Bind<ITaskContext>().To<TaskContext>();
                kernel.Bind<IEmployeeContext>().To<EmployeeContext>();
                kernel.Bind<IPaymentContext>().To<PaymentContext>();
                kernel.Bind<IPaymentTypeContext>().To<PaymentTypeContext>();
                kernel.Bind<IPaymentHistoryContext>().To<PaymentHistoryContext>();
                kernel.Bind<IServiceTypeContext>().To<ServiceTypeContext>();
                kernel.Bind<IAddressContext>().To<AddressContext>();

                
                
                
                
                
                
                
                
                
                //Controllers binding
                kernel.Bind<IFolderController>().To<FolderController>().InSingletonScope();
                kernel.Bind<IFileController>().To<FileController>();
                
                kernel.Bind<ICustomerController>().To<CustomerController>().InSingletonScope();
                kernel.Bind<IEmployeeController>().To<EmployeeController>();
                kernel.Bind<IEmailController>().To<EmailController>();
               
                
               

                
                //For the builtin IUserStore
                //Check this website: http://stackoverflow.com/questions/21939051/what-ninject-binding-should-i-use
                kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
                //kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);
                kernel.Bind<IPrincipal>().ToMethod(c => HttpContext.Current.User);
                kernel.Bind<ICurrentUser>().To<Currentuser>();
                
            }
        
    }
}
