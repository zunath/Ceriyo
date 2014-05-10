#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.CustomDrawableBatches;
using Noesis.Javascript;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Ceriyo.Library.ScriptEngine;

#endif
#endregion

namespace Ceriyo.Entities
{
	public partial class AreaEntity
	{
        MapDrawableBatch MapBatch;

		private void CustomInitialize()
		{
            Map map = new Map("Tilesets/grassland_tiles", 4, 7, 3);
            MapBatch = new MapDrawableBatch(map);

            Player player = new Player();
            player.Name = "newname";
            player.Tag = "newtag";
            player.Resref = "newresref";

            ScriptManager manager = new ScriptManager();
            object test = manager.RunScript("testscript", player);



		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{
            MapBatch.Destroy();

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
