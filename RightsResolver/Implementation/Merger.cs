using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class Merger
    {
        public Rights MergeRights(List<Rights> allRights)
        {
            var platformAccesses = allRights.Select(right => right.PlatformAccesses).ToList();
            var productAccesses = allRights.Select(right => right.ProductAccesses).ToList();

            return new Rights(MergePlatformAccesses(platformAccesses),
                MergeProductAccesses(productAccesses));
        }

        private Dictionary<Platform, Role> MergePlatformAccesses(
            List<Dictionary<Platform, Role>> platformAccesses)
        {
            var mergePlatformAccesses = new Dictionary<Platform, Role>();

            foreach (var access in platformAccesses)
            {
                foreach (var platform in access.Keys)
                {
                    if (!mergePlatformAccesses.ContainsKey(platform))
                        mergePlatformAccesses.Add(platform, access[platform]);
                    else
                    {
                        mergePlatformAccesses[platform] =
                            EnumExtention.Max(mergePlatformAccesses[platform], access[platform]);
                    }
                }
            }

            return mergePlatformAccesses;
        }

        private Dictionary<Platform, List<ProductRole>> MergeProductAccesses(
            List<Dictionary<Platform, List<ProductRole>>> productAccesses)
        {
            var mergeProductAccesses = new Dictionary<Platform, List<ProductRole>>();

            foreach (var access in productAccesses)
            {
                foreach (var platform in access.Keys)
                {
                    if (!mergeProductAccesses.ContainsKey(platform))
                        mergeProductAccesses.Add(platform, access[platform]);
                    else
                    {
                        mergeProductAccesses[platform].AddRange(access[platform]);
                    }

                    mergeProductAccesses[platform] = MergeProductRoles(mergeProductAccesses[platform]);
                }
            }

            return mergeProductAccesses;
        }

        private List<ProductRole> MergeProductRoles(List<ProductRole> roles)
        {
            var productRoles = new Dictionary<string, Role>();

            foreach (var productRole in roles)
            {
                var product = productRole.ProductId;
                if (!productRoles.ContainsKey(product))
                    productRoles.Add(product, productRole.Role);
                else
                {
                    productRoles[product] =
                        EnumExtention.Max(productRoles[product], productRole.Role);
                }
            }

            return productRoles.Select(x => new ProductRole(x.Key, x.Value)).ToList();
        }
    }
}