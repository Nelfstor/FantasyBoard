using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppBattleField
{
    internal class Field
    {        
        int columnsNumber; // x
        public int ColumnsNumber { get => columnsNumber; }
        int rowsNumber; // y 
        public int RowsNumber { get => rowsNumber; }
        static int cellSize;
        public static int CellSize { get => cellSize; }
        static int cellsOccupied;
        public static int CellsOccupied { get => cellsOccupied; set { cellsOccupied = value; } }
        int totalCellNum;
        Cell[,] cells;
        public Cell[,] Cells { get => cells; }
        List<Cell> cells2Update;
        public  List<Cell>? Cells2Update { get => cells2Update; set {cells2Update = value; } } // Список для отрисовки
        Random random;

        public Field(int panelHeight, int panelWidth, int columnsNumber, int rowsNumber)
        {
            this.rowsNumber = rowsNumber;
            this.columnsNumber = columnsNumber;
            // общее количество клеток
            totalCellNum = columnsNumber * rowsNumber;
            random = new Random();
            cellsOccupied = 0;
            // Размер клетки выбирается исходя из того - который размер меньше.
            // размер панели / количество по иксу или размер панели / количество по y
            // Сами клетки - квадратны
            if (panelWidth / columnsNumber < panelHeight / rowsNumber)
                cellSize = panelWidth / columnsNumber -1;
            else cellSize = panelHeight / rowsNumber - 1;

            //создаётся массив клеток
            cells = new Cell[columnsNumber, rowsNumber];

            //создаются сами клетки
            for (int i = 0; i < columnsNumber; i++)
                for (int j = 0; j < rowsNumber; j++)
                    cells[i, j] = new Cell(i, j);

            Cell.SetField(this);

            // Добавляем все клетки в список для отрисовки ( далее, будем отрисовывать только те,
            // что изменены
            cells2Update = new List<Cell>();
            foreach (Cell cell in cells)
            {
                cells2Update.Add(cell);
            }
        }
        /// <summary>
        /// Изымаем клетку из списка для отрисовки
        /// </summary>
        /// <param name="cell"></param>
        public void RemoveFromUpdate(Cell cell)
        {
            cells2Update.Remove(cell);
        }
        /// <summary>
        /// Выбирает свободную клетку в двумерном массиве. 
        /// Рандом может бить в клетки, которые уже заняты, так что метод 
        /// можно было бы усовершенствовать.
        /// Однако это решение - рабочее.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Cell GetFreeCell()
        {
            if (cellsOccupied < totalCellNum)
            {                
                int x = random.Next(columnsNumber );
                int y = random.Next(rowsNumber );
                while (!cells[x, y].IsFree)
                {
                    x = random.Next(columnsNumber );
                    y = random.Next(rowsNumber );
                }
                return cells[x, y];
            }
            else throw new ArgumentException("нет свободных клеток");
        }

        /// Вариант реализации поиска свободной клетки без попытки рандома бить 
        /// в уже занятые клетки

        /*
        public Cell GetFreeCell() 
        {
            if (freeCells.Count > 0)
            {
                if (freeCells.First().Free) //проверка, не убыл ли там GameObject c клетки
                    return freeCells.First();
                else return GetFreeCell();
            }
            else return null;
        }*/
    }

    internal class Cell
    {
        static Field field;
        int x;
        public int X { get => x ;}
        int y;
        public int Y { get => y; }
        bool isFree = true;
        public bool IsFree { get =>isFree; }
        GameObject gameObjectOnCell;
        public GameObject GameObjectOnCell { get => gameObjectOnCell; }

        public static void SetField(Field field)
        {
            Cell.field = field;
        }
        public Cell(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public void SetObjectOnCell(GameObject gameObject)
        {
            gameObjectOnCell = gameObject;
            isFree = false;
            Field.CellsOccupied++;
            if (!field.Cells2Update.Contains(this))
                field.Cells2Update.Add(this);
        }
        public void FreeCell()
        {
            gameObjectOnCell = null;
            isFree = true;
            Field.CellsOccupied--;
            field.Cells2Update.Add(this);
        }
    }
}
