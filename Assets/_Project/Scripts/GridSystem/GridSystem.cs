using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public CellVectors Vectors;
    public readonly List<GridCell> GridCells = new List<GridCell>();


    private readonly List<Vector2> _normalPositions = new List<Vector2>();
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
                GridCell neighbourCell = GetClosestCell(initialPosition);
                if (neighbourCell == null) continue;
                cellDictionary.Add(neighbour, GetClosestCell(initialPosition));
                if (cellDictionary.ContainsKey(neighbour))
                    cell.ListWithAllNeighbours.Add(cellDictionary[neighbour]);
            }
        }
    }

    public void AddNormalPosition(Vector2 position)
    {
        _normalPositions.Add(position);
    }

    public bool FindPositionInNormals(Vector2 position)
    {
        return _normalPositions.All(j => j != position);
    }

    public Vector2 FirstCell()
    {
        return _normalPositions[0];
    }

    public void SetCell(Vector2 position)
    {
        GridCells.OrderBy(x => Vector2.Distance(position, x.Position)).First().Occupied = true;
    }

    public GridCell GetClosestCell(Vector2 position)
    {
        var cell = GridCells.OrderBy(x => Vector2.Distance(position, x.Position)).First();
        if (Vector2.Distance(cell.Position, position) > 0.1f) return null;
         return cell;
    }

    public bool IsFreeCell(int index)
    {
        return !GridCells[index].Occupied;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }
}
