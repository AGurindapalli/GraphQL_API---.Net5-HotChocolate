using System.Linq;
using Demo.Data;
using Demo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace Demo.GraphQL.Platforms
{
    public class PlatformType:ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
           descriptor.Description("Represents any Software or service that has a CLI");

           descriptor.Field(p=>p.LicenseKey).Ignore();

           descriptor
            .Field(p=>p.Commands)
            .ResolveWith<Resolvers>(e=>e.GetCommands(default!,default!))
            .UseDbContext<AppDbContext>()
            .Description("List of available commands");
        
        }
        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform,[ScopedService] AppDbContext context)
            {
                return context.Commands.Where(e=>e.PlatformId==platform.Id);
            }
        }
    }
}