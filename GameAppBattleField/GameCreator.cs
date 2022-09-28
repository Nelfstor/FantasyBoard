using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppBattleField
{
    internal class GameCreator
    {        
        public static Drawer drawer;
        public static Field field;
        Random random;
        List<GameObject> allGameObjects;
        public ObjectArchitype? Victory { get;private set; }
        public static List<GameObject> KilledObjects { get; set; }

        //List<Cell> updateCells;
        public GameCreator(Graphics gr, int panelHeight, int panelWidth, int columnsNumber, int rowNumber)
        {
            //создаём поле
            field = new Field(panelHeight,panelWidth,columnsNumber,rowNumber);
            
            //создаёт рисовальщика
            drawer = new Drawer(gr,field);

            allGameObjects = new List<GameObject>();
            KilledObjects = new List<GameObject>();

            CreateGameObjects();

            //При первом вызове подаём все объекты на отрисовку
            foreach (GameObject gameObject in allGameObjects)
            {
                drawer.AddListGameObject4Update(allGameObjects);
            }
            
            drawer.DrawAllObjects();
            Victory = null;
        }

        public void CreateGameObjects()
        {
            random = new Random();
            // HumanFactory archerFactory = new ArcherFactory(ObjectArchitype.Human);
            CreateHumanObjects(2, 4);
            CreateMonstrObjects(1, 3);
            CreateBuildings(3, 6);
        }
        void CreateBuildings(int minCount, int maxCount)
        {
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Tree());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Seno());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Stone());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Obelisk());
            }

        }
        void CreateMonstrObjects(int minCount, int maxCount)
        {
            // создаёт Monstr объекты;
            for (int i = 1; i <= random.Next(minCount, maxCount+1); i++)
            {
                allGameObjects.Add(new Orc());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount+1); i++)
            {
                allGameObjects.Add(new Harpy());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount+1); i++)
            {
                allGameObjects.Add(new Golem());
            }
            allGameObjects.Add(new Dragon());
        }

        void CreateHumanObjects(int minCount,int maxCount)
        {
            // создаёт Human объекты;
            // циклы дублируются, чтобы был разный рандом
            for (int i = 1; i <= random.Next(minCount, maxCount+1); i++)
            {
                allGameObjects.Add(new Archer());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Swordsman());
            }
            for (int i = 1; i <= random.Next(minCount, maxCount + 1); i++)
            {
                allGameObjects.Add(new Mage());
            }
        }
        
        public void Step()
        {
            foreach(var gameObject in allGameObjects)
            {
                if (!KilledObjects.Contains(gameObject))
                    gameObject.MakeTurn(field);

                drawer.UpdateAll();
            }

            allGameObjects.RemoveAll(x => KilledObjects.Contains(x));
                            
            drawer.UpdateAll();

            if (allGameObjects.All(x => (x.GetStaticArchitype() == ObjectArchitype.Monstr) ||
            (x.GetStaticArchitype() == ObjectArchitype.Building)))
                Victory = ObjectArchitype.Monstr;

            else if (allGameObjects.All(x => (x.GetStaticArchitype() == ObjectArchitype.Human) ||
            (x.GetStaticArchitype() == ObjectArchitype.Building)))
                Victory = ObjectArchitype.Human;
        } 

    }
}
