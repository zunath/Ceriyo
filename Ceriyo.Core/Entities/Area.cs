using System;
using Artemis;
using Ceriyo.Core.Components;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Data;
using Ceriyo.Core.Entities.Contracts;
using Ceriyo.Core.Services.Contracts;

namespace Ceriyo.Core.Entities
{
    public class Area: IGameEntity<AreaData>
    {
        private readonly IComponentFactory _factory;
        private readonly IModuleDataService _moduleDataService;
        private readonly IModuleResourceService _resourceService;

        public Area(IComponentFactory factory,
            IModuleDataService moduleDataService,
            IModuleResourceService resourceService)
        {
            _factory = factory;
            _moduleDataService = moduleDataService;
            _resourceService = resourceService;
        }

        public void BuildEntity(Entity entity, AreaData data) 
        {
            if(data == null)
                throw new ArgumentException($"{nameof(data)} cannot be null.");

            var name = _factory.Create<Nameable>();
            var tag = _factory.Create<Tag>();
            var resref = _factory.Create<Resref>();
            var description = _factory.Create<Description>();
            var scriptGroup = _factory.Create<ScriptGroup>();
            var localData = _factory.Create<LocalData>();
            var renderable = _factory.Create<Renderable>();
            var map = _factory.Create<Map>();

            name.Value = data.Name;
            tag.Value = data.Tag;
            resref.Value = data.Resref;
            description.Value = data.Description;

            scriptGroup.Add(ScriptEvent.OnAreaEnter, data.OnAreaEnter);
            scriptGroup.Add(ScriptEvent.OnAreaExit, data.OnAreaExit);
            scriptGroup.Add(ScriptEvent.OnHeartbeat, data.OnAreaHeartbeat);
            
            foreach (var @string in data.LocalVariables.LocalStrings)
            {
                localData.LocalStrings.Add(@string.Key, @string.Value);
            }

            foreach (var @double in data.LocalVariables.LocalDoubles)
            {
                localData.LocalDoubles.Add(@double.Key, @double.Value);
            }

            TilesetData tileset = _moduleDataService.Load<TilesetData>(data.TilesetGlobalID);
            renderable.Texture = _resourceService.LoadTexture2D(ResourceType.Tileset, tileset.ResourceName);

            map.Width = data.Width;
            map.Height = data.Height;
            map.Tiles = new Tile[data.Width, data.Height];

            for (int x = 0; x < map.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < map.Tiles.GetLength(1); y++)
                {
                    TileData tileData = data.TileAtlas.GetTile(x, y);
                    if (tileData != null)
                    {
                        map.Tiles[x, y] = new Tile
                        {
                            SourceX = tileData.SourceX,
                            SourceY = tileData.SourceY
                        };
                    }
                }
            }

            entity.AddComponent(name);
            entity.AddComponent(tag);
            entity.AddComponent(resref);
            entity.AddComponent(description);
            entity.AddComponent(scriptGroup);
            entity.AddComponent(localData);
            entity.AddComponent(renderable);
            entity.AddComponent(map);
        }
    }
}
