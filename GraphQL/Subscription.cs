using Demo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace Demo.GraphQL
{
    public class Subscription{

        [Subscribe]
        [Topic]
        public Platform onAddPlatform([EventMessage]Platform platform)=>platform;
    }
}