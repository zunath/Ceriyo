using System.Threading;
using Ceriyo.Core.Services.Contracts;
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
        private readonly Mock<IObservableDataFactory> _mockObservableFactory;
        private readonly Mock<IModuleDataService> _mockModuleDataService;

        public AbilityEditorViewModelTests()
        {
            _eventAggregator = new EventAggregator();
            
            _mockObservableFactory = new Mock<IObservableDataFactory>();
            _mockObservableFactory.Setup(x => x.Create<AbilityDataObservable>()).Returns(() => new AbilityDataObservable());

            _mockModuleDataService = new Mock<IModuleDataService>();
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
                _mockObservableFactory.Object,
                _mockModuleDataService.Object);
            model.NewCommand.Execute();

            Assert.AreEqual(1, model.Abilities.Count);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void NewCommand_ExecuteMultiple_NamesShouldMatch()
        {
            AbilityEditorViewModel model = new AbilityEditorViewModel(
                _eventAggregator, 
                _mockObservableFactory.Object,
                _mockModuleDataService.Object);
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
