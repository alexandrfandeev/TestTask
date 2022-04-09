using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public CellVectors Vectors;
    public List<GridCell> GridCells = new List<GridCell>();
    
    private GridCell[,] _gridArray;
    private int _width = 7;
    private int _height = 7;
    private float _cellSize = 1f;

    public void Initialize()
    {
        _gridArray = new GridCell[_width, _height];
        
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
            _gridArray[x, y] = new GridCell(new Vector2(x, y));
            GridCells.Add(_gridArray[x, y]);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
        }
        
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
        
        OnConstructVirtualGrid();
    }

    private void OnConstructVirtualGrid()
    {
        foreach (GridCell cell in GridCells)
        {
            Dictionary<Position, GridCell> cellDictionary = cell.Neighbours;
            Position[] cellNeighbours = (Position[]) Enum.GetValues(typeof(Position));

            foreach (Position neighbour in cellNeighbours)
            {
                CellLocalPositionInSpace cellVectorUnit = Vectors.cellPositions.Find(x => x.enumLocalPosition == neighbour);
                Vector2 initialPosition = cellVectorUnit.localPosition + cell.Position;
                cellDictionary.Add(neighbour, GridCells.GetClosestCell(initialPosition));
                if (cellDictionary.ContainsKey(neighbour))
                    cell.ListWithAllNeighbours.Add(cellDictionary[neighbour]);
            }
        }
    }

    public GridCell GetClosestCell(Vector2 position)
    {
        return GridCells.OrderBy(x => Vector2.Distance(position, x.Position)).First();
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }
}