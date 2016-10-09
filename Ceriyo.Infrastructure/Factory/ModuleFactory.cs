using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;

namespace Ceriyo.Infrastructure.Factory
{
    public class ModuleFactory: IModuleFactory
    {
        private readonly IValidatorFactory _validatorFactory;

        public ModuleFactory(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public ModuleData Create()
        {
            ModuleData data = new ModuleData
            {
                ValidatorFactory = _validatorFactory
            };

            return data;
        }
    }
}
