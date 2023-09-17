using Microsoft.Azure.CosmosRepository;

namespace cosmos_container.Data
{
    public class Person : Item
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}