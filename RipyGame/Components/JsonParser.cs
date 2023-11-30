using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Models;
using Microsoft.Xna.Framework;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Components
{
    public class JsonParser
    {
        static JObject wholeObj;

        public static void LoadObjectFromFile(string path)
        {
            if (File.Exists(path))
            {
                StreamReader file = File.OpenText(path);
                JsonTextReader reader = new JsonTextReader(file);
                wholeObj = JObject.Load(reader);
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        public static Rectangle GetRectangle(JObject obj)
        {
            int x = Convert.ToInt32(obj.GetValue("positionX"));
            int y = Convert.ToInt32(obj.GetValue("positionY"));
            int width = Convert.ToInt32(obj.GetValue("width"));
            int height = Convert.ToInt32(obj.GetValue("height"));

            Rectangle rect = new Rectangle(x, y, width, height);
            return rect;
        }

        public static Rectangle GetRectangle(string path, string propertyName)
        {
            LoadObjectFromFile(path);

            JObject obj = (JObject)wholeObj.GetValue(propertyName);

            return GetRectangle(obj);
        }

        public static List<Rectangle> GetRectangleList(string path, string propertyName)
        {
            LoadObjectFromFile(path);
            List<Rectangle> rectList = new List<Rectangle>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);
            foreach (JObject obj in arrayObj)
            {
                rectList.Add(GetRectangle(obj));
            }

            return rectList;
        }

        public static PlattformTypes GetPlattformType(JObject obj)
        {
            string pType = Convert.ToString(obj.GetValue("plattformType"));
            PlattformTypes plattformType = PlattformTypes.Solid;
            switch (pType)
            {
                case "solid":
                    plattformType = PlattformTypes.Solid;
                    break;
                case "interactable":
                    plattformType = PlattformTypes.Interactable;
                    break;
                default:
                    plattformType = PlattformTypes.Solid;
                    break;
            }
            return plattformType;
        }

        public static List<PlattformTypes> GetPlattformTypeList(string path)
        {
            string propertyName = "platforms";
            LoadObjectFromFile(path);
            List<PlattformTypes> plattformTypeList = new List<PlattformTypes>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);
            foreach (JObject obj in arrayObj)
            {
                plattformTypeList.Add(GetPlattformType(obj));
            }

            return plattformTypeList;
        }

        public static PlattformAttributes GetPlattformAttribute(JObject obj)
        {
            string pAttribute = Convert.ToString(obj.GetValue("plattformAttribute"));
            PlattformAttributes plattformAttribute = PlattformAttributes.None;
            switch (pAttribute)
            {
                case "none":
                    plattformAttribute = PlattformAttributes.None;
                    break;
                case "dangerous":
                    plattformAttribute = PlattformAttributes.Dangerous;
                    break;
                default:
                    plattformAttribute= PlattformAttributes.None;
                    break;
            }
            return plattformAttribute;
        }

        public static List<PlattformAttributes> GetPlattformAttributeList(string path)
        {
            string propertyName = "platforms";
            LoadObjectFromFile(path);
            List<PlattformAttributes> plattformAttributeList = new List<PlattformAttributes>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);
            foreach (JObject obj in arrayObj)
            {
                plattformAttributeList.Add(GetPlattformAttribute(obj));
            }

            return plattformAttributeList;
        }

        public static EnemyTypes GetEnemyType(JObject obj)
        {
            string eType = Convert.ToString(obj.GetValue("enemyType"));
            EnemyTypes enemyType = EnemyTypes.CrystalGuardian;
            switch (eType)
            {
                case "crystalGuardian":
                    enemyType = EnemyTypes.CrystalGuardian;
                    break;
                case "frostWraith":
                    enemyType = EnemyTypes.FrostWraith;
                    break;
                default:
                    enemyType = EnemyTypes.CrystalGuardian;
                    break;
            }
            return enemyType;
        }

        public static List<EnemyTypes> GetEnemyTypeList(string path)
        {
            string propertyName = "enemies";
            LoadObjectFromFile(path);
            List<EnemyTypes> enemyTypeList = new List<EnemyTypes>();
            JArray arrayObj = (JArray)wholeObj.GetValue(propertyName);
            foreach (JObject obj in arrayObj)
            {
                enemyTypeList.Add(GetEnemyType(obj));
            }

            return enemyTypeList;
        }

        public static void WriteJsonToFile(string path)
        {
            JArray playerArray = new JArray();
            JArray platformArray = new JArray();
            JArray enemyArray = new JArray();
            JObject bigObj = new JObject();

            foreach (Player playerObj in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                JObject curObj = CreatePlayerObject(playerObj.PlayerIndex, playerObj.Bounds);
                playerArray.Add(curObj);
            }
            foreach (Plattform plattformObj in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
            {
                JObject curObj = CreatePlattformObject(plattformObj.PlattformType, plattformObj.PlattformAttribute, plattformObj.Bounds);
                platformArray.Add(curObj);
            }
            foreach (Enemy enemyObj in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.Enemy))
            {
                JObject curObj = CreateEnemyObject(enemyObj.EnemyType, enemyObj.Bounds, enemyObj.velocity);
                enemyArray.Add(curObj);
            }

            bigObj.Add("players", playerArray);
            bigObj.Add("platforms", platformArray);
            bigObj.Add("enemies", enemyArray);

            File.WriteAllText(path, bigObj.ToString());
        }

        private static JObject CreatePlayerObject(PlayerIndex playerIndex, Rectangle bounds)
        {
            int pIndex = 0;
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    pIndex = 1;
                    break;
                case PlayerIndex.Two:
                    pIndex = 2;
                    break;
                case PlayerIndex.Three:
                    pIndex = 3;
                    break;
                case PlayerIndex.Four:
                    pIndex = 4;
                    break;
                default:
                    break;
            }
            JObject obj = new JObject();
            obj.Add("playerIndex", pIndex);
            obj.Add("positionX", bounds.X);
            obj.Add("positionY", bounds.Y);
            obj.Add("width", bounds.Width);
            obj.Add("height", bounds.Height);

            return obj;
        }

        private static JObject CreatePlattformObject(PlattformTypes plattformType, PlattformAttributes plattformAttribute, Rectangle bounds)
        {
            string pType = "";
            switch (plattformType)
            {
                case PlattformTypes.Solid:
                    pType = "solid";
                    break;
                case PlattformTypes.Interactable:
                    pType = "interactable";
                    break;
                default:
                    pType = "none";
                    break;
            }

            string pAttribute = "";
            switch (plattformAttribute)
            {
                case PlattformAttributes.None:
                    pAttribute = "none";
                    break;
                case PlattformAttributes.Passage:
                    pAttribute = "passage";
                    break;
                case PlattformAttributes.Portal:
                    pAttribute = "portal";
                    break;
                case PlattformAttributes.Breakable:
                    pAttribute = "breakable";
                    break;
                case PlattformAttributes.Dangerous:
                    pAttribute = "dangerous";
                    break;
                case PlattformAttributes.ItemSpawn:
                    pAttribute = "itemSpawn";
                    break;
                default:
                    break;
            }

            JObject obj = new JObject();
            obj.Add("plattformType", pType);
            obj.Add("plattformAttribute", pAttribute);
            obj.Add("positionX", bounds.X);
            obj.Add("positionY", bounds.Y);
            obj.Add("width", bounds.Width);
            obj.Add("height", bounds.Height);

            return obj;
        }
        
        private static JObject CreateEnemyObject(EnemyTypes enemyType, Rectangle bounds, Vector2 velocity)
        {
            string eType = "";
            switch (enemyType)
            {
                case EnemyTypes.CrystalGuardian:
                    eType = "crystalGuardian";
                    break;
                case EnemyTypes.FrostWraith:
                    eType = "frostWraith";
                    break;
                case EnemyTypes.ShadowPhantom:
                    eType = "shadowPhantom";
                    break;
                default:
                    eType = "none";
                    break;
            }


            JObject obj = new JObject();
            obj.Add("enemyType", eType);
            obj.Add("positionX", bounds.X);
            obj.Add("positionY", bounds.Y);
            obj.Add("width", bounds.Width);
            obj.Add("height", bounds.Height);
            obj.Add("velocityX", velocity.X);
            obj.Add("velocityY", velocity.Y);

            return obj;
        }
    }
}
