using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Commands
{
    public class AddProductCommand : IRequest
    {
        public AddProductCommand(int id, int productId, int quantity)
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