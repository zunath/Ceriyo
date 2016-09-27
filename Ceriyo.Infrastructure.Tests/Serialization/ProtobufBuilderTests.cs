using System;
using System.Collections.Generic;
using Ceriyo.Infrastructure.Serialization;
using NUnit.Framework;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Tests.Serialization
{
    
    public class ProtobufBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [TearDown]
        public void TearDown()
        {
            
        }
        private static List<SimpleClass> GenerateSimpleClassList()
        {
            var list = new List<SimpleClass>();

            for (int i = 0; i < 200; i++)
            {
                list.Add(new SimpleClass
                {
                    Id = i,
                    Name = $"Test class {i}"
                });
            }
            return list;
        }

        [Test]
        public void SerializeSimpleClass()
        {
            var simpleClass = new SimpleClass
            {
                Id = 1,
                Name = "Test1"
            };

            ProtobufBuilder.Build(simpleClass);

            var result = Serializer.DeepClone(simpleClass);

            Assert.AreEqual(simpleClass.Id, result.Id);
            Assert.AreEqual(simpleClass.Name, result.Name);
        }

        [Test]
        public void SerializeSimpleClassList()
        {
            var list = GenerateSimpleClassList();

            ProtobufBuilder.Build(list);

            var result = Serializer.DeepClone(list);

            Assert.AreEqual(list[100].Id, result[100].Id);
            Assert.AreEqual(list[100].Name, result[100].Name);
        }

        [Test]
        public void SerializeComplexClass()
        {
            var complexClass = new ComplexClass<SimpleClass>(Guid.NewGuid())
            {
                Values = GenerateSimpleClassList()
            };

            ProtobufBuilder.Build(complexClass);

            var result = Serializer.DeepClone(complexClass);

            Assert.AreEqual(complexClass.Key, result.Key);
            Assert.AreEqual(complexClass.Values[100].Id, result.Values[100].Id);
            Assert.AreEqual(complexClass.Values[100].Name, result.Values[100].Name);
        }

        private class ComplexClass<T>
        {
            public ComplexClass(Guid key)
            {
                Key = key;
            }

            public Guid Key { get; }
            public List<T> Values { get; set; }
        }

        private class SimpleClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
