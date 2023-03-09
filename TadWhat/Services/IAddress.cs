using Dadata;
using Dadata.Model;
using System.Threading.Tasks;

namespace TadWhat.Services
{
    public interface IAddress
    {
        Address Address { get; }

        Task Start(string city, string street, string house, string token, string secret);
    }

    public class AddreseRussian : IAddress
    {
        private Address address;
        public Address Address => address;

        public async Task Start(string city,string street, string house, string token, string secret){

            var api = new CleanClientAsync(token, secret);
            address = await api.Clean<Address>($"{city} {street} {house}");
        }
    }
}
