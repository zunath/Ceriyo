using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Domain.Services.DataServices.Contracts;

namespace Ceriyo.Domain.Services.DataServices
{
    public class DataDomainService: IDataDomainService
    {
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;

        public DataDomainService(IDataService dataService,
            IPathService pathService)
        {
            _dataService = dataService;
            _pathService = pathService;
        }

        public void SaveData<T>(T data)
            where T: IDataDomainObject
        {
            string path = $"{_pathService.ModulesToolsetTempDirectory}{data.DirectoryName}/{data.GlobalID}.dat";
            _dataService.Save(data, path);
        }

        public void DeleteData<T>(T data)
            where T: IDataDomainObject
        {
            string path = $"{_pathService.ModulesToolsetTempDirectory}{data.DirectoryName}/{data.GlobalID}.dat";
            _dataService.Delete(path);
        }
    }
}
