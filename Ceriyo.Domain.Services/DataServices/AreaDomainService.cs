using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class AreaDomainService: IAreaDomainService
    {
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public AreaDomainService(IDataService dataService,
            IPathService pathService)
        {
            _dataService = dataService;
            _pathService = pathService;
        }

        public void SaveArea(AreaData data)
        {
            string path = $"{_pathService.ModulesToolsetTempDirectory}Area/{data.GlobalID}.dat";
            _dataService.Save(data, path);
        }

        public void DeleteArea(AreaData data)
        {
            string path = $"{_pathService.ModulesToolsetTempDirectory}Area/{data.GlobalID}.dat";
            _dataService.Delete(path);
        }
    }
}
