using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Commands
{
    public class AddProductCommand : IRequest
    {
        public AddProductCommand(int id, int productId)
        {
            Id = id;
            ProductId = productId;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}