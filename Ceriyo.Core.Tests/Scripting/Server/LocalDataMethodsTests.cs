using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting.Server;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting.Server
{
    public class LocalDataMethodsTests
    {
        private LocalDataMethods _localDataMethods;

        [SetUp]
        public void SetUp()
        {
            _localDataMethods = new LocalDataMethods();
        }

        private Entity BuildValidEntity()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            LocalData data = new LocalData();
            entity.AddComponent(data);
            return entity;
        }

        private Entity BuildInvalidEntity()
        {
            EntityWorld world = TestHelpers.CreateEntityWorld();
            Entity entity = world.CreateEntity();
            return entity;
        }
        [Test]
        public void SetLocalValue_ShouldEqual100Int()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Test", 100);
            int value = Convert.ToInt32(entity.GetComponent<LocalData>().LocalDoubles["Test"]);

            Assert.AreEqual(value, 100);
        }
        [Test]
        public void SetLocalValue_ShouldEqual50Float()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Test", 50.0f);
            double value = entity.GetComponent<LocalData>().LocalDoubles["Test"];

            Assert.AreEqual(value, 50.0f);
        }
        [Test]
        public void SetLocalValue_ShouldEqualString()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Test", "This is a sample message.");
            string value = entity.GetComponent<LocalData>().LocalStrings["Test"];
            Assert.AreEqual(value, "This is a sample message.");
        }

        [Test]
        public void GetLocalFloat_ShouldEqualValue()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Test", 543.32f);
            Assert.AreEqual(_localDataMethods.GetLocalNumber(entity, "Test"), 543.32f);
        }

        [Test]
        public void GetLocalString_ShouldEqualValue()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Test", "Setting a simple message.");
            Assert.AreEqual(_localDataMethods.GetLocalString(entity, "Test"), "Setting a simple message.");
        }

        [Test]
        public void SetLocalValue_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                _localDataMethods.SetLocalValue(entity, "TestVal1", 100.0f);
                _localDataMethods.SetLocalValue(entity, "TestVal2", 4444);
                _localDataMethods.SetLocalValue(entity, "TestVal3", "Sample string set here");
            });
        }

        [Test]
        public void GetLocalFloat_NoComponent_ShouldReturn0Point0()
        {
            Entity entity = BuildInvalidEntity();
            double value = _localDataMethods.GetLocalNumber(entity, "nocomponent");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void GetLocalString_NoComponent_ShouldReturnEmptyString()
        {
            Entity entity = BuildInvalidEntity();
            string value = _localDataMethods.GetLocalString(entity, "nocomponent");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void GetLocalFloat_NoKey_ShouldReturn0Point0()
        {
            Entity entity = BuildValidEntity();
            double value = _localDataMethods.GetLocalNumber(entity, "nokey");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void GetLocalString_NoKey_ShouldReturnEmptyString()
        {
            Entity entity = BuildValidEntity();
            string value = _localDataMethods.GetLocalString(entity, "nokey");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void SetLocalInt_GetLocalFloat_ShouldReturn123Point0()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "Key", 123);
            double result = _localDataMethods.GetLocalNumber(entity, "Key");
            Assert.AreEqual(result, 123.0f);
        }

        [Test]
        public void DeleteLocalVariables_ShouldNotThrowException()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "haskey", "this is a test string");
            _localDataMethods.SetLocalValue(entity, "haskey2", 55523.3f);

            Assert.DoesNotThrow(delegate
            {
                _localDataMethods.DeleteLocalNumber(entity, "nokey");
                _localDataMethods.DeleteLocalString(entity, "nokey2");

                _localDataMethods.DeleteLocalString(entity, "haskey");
                _localDataMethods.DeleteLocalNumber(entity, "haskey2");
            });
        }
        [Test]
        public void DeleteLocalString_ValueShouldBeRemoved()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "key", "test string");
            string value = _localDataMethods.GetLocalString(entity, "key");
            Assert.AreEqual(value, "test string");

            _localDataMethods.DeleteLocalString(entity, "key");
            value = _localDataMethods.GetLocalString(entity, "key");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void DeleteLocalNumber_ValueShouldBeRemoved()
        {
            Entity entity = BuildValidEntity();
            _localDataMethods.SetLocalValue(entity, "key", 2345);
            double value = _localDataMethods.GetLocalNumber(entity, "key");
            Assert.AreEqual(value, 2345);

            _localDataMethods.DeleteLocalNumber(entity, "key");
            value = _localDataMethods.GetLocalNumber(entity, "key");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void DeleteLocalString_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                _localDataMethods.DeleteLocalNumber(entity, "nokey1");
                _localDataMethods.DeleteLocalString(entity, "nokey2");
            });
        }
        [Test]
        public void DeleteLocalVariables_NoKey_ShouldNotThrowException()
        {
            Entity entity = BuildValidEntity();
            Assert.DoesNotThrow(delegate
            {
                _localDataMethods.DeleteLocalNumber(entity, "nokey1");
                _localDataMethods.DeleteLocalString(entity, "nokey2");
            });
        }
    }
}
