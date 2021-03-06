using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Entities.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace Business.DependencyResolvers.Autofact
{
    public class AutofacBusinessModule : Module
    {
        /// <summary>
        /// Manager içinde veri tutulmuyorsa singleton yapılabilir aksi halde örn tüm sepetler aynı olur. Data db de tutuluyorsa yine singleton yapılabilir.
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //program ilk açılırken outofac bellekte bu nesnelerin reflection ıle instance ını alır boylece daha performanslı çalışır. 
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimSevice>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<GroupManager>().As<IGroupService>().SingleInstance();
            builder.RegisterType<EfGroupDal>().As<IGroupDal>().SingleInstance();

            builder.RegisterType<HomeworkManager>().As<IHomeworkService>().SingleInstance();
            builder.RegisterType<EfHomeworkDal>().As<IHomeworkDal>().SingleInstance();

            builder.RegisterType<GroupHomeworkManager>().As<IGroupHomeworkService>().SingleInstance();
            builder.RegisterType<EfGroupHomeworkDal>().As<IGroupHomeworkDal>().SingleInstance();

            builder.RegisterType<FacultyManager>().As<IFacultyService>().SingleInstance();
            builder.RegisterType<EfFacultyDal>().As<IFacultyDal>().SingleInstance();

            builder.RegisterType<SectionManager>().As<ISectionService>().SingleInstance();
            builder.RegisterType<EfSectionDal>().As<ISectionDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
