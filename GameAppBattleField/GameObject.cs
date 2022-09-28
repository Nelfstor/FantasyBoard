using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppBattleField
{
    enum DamageType
    {
        Flying,
        EarthBound, 
        Magic
    }
     enum ObjectArchitype
    {
        Monstr,
        Human,
        Building
    }

    enum Directions
    {
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }

    abstract internal class GameObject 
    {
        public static ObjectArchitype staticArchitype;
        public string SubTypeName { get; internal set; }
        protected Cell cell;
        internal Cell Cell { get => cell; }
        protected static Random random;
        protected bool killed;
        public bool Killed { get =>killed;}
        protected int hP;
        internal int HP { get => hP; }
        protected int maxHP;
        internal int MaxHP { get => maxHP; }
        protected int defence_rate;
        protected int minDamage;
        protected int maxDamage;
        protected int damageRad; //радиус   
        protected bool multiTarget;
        protected List<DamageType> targetDamageType;
        public List<DamageType> TargetDamageType { get => targetDamageType; }
        protected List<DamageType> receiveDamageType;
        public List<DamageType> ReceiveDamageType { get => receiveDamageType; }

        static GameObject()
        {
            random = new Random();
        }
        public void TakeDamage(int damage,DamageType damageType)
        {
            hP -= damage - damage*defence_rate/100;
            if (hP <= 0)
            {
                killed = true;
                hP = 0;
                cell.FreeCell();
                GameCreator.drawer.AddGameObject4Update(this);
                GameCreator.KilledObjects.Add(this);
            }
        }
        public GameObject()
        {
            cell = GameCreator.field.GetFreeCell();
            cell.SetObjectOnCell(this);
            targetDamageType = new List<DamageType>(3);
            receiveDamageType = new List<DamageType>(3);
        }
        public void MakeTurn(Field field)
        {
            if (GetStaticArchitype() != ObjectArchitype.Building)
            {
                if (!AttackObjectsInRange(field))
                    Move(field);
            }
            else if (this.GetType().Name == "Obelisk")
                AttackObjectsInRange(field);

            GameCreator.drawer.AddGameObject4Update(this);            
        }
        public virtual ObjectArchitype GetStaticArchitype()
        {
            return staticArchitype;
        }
        internal void Move(Field field)
        {
            List<Directions> tryied = new List<Directions>(4);
            bool success = false;
            Directions direction;
            while ((!success) && (tryied.Count < 4))
            {
                //случайное направление
                direction = (Directions)random.Next(1, 5);
                switch (direction)
                {
                    case Directions.North:
                        if (cell.Y > 0 && field.Cells[cell.X, cell.Y - 1].IsFree)
                        {
                            MoveToCell(field.Cells[cell.X, cell.Y - 1]);
                            success = true;
                        }
                        break;
                    case Directions.East:
                        if (cell.X < field.ColumnsNumber - 1 && field.Cells[cell.X + 1, cell.Y].IsFree)
                        {
                            MoveToCell(field.Cells[cell.X + 1, cell.Y]);
                            success = true;
                        }
                        break;
                    case Directions.South:
                        if (cell.Y < field.RowsNumber - 1 && field.Cells[cell.X, cell.Y + 1].IsFree)
                        {
                            MoveToCell(field.Cells[cell.X, cell.Y + 1]);
                            success = true;
                        }
                        break;
                    case Directions.West:
                        if (cell.X > 0 && field.Cells[cell.X - 1, cell.Y].IsFree)
                        {
                            this.MoveToCell(field.Cells[cell.X - 1, cell.Y]);
                            success = true;
                        }
                        break;
                    default:
                        {                           
                        }
                        break;
                }
                //Пополняем список проверенных направлений
                if (!tryied.Contains((Directions)direction))
                    tryied.Add((Directions)direction);
            }
        }
        /// <summary>
        /// Объект освобождает клетку затем занимает новую, которая проверена
        /// </summary>
        /// <param name="targetCell"></param>
        internal void MoveToCell(Cell targetCell)
        {
            cell.FreeCell();
            targetCell.SetObjectOnCell(this);
            this.cell = targetCell;
        }

        /// <summary>
        /// Ищут и атакует объекты доступных для атаки
        /// Если их нет - будут гулять ) 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        internal bool AttackObjectsInRange(Field field)
        {
            List<GameObject> result = new List<GameObject>();

            // определение радиуса поражения
            // можно было идти другим путём и перебирать по всем объектам для атаки, но при 
            // большом поле это не эффективно
            int leftBoundary = cell.X - damageRad >= 0 ? cell.X - damageRad : 0;
            int rightBoundary = (cell.X + damageRad <= field.ColumnsNumber) ? cell.X + damageRad : field.ColumnsNumber;
            //upperBoundary находится сверху, но ось направлена вних (чем ниже - тем выше значение).
            int upperBoundary = cell.Y - damageRad >= 0 ? cell.Y - damageRad : 0;
            int lowerBoundary = (cell.Y + damageRad <= field.RowsNumber) ? cell.Y + damageRad : field.RowsNumber;

            for (int i = leftBoundary; i < rightBoundary; i++)
                for (int j = upperBoundary; j < lowerBoundary; j++)
                {
                    double distance = Math.Sqrt(Math.Pow(cell.X - i, 2) + Math.Pow(cell.Y - j, 2));
                    if (distance <= damageRad)
                    {
                        // свободна ли клетка
                        if (!field.Cells[i, j].IsFree) 
                        {
                            GameObject targetObject = field.Cells[i, j].GameObjectOnCell;
                                                        
                            // FriendlyFire Off
                            if ( targetObject.GetStaticArchitype() != this.GetStaticArchitype())
                            {
                                // Если у объекта-цели в списке есть тот тип принимаемого урона, который есть 
                                // у текущего объекта - то он бъёт
                                if (targetObject.ReceiveDamageType.Any(x => this.TargetDamageType.Contains(x)))
                                { 
                                    targetObject.TakeDamage(random.Next(minDamage, maxDamage),
                                        targetObject.ReceiveDamageType.Find(x => TargetDamageType.Contains(x)));
                                    // если юнит может атаковать только одного юнита, то на этом мы заканчиваем его атаку
                                    // если нет - сканирует далее.
                                    if (!multiTarget) return true;

                                }                                    
                            }
                        }
                    }
                }
            return false;
        }
    }
    internal class Creature : GameObject
    {
        public Creature(): base()
        {

        }
    }
    internal class Human : Creature
    {
        internal static new ObjectArchitype staticArchitype;
        internal  HumanType humanType;
        public enum HumanType
        {
            Archer,
            Swordsman,
            Mage
        }
        public override ObjectArchitype GetStaticArchitype()
        {
            return staticArchitype;
        }
        public Human() : base()
        {
            //Architype = ObjectArchitype.Human; // По сути это должно быть статическое поле
                                               // Но в таком случае возникает сложность 
                                               // универсальности кода в проверках типа объекта
                                               // GetType не помог.
                                               // StackOverFlow и прочие - не помогли. 
            staticArchitype = ObjectArchitype.Human;
        }
    }
    internal class Swordsman : Human
    {

        // что такое , 20-30 хп (2-5) 
        public Swordsman() : base()
        {
            //Architype = ObjectArchitype.Human;
            minDamage = 10;
            maxDamage = 15;
            damageRad = 1;

            humanType = HumanType.Swordsman;
            SubTypeName = humanType.ToString();

            targetDamageType.Add(DamageType.EarthBound);
            receiveDamageType.Add(DamageType.Magic);
            receiveDamageType.Add(DamageType.EarthBound);
            hP = 20 + random.Next(10); // 10 - 20 HP
            maxHP = HP;
        }
    }

    internal class Archer : Human
    {
        public Archer() : base()
        {
            //Architype = ObjectArchitype.Human;

            humanType = HumanType.Archer;
            SubTypeName = humanType.ToString();
            minDamage = 5;
            maxDamage = 15;
            damageRad = 2;

            hP = 10 + random.Next(10); // 10 - 20 HP
            targetDamageType.Add(DamageType.Flying);

            receiveDamageType.Add(DamageType.EarthBound);
            receiveDamageType.Add(DamageType.Magic);
            maxHP = HP;
        }
    }
    internal class Mage : Human
    {
        public Mage() : base()
        {  
            humanType = HumanType.Mage;
            SubTypeName = humanType.ToString();
            minDamage = 5;
            maxDamage = 30;
            damageRad = 2;

            hP = 10  ; // 10   HP

            targetDamageType.Add(DamageType.Magic);

            receiveDamageType.Add(DamageType.EarthBound);
            receiveDamageType.Add(DamageType.Magic);
            maxHP = HP;
        }
    }
    
    internal class Monstr : Creature
    {
        internal static new ObjectArchitype staticArchitype;
        public override ObjectArchitype GetStaticArchitype()
        {
            return staticArchitype;
        }
        internal MonstrType monstrType;
        public enum MonstrType
        {
            Harpy,
            Orc,
            Golem,
            Dragon
        }
        public Monstr() : base()
        {
            staticArchitype = ObjectArchitype.Monstr;
        }
    }
    internal class Harpy : Monstr
    { 
        public Harpy(): base()
        {
            minDamage = 5;
            maxDamage = 10;
            damageRad = 1;

            monstrType = MonstrType.Harpy;
            SubTypeName = monstrType.ToString();

            hP = 6 + random.Next(6); // 10   HP
            targetDamageType.Add(DamageType.Flying);
            targetDamageType.Add(DamageType.EarthBound);           

            receiveDamageType.Add(DamageType.Flying);
            receiveDamageType.Add(DamageType.Magic);
            maxHP = HP;
        }
    }
    internal class Orc : Monstr
    {
        public Orc() : base()
        {
            defence_rate = 50;
            minDamage = 10;
            maxDamage = 15;
            damageRad = 1;

            monstrType = MonstrType.Orc;
            SubTypeName = monstrType.ToString();

            hP = 10 + random.Next(8); // 10   HP
            targetDamageType.Add(DamageType.Flying); // видимо, у них есть лук, не сказано, 
                                                     // что они НЕ атакуют летающих
            targetDamageType.Add(DamageType.EarthBound);

            receiveDamageType.Add(DamageType.EarthBound);
            receiveDamageType.Add(DamageType.Magic);
            maxHP = HP;
        }
    }
    internal class Golem : Monstr
    {
        public Golem() : base()
        {
            minDamage = 15;
            maxDamage = 15;
            damageRad = 1;

            monstrType = MonstrType.Golem;
            SubTypeName = monstrType.ToString();

            hP = 30 + random.Next(21); // 10   HP
            targetDamageType.Add(DamageType.Flying); // видимо, у них есть лук, не сказано, 
                                                     // что они НЕ атакуют летающих
            targetDamageType.Add(DamageType.EarthBound);

            receiveDamageType.Add(DamageType.EarthBound);
            //иммунитет к магии ( мы допускаем, что он не летает).
            maxHP = HP;
        }
    }
    internal class Dragon : Monstr
    {
        public Dragon() : base()
        {
            minDamage = 5;
            maxDamage = 30;
            damageRad = 2;

            monstrType = MonstrType.Dragon;
            SubTypeName = monstrType.ToString();

            hP = 100 + random.Next(51); // 10   HP
            targetDamageType.Add(DamageType.Flying); 
            targetDamageType.Add(DamageType.EarthBound);

            receiveDamageType.Add(DamageType.Flying);
            //иммунитет к магии  
            maxHP = HP;
        }
    }


    internal class Building : GameObject
    {
        internal static new ObjectArchitype staticArchitype;
        public override ObjectArchitype GetStaticArchitype()
        {
            return staticArchitype;
        }
        internal BuildingType buildingType;
        public enum BuildingType
        {
            Tree,
            Seno,
            Stone,
            Obelisk
        }
        public Building() : base()
        {
            staticArchitype = ObjectArchitype.Building;
        }
    }
    internal class DestroyableBuilding : Building
    {
        public DestroyableBuilding() : base()
        {

        }
    }
    internal class Tree : DestroyableBuilding
    {
        public Tree() : base()
        {
            buildingType = BuildingType.Tree;
            SubTypeName = buildingType.ToString();

            hP = 10; // 10   HP            

            receiveDamageType.Add(DamageType.Magic);
            receiveDamageType.Add(DamageType.EarthBound);
           
            maxHP = HP;
        }
    }
    internal class Seno : DestroyableBuilding
    {
        public Seno() : base()
        {
            buildingType = BuildingType.Tree;
            SubTypeName = buildingType.ToString();

            hP = 10 ; // 10   HP            

            receiveDamageType.Add(DamageType.Magic);
            receiveDamageType.Add(DamageType.EarthBound);

            maxHP = HP;
        }
    }
    internal class Obelisk : DestroyableBuilding
    { 
        public Obelisk() : base()
        {
            minDamage = 10;
            maxDamage = 20;
            damageRad = 2;

            buildingType = BuildingType.Obelisk;
            SubTypeName = buildingType.ToString();

            hP = 10; // 10   HP            

            receiveDamageType.Add(DamageType.Magic);
            receiveDamageType.Add(DamageType.EarthBound);

            maxHP = HP;
            targetDamageType.Add(DamageType.Magic);
        }
    }

    internal class UnDestroyableBuilding : Building
    {
        public UnDestroyableBuilding() : base()
        {

        }
    }
    internal class Stone : UnDestroyableBuilding
    {
        public Stone() : base()
        {
            buildingType = BuildingType.Stone;
            SubTypeName = buildingType.ToString();

            hP = 1; 

            maxHP = HP;
        }
    }
}
