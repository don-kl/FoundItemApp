using NetTopologySuite.Features;
using FoundItemApp.DTo;
using FoundItemApp.DTo.RegionDTo;
using FoundItemApp.Dto.Region;

namespace FoundItemApp.Interfaces
{
    public interface IRegionServices
    {
        Task<HashSet<string>?> GetAllRegionNames();

        Task<double[]?> GetRegionEnvelope(string regionName);

        Task<Feature?> GetRegionBorders(string name); 
    }
}

