using System.Linq;
using Demo.Data;
using Demo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace Demo.GraphQL.Commands
{
    public class CommandType:ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Represents any Executable commands.");

            descriptor
                .Field(e=>e.Platform)
                .ResolveWith<Resolvers>(p=>p.GetPlatforms(default!,default!))
                .UseDbContext<AppDbContext>()
                .Description("Platform which the command belongs.");
        }

        private class Resolvers
        {
            public Platform GetPlatforms(Command command,[ScopedService] AppDbContext context)
            {
                return context.Platforms.FirstOrDefault(e=>e.Id==command.PlatformId);
            }
        }
    }
}