using MiniCommerce.Identity.Domain.Aggregates.Users.Entities;

namespace MiniCommerce.Identity.Domain.Aggregates.Users.Specifications;

public class UserByEmailSpec : Specification<User>
{
    public UserByEmailSpec(string email)
    {
        Criteria = u => u.Email == email;
    }
}
