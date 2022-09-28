using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppBattleField
{

    internal abstract class GameObjectsFactory
    {
        static Random random;
        static Field field;
        ObjectArchitype architype;
        public GameObjectsFactory(ObjectArchitype architype)
        {
            this.architype = architype;
        }
        public abstract GameObject CreateGameObject();
            

    }
    internal class HumanFactory : GameObjectsFactory
    {
        public HumanFactory(ObjectArchitype architype) : base(architype)
        { }
        public override GameObject CreateGameObject()
        {
            return new Human();
        }

    }
    internal class ArcherFactory: HumanFactory
    {
        public ArcherFactory(ObjectArchitype architype) : base(architype)
        { 
        }
        public override GameObject CreateGameObject()
        {
            return new Archer();
        }
    }
    internal class SwordsmanFactory : HumanFactory
    {
        public SwordsmanFactory(ObjectArchitype architype) : base(architype)
        {
        }
        public override GameObject CreateGameObject()
        {
            return new Swordsman();
        }
    }
    internal class MageFactory : HumanFactory
    {
        public MageFactory(ObjectArchitype architype) : base(architype)
        {
        }
        public override GameObject CreateGameObject()
        {
            return new Mage();
        }
    }




}
