using System.Threading;
using System.Threading.Tasks;
using Demo.Data;
using Demo.GraphQL.Commands;
using Demo.GraphQL.Platforms;
using Demo.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace Demo.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
         [ScopedService] AppDbContext context,
         [Service] ITopicEventSender eventSender,
         CancellationToken cancellationToken
         )
        {
            var platform = new Platform{
                Name=input.Name
            };
            context.Add(platform);
            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(Subscription.onAddPlatform),platform,cancellationToken);
            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService]AppDbContext context)
        {
            var command = new Command{
                HowTo=input.HowTo,
                CommandLine=input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}