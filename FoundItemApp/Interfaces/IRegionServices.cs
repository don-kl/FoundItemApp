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
        Task<List<string>?> GetAllRegionNames();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        Task<GetRegionEnvelopeDto?> GetRegionEnvelope(string regionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Feature?> GetRegionBorders(string name); 
    }
}

