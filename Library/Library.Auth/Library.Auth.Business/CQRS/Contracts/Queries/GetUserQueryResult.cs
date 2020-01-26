namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetUserQueryResult
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}