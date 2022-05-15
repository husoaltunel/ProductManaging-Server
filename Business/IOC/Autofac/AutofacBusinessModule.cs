using Autofac;
using AutoMapper;
using Core.Managers.Auth.Commands.Register;
using Core.Utilities.Mapping.AutoMapper;
using Core.Utilities.Hashing;
using Core.Utilities.Hashing.Abstract;
using Core.Utilities.Security;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core.IOC.Autofac
{
    public class AutofacBusinessModule : Module
    {
        private readonly IConfiguration _Configuration;
        public AutofacBusinessModule(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }
             )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            builder.Register<IDbConnection>(connection => new NpgsqlConnection(_Configuration.GetConnectionString("DbProductManagingConnection")));
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<HashingHelper>().As<IHashingHelper>();
        }

    }
}
