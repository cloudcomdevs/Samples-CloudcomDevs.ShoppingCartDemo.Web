using CloudComDevs.ShoppingCartDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

           // Database.SetInitializer(new DropCreateDatabaseAlways<ProductContext>());
            // Initialize the product database.
         //   Database.SetInitializer(new ProductDatabaseInitializer());

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);


            Database.SetInitializer<ProductContext>(new DropCreateDatabaseIfModelChanges<ProductContext>());
          
        }
    }


    public class SimpleMembershipInitializer
    {
        public SimpleMembershipInitializer()
        {
            using (var context = new UsersContext())
                context.UserProfiles.Find(1);

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("webadmin", false) == null)
            {
                membership.CreateUserAndAccount("webadmin", "webadmin");
            }
            if (!roles.GetRolesForUser("webadmin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "webadmin" }, new[] { "Admin" });
            } 

        }
    }
}