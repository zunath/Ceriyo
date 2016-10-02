using Ceriyo.Toolset.WPF.Events.Ability;
using Ceriyo.Toolset.WPF.Events.DataEditor;
using Ceriyo.Toolset.WPF.Views.AbilityEditorView;
using Moq;
using NUnit.Framework;
using Prism.Events;

namespace Ceriyo.Toolset.WPF.Tests
{
    public class AbilityEditorViewModelTests
    {
        private readonly Mock<IEventAggregator> _mockAggregator;
        private Mock<AbilityChangedEvent> _mockAbilityChangedEvent;
        private Mock<AbilityCreatedEvent> _mockAbilityCreatedEvent;
        private Mock<AbilityDeletedEvent> _mockAbilityDeletedEvent;
        private Mock<DataEditorClosedEvent> _mockDataEditorClosedEvent;


        public AbilityEditorViewModelTests()
        {
            
            _mockAbilityChangedEvent = new Mock<AbilityChangedEvent>();
            _mockAbilityCreatedEvent = new Mock<AbilityCreatedEvent>();
            _mockAbilityDeletedEvent = new Mock<AbilityDeletedEvent>();
            _mockDataEditorClosedEvent = new Mock<DataEditorClosedEvent>();

            _mockAggregator = new Mock<IEventAggregator>();
            _mockAggregator.Setup(x => x.GetEvent<AbilityChangedEvent>()).Returns(_mockAbilityChangedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<AbilityCreatedEvent>()).Returns(_mockAbilityCreatedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<AbilityDeletedEvent>()).Returns(_mockAbilityDeletedEvent.Object);
            _mockAggregator.Setup(x => x.GetEvent<DataEditorClosedEvent>()).Returns(_mockDataEditorClosedEvent.Object);
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
            AbilityEditorViewModel model = new AbilityEditorViewModel(_mockAggregator.Object);
            model.NewCommand.Execute();

            Assert.AreEqual(1, model.Abilities.Count);
        }

        [Test]
        public void NewCommand_ExecuteMultiple_NamesShouldMatch()
        {
            AbilityEditorViewModel model = new AbilityEditorViewModel(_mockAggregator.Object);
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
