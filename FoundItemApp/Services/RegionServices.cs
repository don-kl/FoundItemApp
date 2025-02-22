using FoundItemApp.Data;
using FoundItemApp.Dto.Region;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System.Linq;
using FoundItemApp.Interfaces;

namespace FoundItemApp.Services
{
    public class RegionServices : IRegionServices
    {
        private readonly DataContext _context;

        public RegionServices(DataContext context)
        {
            _context = context;
        }

        public async Task<List<string>?> GetAllRegionNames()
        {
            var names = await _context.Regions.Select(r => r.Name).ToListAsync();

            if(names == null)
            {
                return null;
            }

            var regionsNames = new HashSet<string>(names).ToList();

            return regionsNames;
        }

        public async Task<Double[]?> GetRegionEnvelope(string regionName)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Name == regionName);

            if(region == null)
            {
                return null;
            }

            Envelope env = region.Borders.EnvelopeInternal;

            double[] envelope = { env.MinY, env.MinX, env.MaxY, env.MaxX };

            return envelope;
        }

        public async Task<Feature?> GetRegionBorders(string regionName)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Name == regionName);

            if(region == null)
            {
                return null;
            }

            var envelope = GetRegionEnvelope(regionName);

            AttributesTable attributeTable = new AttributesTable
            {
                {"name", region.Name},
                {"envelope",  envelope}
            };

            Feature feature = new Feature(region.Borders, attributeTable);

            return feature;


        }
    }
}
