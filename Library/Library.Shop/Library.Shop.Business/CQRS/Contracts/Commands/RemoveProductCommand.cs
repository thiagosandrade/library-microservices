using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public RemoveProductCommand(int id, int productId, int quantity, string user)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            User = user;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string User { get; set; }
    }
}