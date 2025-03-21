using NetTopologySuite.Features;
using FoundItemApp.Dto.Region;

namespace FoundItemApp.Interfaces
{
    public interface IRegionServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<int, string>?> GetAllRegionNames();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        Task<double[]?> GetRegionEnvelope(int regionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        Task<Feature?> GetRegionBorders(int regionId); 
    }
}

