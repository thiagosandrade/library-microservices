using MediatR;

namespace Library.Shop.Business.CQRS.Contracts.Commands
{
    public class RemoveProductCommand : IRequest
    {
        public RemoveProductCommand(int id, int productId)
        {
            Id = id;
            ProductId = productId;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}