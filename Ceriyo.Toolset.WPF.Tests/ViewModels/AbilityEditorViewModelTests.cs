using System.Threading;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Infrastructure.Services;
using Ceriyo.Infrastructure.WPF.Factory.Contracts;
using Ceriyo.Infrastructure.WPF.Observables;
using Ceriyo.Toolset.WPF.Views.AbilityEditorView;
using Moq;
using NUnit.Framework;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Tests.ViewModels
{
    public class AbilityEditorViewModelTests
    {
        private readonly EventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IPathService _pathService;
        private readonly Mock<IObservableDataFactory> _mockObservableFactory;

        public AbilityEditorViewModelTests()
        {
            _eventAggregator = new EventAggregator();

            var mockLogger = new Mock<ILogger>();
            _dataService = new DataService(mockLogger.Object);
            _pathService = new PathService();
            
            _mockObservableFactory = new Mock<IObservableDataFactory>();
            _mockObservableFactory.Setup(x => x.Create<AbilityDataObservable>()).Returns(() => new AbilityDataObservable());

        }

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void NewCommand_Execute_ShouldBeOneAbility()
        {
            AbilityEditorViewModel model = new AbilityEditorViewModel(
                _eventAggregator,
                _dataService, 
                _pathService,
                _mockObservableFactory.Object);
            model.NewCommand.Execute();

            Assert.AreEqual(1, model.Abilities.Count);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void NewCommand_ExecuteMultiple_NamesShouldMatch()
        {
            AbilityEditorViewModel model = new AbilityEditorViewModel(
                _eventAggregator, 
                _dataService, 
                _pathService,
                _mockObservableFactory.Object);
            model.NewCommand.Execute();
            model.NewCommand.Execute();
            model.NewCommand.Execute();
            model.NewCommand.Execute();

            Assert.AreEqual("Ability1", model.Abilities[0].Name);
            Assert.AreEqual("Ability2", model.Abilities[1].Name);
            Assert.AreEqual("Ability3", model.Abilities[2].Name);
            Assert.AreEqual("Ability4", model.Abilities[3].Name);

        }
        

    }
}
