using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAppBattleField
{   internal class Drawer
    {
        Field field;
        //задаётся палитра
        Color gridlineColor = Color.Black;
        Color emptyCellColor = Color.LightSteelBlue;
        Color monstrColor = Color.OrangeRed;
        Color humanColor = Color.AliceBlue;
        Color buildingColor = Color.RosyBrown;
        Color textColor = Color.Black;

        List<GameObject> objects2Update;
        Graphics gr;
        Font font;
        Brush brush = new SolidBrush(Color.Black);
        public Drawer(Graphics gr, Field field)
        {
            this.gr = gr;
            int fontsize = Field.CellSize/5;
            this.field = field;
            

            font = new Font(FontFamily.GenericSerif, fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
            brush = new SolidBrush(textColor);

            objects2Update = new List<GameObject>();
        }
        public void DrawGrid()
        {            
            foreach (Cell cell in field.Cells)
            {
                DrawCell(cell);
            }
            brush = new SolidBrush(gridlineColor);
            Pen pen = new Pen(brush);

            for (int i = 0; i <= field.RowsNumber; i++)
                gr.DrawLine(pen, 0, i * Field.CellSize, field.ColumnsNumber * Field.CellSize, i * Field.CellSize );

            for (int i = 0; i <= field.ColumnsNumber; i++)
            {
                gr.DrawLine(pen, i * Field.CellSize, 0, i * Field.CellSize, field.RowsNumber * Field.CellSize);
            }
        }
        public void DrawAllObjects()
        {
            DrawGrid();
            UpdateObjects();
        }
        public void UpdateAll()
        {
            UpdateCells();
            UpdateObjects();
        }
        public void UpdateCells()
        {
            foreach (Cell cell in field.Cells2Update)
                DrawCell(cell);

            field.Cells2Update.Clear();
        }
        public void UpdateObjects()
        {
            foreach (GameObject gameObject in objects2Update)
                DrawGameObject(gameObject);

            objects2Update.Clear();
        }
        public void DrawGameObject(GameObject gameObject)
        {
            DrawCell(gameObject.Cell);
            brush = new SolidBrush(textColor);            

            // gr.DrawString(gameObject.GetStaticArchitype().ToString(), font, brush, gameObject.Cell.X * Field.CellSize, gameObject.Cell.Y * Field.CellSize);
            gr.DrawString(gameObject.SubTypeName, font, brush, gameObject.Cell.X * Field.CellSize, gameObject.Cell.Y * Field.CellSize);
            int i = 1;
            foreach (DamageType damageType in gameObject.ReceiveDamageType)
            {                
                gr.DrawString(damageType.ToString().Substring(0,5), font, brush, gameObject.Cell.X * Field.CellSize, gameObject.Cell.Y * Field.CellSize + (Field.CellSize / 4)*i);
                i++; //отступ
            }
            gr.DrawString($"{gameObject.HP}/{gameObject.MaxHP}", font, brush, gameObject.Cell.X * Field.CellSize, gameObject.Cell.Y * Field.CellSize + Field.CellSize / 4 * 3);
        }
        public void AddGameObject4Update(GameObject gameObject)
        {
            if (!objects2Update.Contains(gameObject))
            objects2Update.Add(gameObject);
        }
        public void AddListGameObject4Update(List<GameObject> list)
        {
            objects2Update.AddRange(list.Where(x => !objects2Update.Contains(x)));
        }
        public void DrawCell(Cell cell)
        {
            
            if (cell.IsFree) brush = new SolidBrush(emptyCellColor);
            else if (cell.GameObjectOnCell.GetStaticArchitype() == ObjectArchitype.Human)
            {
                brush = new SolidBrush(humanColor);
            }
            else if (cell.GameObjectOnCell.GetStaticArchitype() == ObjectArchitype.Monstr)
                brush = new SolidBrush(monstrColor);
            else brush = new SolidBrush(buildingColor);

            gr.FillRectangle(brush, new Rectangle(2+cell.X*Field.CellSize, 2+ cell.Y*Field.CellSize, Field.CellSize -2, Field.CellSize-2));
        }
    }
}
