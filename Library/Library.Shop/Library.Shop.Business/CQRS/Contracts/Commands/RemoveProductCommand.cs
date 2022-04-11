using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public RemoveProductCommand(int id, int productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}