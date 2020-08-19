using System;

namespace Demo.Contracts
{
    public interface IUserCreated
    {
        Guid CorrelationId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}