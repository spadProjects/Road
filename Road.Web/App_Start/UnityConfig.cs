using System.Web.Mvc;
using Road.Infrastructure.Repositories;
using Road.Web.Controllers;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Road.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<ArticleCategoriesRepository>();
            container.RegisterType<ArticlesRepository>();
            container.RegisterType<UsersRepository>();
            //container.RegisterType<UsersController>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}