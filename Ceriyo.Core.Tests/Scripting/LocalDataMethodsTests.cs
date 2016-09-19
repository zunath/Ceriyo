using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Scripting;
using Ceriyo.Testing.Shared;
using NUnit.Framework;

namespace Ceriyo.Core.Tests.Scripting
{
    [TestFixture]
    public class LocalDataMethodsTests
    {
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
            LocalDataMethods.SetLocalValue(entity, "Test", 100);
            int value = Convert.ToInt32(entity.GetComponent<LocalData>().LocalFloats["Test"]);

            Assert.AreEqual(value, 100);
        }
        [Test]
        public void SetLocalValue_ShouldEqual50Float()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "Test", 50.0f);
            float value = entity.GetComponent<LocalData>().LocalFloats["Test"];

            Assert.AreEqual(value, 50.0f);
        }
        [Test]
        public void SetLocalValue_ShouldEqualString()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "Test", "This is a sample message.");
            string value = entity.GetComponent<LocalData>().LocalStrings["Test"];
            Assert.AreEqual(value, "This is a sample message.");
        }

        [Test]
        public void GetLocalFloat_ShouldEqualValue()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "Test", 543.32f);
            Assert.AreEqual(LocalDataMethods.GetLocalNumber(entity, "Test"), 543.32f);
        }

        [Test]
        public void GetLocalString_ShouldEqualValue()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "Test", "Setting a simple message.");
            Assert.AreEqual(LocalDataMethods.GetLocalString(entity, "Test"), "Setting a simple message.");
        }

        [Test]
        public void SetLocalValue_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                LocalDataMethods.SetLocalValue(entity, "TestVal1", 100.0f);
                LocalDataMethods.SetLocalValue(entity, "TestVal2", 4444);
                LocalDataMethods.SetLocalValue(entity, "TestVal3", "Sample string set here");
            });
        }

        [Test]
        public void GetLocalFloat_NoComponent_ShouldReturn0Point0()
        {
            Entity entity = BuildInvalidEntity();
            float value = LocalDataMethods.GetLocalNumber(entity, "nocomponent");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void GetLocalString_NoComponent_ShouldReturnEmptyString()
        {
            Entity entity = BuildInvalidEntity();
            string value = LocalDataMethods.GetLocalString(entity, "nocomponent");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void GetLocalFloat_NoKey_ShouldReturn0Point0()
        {
            Entity entity = BuildValidEntity();
            float value = LocalDataMethods.GetLocalNumber(entity, "nokey");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void GetLocalString_NoKey_ShouldReturnEmptyString()
        {
            Entity entity = BuildValidEntity();
            string value = LocalDataMethods.GetLocalString(entity, "nokey");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void SetLocalInt_GetLocalFloat_ShouldReturn123Point0()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "Key", 123);
            float result = LocalDataMethods.GetLocalNumber(entity, "Key");
            Assert.AreEqual(result, 123.0f);
        }

        [Test]
        public void DeleteLocalVariables_ShouldNotThrowException()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "haskey", "this is a test string");
            LocalDataMethods.SetLocalValue(entity, "haskey2", 55523.3f);

            Assert.DoesNotThrow(delegate
            {
                LocalDataMethods.DeleteLocalNumber(entity, "nokey");
                LocalDataMethods.DeleteLocalString(entity, "nokey2");

                LocalDataMethods.DeleteLocalString(entity, "haskey");
                LocalDataMethods.DeleteLocalNumber(entity, "haskey2");
            });
        }
        [Test]
        public void DeleteLocalString_ValueShouldBeRemoved()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "key", "test string");
            string value = LocalDataMethods.GetLocalString(entity, "key");
            Assert.AreEqual(value, "test string");

            LocalDataMethods.DeleteLocalString(entity, "key");
            value = LocalDataMethods.GetLocalString(entity, "key");
            Assert.AreEqual(value, string.Empty);
        }

        [Test]
        public void DeleteLocalNumber_ValueShouldBeRemoved()
        {
            Entity entity = BuildValidEntity();
            LocalDataMethods.SetLocalValue(entity, "key", 2345);
            float value = LocalDataMethods.GetLocalNumber(entity, "key");
            Assert.AreEqual(value, 2345);

            LocalDataMethods.DeleteLocalNumber(entity, "key");
            value = LocalDataMethods.GetLocalNumber(entity, "key");
            Assert.AreEqual(value, 0.0f);
        }
        [Test]
        public void DeleteLocalString_NoComponent_ShouldNotThrowException()
        {
            Entity entity = BuildInvalidEntity();
            Assert.DoesNotThrow(delegate
            {
                LocalDataMethods.DeleteLocalNumber(entity, "nokey1");
                LocalDataMethods.DeleteLocalString(entity, "nokey2");
            });
        }
        [Test]
        public void DeleteLocalVariables_NoKey_ShouldNotThrowException()
        {
            Entity entity = BuildValidEntity();
            Assert.DoesNotThrow(delegate
            {
                LocalDataMethods.DeleteLocalNumber(entity, "nokey1");
                LocalDataMethods.DeleteLocalString(entity, "nokey2");
            });
        }
    }
}
