using System;

namespace Demo.Contracts
{
    public class UserCreated : IUserCreated
    {
        public Guid CorrelationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}