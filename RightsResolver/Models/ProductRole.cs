using JetBrains.Annotations;

namespace RightsResolver
{
    public class ProductRole
    {
        [NotNull] public string ProductId { get; }
        public Role Role { get; }

        public ProductRole([NotNull] string productId, Role role)
        {
            ProductId = productId;
            Role = role;
        }
    }
}
