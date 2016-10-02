using Ceriyo.Core.Contracts;
using Ceriyo.Infrastructure.Services;
using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Events.Module;
using Ceriyo.Toolset.WPF.Views.AbilityEditorView;
using Moq;
using NUnit.Framework;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Tests.ViewModels
{
    public class AbilityEditorViewModelTests
    {
        private readonly Mock<IEventAggregator> _mockAggregator;
        private readonly IDataService _dataService;

        public AbilityEditorViewModelTests()
        {
            var mockLogger = new Mock<ILogger>();
            _dataService = new DataService(mockLogger.Object);
            var mockAbilityChangedEvent = new Mock<AbilityChangedEvent>();
            var mockAbilityCreatedEvent = new Mock<AbilityCreatedEvent>();
            var mockAbilityDeletedEvent = new Mock<AbilityDeletedEvent>();
            var mockDataEditorClosedEvent = new Mock<DataEditorClosedEvent>();
            var mockModuleLoadedEvent = new Mock<ModuleLoadedEvent>();
            var mockModuleClosedEvent = new Mock<ModuleClosedEvent>();

            _mockAggregator = new Mock<IEventAggregator>();
            _mockAggregator.Setup(x => x.GetEvent<AbilityChangedEvent>()).Returns(mockAbilityChangedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<AbilityCreatedEvent>()).Returns(mockAbilityCreatedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<AbilityDeletedEvent>()).Returns(mockAbilityDeletedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<DataEditorClosedEvent>()).Returns(mockDataEditorClosedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<ModuleLoadedEvent>()).Returns(mockModuleLoadedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<ModuleClosedEvent>()).Returns(mockModuleClosedEvent.Object);
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
            AbilityEditorViewModel model = new AbilityEditorViewModel(_mockAggregator.Object, _dataService);
            model.NewCommand.Execute();

            Assert.AreEqual(1, model.Abilities.Count);
        }

        [Test]
        public void NewCommand_ExecuteMultiple_NamesShouldMatch()
        {
            AbilityEditorViewModel model = new AbilityEditorViewModel(_mockAggregator.Object, _dataService);
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
