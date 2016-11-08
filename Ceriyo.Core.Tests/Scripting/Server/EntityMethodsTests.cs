using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Core.Services;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting.Server
{
    [TestFixture]
    public class EntityMethodsTests
    {
        private EntityMethods _entityMethods;
        private IEngineService _engineService;

        [SetUp]
        public void SetUp()
        {
            _engineService = new EngineService();
            _entityMethods = new EntityMethods(_engineService);
        }

        [Test]
        public void EntityMethods_GetName_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Nameable component = new Nameable
            {
                Value = "MyName"
            };
            entity.AddComponent(component);
            string name = _entityMethods.GetName(entity);

            Assert.AreEqual(name, "MyName");
        }

        [Test]
        public void EntityMethods_GetName_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string name = _entityMethods.GetName(entity);

            Assert.AreEqual(name, string.Empty);
        }

        [Test]
        public void EntityMethods_SetName_ShouldMatch()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Nameable component = new Nameable
            {
                Value = "InitialName"
            };
            entity.AddComponent(component);
            Assert.AreEqual("InitialName", _entityMethods.GetName(entity));
            _entityMethods.SetName(entity, "NewName");
            Assert.AreEqual("NewName", _entityMethods.GetName(entity));
        }

        [Test]
        public void EntityMethods_SetName_NoComponent_ShouldNotThrow()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();

            Assert.DoesNotThrow(() =>
            {
                _entityMethods.SetName(entity, "NewName");
            });

            var gotName = _entityMethods.GetName(entity);
            Assert.AreEqual(string.Empty, gotName);
        }

        [Test]
        public void EntityMethods_SetName_ShouldTrimTo256Characters()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Nameable component = new Nameable
            {
                Value = "InitialName"
            };
            entity.AddComponent(component);

            string longName = string.Empty;
            for (int x = 0; x < 1000; x++)
            {
                longName += "A";
            }
            _entityMethods.SetName(entity, longName);
            var name = _entityMethods.GetName(entity);

            Assert.AreEqual(_engineService.MaxNameLength, name.Length);
        }


        [Test]
        public void EntityMethods_GetTag_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Tag component = new Tag
            {
                Value = "MyTag"
            };
            entity.AddComponent(component);
            string tag = _entityMethods.GetTag(entity);

            Assert.AreEqual(tag, "MyTag");
        }

        [Test]
        public void EntityMethods_GetTag_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string tag = _entityMethods.GetTag(entity);

            Assert.AreEqual(tag, string.Empty);
        }


        [Test]
        public void EntityMethods_SetTag_ShouldMatch()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Tag component = new Tag
            {
                Value = "InitialTag"
            };
            entity.AddComponent(component);
            Assert.AreEqual("InitialTag", _entityMethods.GetTag(entity));
            _entityMethods.SetTag(entity, "NewTag");
            Assert.AreEqual("NewTag", _entityMethods.GetTag(entity));
        }

        [Test]
        public void EntityMethods_SetTag_NoComponent_ShouldNotThrow()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();

            Assert.DoesNotThrow(() =>
            {
                _entityMethods.SetTag(entity, "NewTag");
            });

            var gotTag = _entityMethods.GetTag(entity);
            Assert.AreEqual(string.Empty, gotTag);
        }

        [Test]
        public void EntityMethods_SetTag_ShouldTrimTo64Characters()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Tag component = new Tag
            {
                Value = "InitialTag"
            };
            entity.AddComponent(component);

            string longTag = string.Empty;
            for (int x = 0; x < 1000; x++)
            {
                longTag += "A";
            }
            _entityMethods.SetTag(entity, longTag);
            var tag = _entityMethods.GetTag(entity);

            Assert.AreEqual(_engineService.MaxTagLength, tag.Length);
        }



        [Test]
        public void EntityMethods_GetResref_ShouldReturnValue()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            Resref component = new Resref
            {
                Value = "MyResref"
            };
            entity.AddComponent(component);
            string resref = _entityMethods.GetResref(entity);

            Assert.AreEqual(resref, "MyResref");
        }

        [Test]
        public void EntityMethods_GetResref_NoComponent_ShouldReturnEmptyString()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            string resref = _entityMethods.GetResref(entity);

            Assert.AreEqual(resref, string.Empty);
        }
    }
}
