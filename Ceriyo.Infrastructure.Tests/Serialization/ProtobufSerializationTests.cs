﻿using System.IO;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Infrastructure.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Tests.Serialization
{
    public class ProtobufSerializationTests
    {
        private Mock<ILogger> _mockLogger;
        private IDataService _dataService;
        
        [OneTimeSetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger>();
            _dataService = new DataService(_mockLogger.Object);
            _dataService.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            
        }


        [Test]
        public void SerializerConfig_AbilityData_ShouldBeEqual()
        {
            AbilityData data = new AbilityData
            {
                Comment = "it's a comment",
                Description = "it's a description",
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag",
                IsPassive = true,
                OnActivated = "on activated script"
            };
            AbilityData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<AbilityData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }

        [Test]
        public void SerializerConfig_AnimationData_ShouldBeEqual()
        {
            AnimationData data = new AnimationData
            {
                Comment = "it's a comment",
                Description = "it's a description",
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            AnimationData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<AnimationData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_ClassData_ShouldBeEqual()
        {
            ClassData data = new ClassData
            {
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            ClassData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ClassData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_ClassRequirementData_ShouldBeEqual()
        {
            ClassRequirementData data = new ClassRequirementData()
            {
                ClassResref = "parentresref",
                LevelRequired = 35
            };
            ClassRequirementData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ClassRequirementData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_CreatureData_ShouldBeEqual()
        {
            CreatureData data = new CreatureData()
            {
                Comment = "it's a comment",
                Description = "it's a description",
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag",
                ClassResref = "classresref",
                DialogResref = "dialogresref",
                Level = 34,
                OnAttacked = "the onattacked script",
                OnConversation = "the onconversation script",
                OnDamaged = "the ondamaged script",
                OnDeath = "the ondeath script",
                OnDisturbed = "the ondisturbed script",
                OnHeartbeat = "the onheartbeat script",
                OnSpawned = "the onspawned script"
            };
            CreatureData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<CreatureData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_DialogData_ShouldBeEqual()
        {
            DialogData data = new DialogData
            {
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            DialogData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<DialogData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_FrameData_ShouldBeEqual()
        {
            FrameData data = new FrameData
            {
                Name = "it's a name",
                FlipHorizontal = true,
                FlipVertical = true,
                FrameLength = 50.04f,
                TextureCellX = 5,
                TextureCellY = 2
            };
            FrameData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<FrameData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_ItemData_ShouldBeEqual()
        {
            ItemData data = new ItemData()
            {
                Comment = "it's a comment",
                Description = "it's a description",
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag",
                OnActivated = "on activated script",
                IsPlot = true,
                IsStolen = true,
                IsUndroppable = true,
                ItemTypeResref = "itemtyperesref",
                OnAcquired = "onacquired script",
                OnEquipped = "onequipped script",
                OnUnacquired = "onunacquired script",
                OnUnequipped = "onunequipped script"
            };
            ItemData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ItemData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_ItemPropertyData_ShouldBeEqual()
        {
            ItemPropertyData data = new ItemPropertyData()
            {
                Comment = "it's a comment",
                Description = "it's a description",
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            ItemPropertyData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ItemPropertyData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }



        [Test]
        public void SerializerConfig_ItemTypeData_ShouldBeEqual()
        {
            ItemTypeData data = new ItemTypeData()
            {
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            ItemTypeData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ItemTypeData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }



        [Test]
        public void SerializerConfig_LocalVariableData_ShouldBeEqual()
        {
            LocalVariableData data = new LocalVariableData();
            data.LocalStrings.Add("stringkey1", "stringval1");
            data.LocalStrings.Add("stringkey2", "stringval2");
            data.LocalStrings.Add("stringkey3", "stringval3");

            data.LocalFloats.Add("floatkey1", 1.23f);
            data.LocalFloats.Add("floatkey2", 34.3f);
            data.LocalFloats.Add("floatkey3", 55.24235f);

            LocalVariableData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<LocalVariableData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }
        [Test]
        public void SerializerConfig_ModuleData_ShouldBeEqual()
        {
            ModuleData data = new ModuleData();
            data.AbilityIDs.Add("ability1");
            data.AbilityIDs.Add("ability2");
            data.AbilityIDs.Add("ability3");

            data.ClassIDs.Add("class1");
            data.ClassIDs.Add("class2");
            data.ClassIDs.Add("class3");

            data.CreatureIDs.Add("creature1");
            data.CreatureIDs.Add("creature2");
            data.CreatureIDs.Add("creature3");

            data.ItemIDs.Add("item1");
            data.ItemIDs.Add("item2");
            data.ItemIDs.Add("item3");

            data.ItemPropertyIDs.Add("itemproperty1");
            data.ItemPropertyIDs.Add("itemproperty2");
            data.ItemPropertyIDs.Add("itemproperty3");

            data.PlaceableIDs.Add("placeable1");
            data.PlaceableIDs.Add("placeable2");
            data.PlaceableIDs.Add("placeable3");

            data.ScriptIDs.Add("script1");
            data.ScriptIDs.Add("script2");
            data.ScriptIDs.Add("script3");

            data.SkillIDs.Add("skill1");
            data.SkillIDs.Add("skill2");
            data.SkillIDs.Add("skill3");

            data.TilesetIDs.Add("tileset1");
            data.TilesetIDs.Add("tileset2");
            data.TilesetIDs.Add("tileset3");

            ModuleData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ModuleData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }


        [Test]
        public void SerializerConfig_ModuleFileData_ShouldBeEqual()
        {
            ModuleFileData data = new ModuleFileData()
            {
                FileName = "new file name",
                Data = new byte[] {123,232,234,2,92}
            };
            ModuleFileData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ModuleFileData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }

        [Test]
        public void SerializerConfig_PlaceableFileData_ShouldBeEqual()
        {
            PlaceableData data = new PlaceableData()
            {
                AutoRemoveKey = true,
                Comment = "it's a comment",
                Description = "it's a description",
                IsKeyRequired = true,
                IsLocked = true,
                IsPlot = true,
                IsStatic = true,
                IsUseable = true,
                KeyTag = "key tag 1",
                Name = "it's a name",
                OnAttacked = "onattacked script",
                OnClosed = "onclosed script",
                OnDamaged = "ondamaged script",
                OnDeath = "ondeath script",
                OnDisturbed = "ondisturbed script",
                OnHeartbeat = "onheartbeat script",
                OnLocked = "onlocked script",
                OnOpened = "onopened script",
                OnUnlocked = "onunlocked script",
                OnUsed = "onused script",
                Resref = "it's a resref",
                Tag = "it's a tag"
            };
            PlaceableData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<PlaceableData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }
        [Test]
        public void SerializerConfig_ScriptData_ShouldBeEqual()
        {
            ScriptData data = new ScriptData()
            {
                FileName = "new file name",
                Resref = "it's a resref",
                Name = "it's a name"
            };
            ScriptData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<ScriptData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }

        [Test]
        public void SerializerConfig_SkillData_ShouldBeEqual()
        {
            SkillData data = new SkillData()
            {
                Name = "it's a name",
                Resref = "it's a resref",
                Tag = "it's a tag",
                Comment = "it's a comment",
                Description = "it's a description",
                IsPassive = true,
                OnActivated = "onactivated script"
            };
            SkillData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<SkillData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }

        [Test]
        public void SerializerConfig_TilesetData_ShouldBeEqual()
        {
            TilesetData data = new TilesetData()
            {
                Name = "it's a name",
                Resref = "it's a resref",
                Comment = "it's a comment",
                Description = "it's a description",
                Tag = "it's a tag"
            };
            TilesetData result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, data);
                stream.Position = 0;

                result = Serializer.Deserialize<TilesetData>(stream);
            }

            var dataJson = JsonConvert.SerializeObject(data);
            var resultJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(dataJson, resultJson);
        }
    }
}
