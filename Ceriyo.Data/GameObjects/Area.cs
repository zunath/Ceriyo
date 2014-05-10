﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data.Enumerations;

namespace Ceriyo.Data.GameObjects
{
    public class Area : IGameObject
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Resref { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public string TextureFilePath { get; private set; }
        public List<Tile[,]> MapTiles { get; private set; }
        public int LayerCount { get; private set; }
        public string WorkingDirectory { get { return WorkingPaths.AreasDirectory; } }
        public IList<LocalVariable> LocalVariables { get; set; }
        public IDictionary<ScriptEventTypeEnum, string> Scripts { get; set; }

        public Area(
            string name,
            string tag,
            string resref,
            string textureFilePath, 
            int tilesWide, 
            int tilesHigh, 
            int numberOfLayers)
        {
            this.Name = name;
            this.Tag = tag;
            this.Resref = resref;
            this.TextureFilePath = textureFilePath;
            this.MapWidth = tilesWide;
            this.MapHeight = tilesHigh;
            this.LayerCount = numberOfLayers;
            this.MapTiles = new List<Tile[,]>();

            for (int layer = 0; layer < LayerCount; layer++)
            {
                MapTiles.Add(new Tile[MapWidth, MapHeight]);

                for (int x = 0; x <= MapTiles[layer].GetUpperBound(0); x++)
                {
                    for (int y = 0; y < MapTiles[layer].GetUpperBound(1); y++)
                    {
                        MapTiles[layer][x, y] = new Tile();
                    }
                }
            }
        }
    }
}
