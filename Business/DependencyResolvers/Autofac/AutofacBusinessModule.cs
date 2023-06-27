﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //Çalışan uygulama

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //Uygulama içinde impl edilmiş interface'leri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()               //ve onlar için AspectInterceptorSelector'u çağır yani analiz et Aspect'lerini al diyor.
                }).SingleInstance();
        }
    }
}