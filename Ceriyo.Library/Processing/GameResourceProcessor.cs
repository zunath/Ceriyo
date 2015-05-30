using Ceriyo.Data.GameObjects;
using System.Collections.Generic;
using System.Linq;

namespace Ceriyo.Library.Processing
{
    public static class GameResourceProcessor
    {
        public static string GenerateUniqueResref(IList<IGameObject> gameObjectList, string defaultCategoryName = "")
        {
            int count = 0;
            string resref = defaultCategoryName + count;

            if (gameObjectList.Count > 0)
            {
                resref = gameObjectList[0].CategoryName;

                while (gameObjectList.FirstOrDefault(x => x.Resref == resref + count) != null)
                {
                    count++;
                }

                resref = resref + count;
            }

            return resref;
        }
    }
}
