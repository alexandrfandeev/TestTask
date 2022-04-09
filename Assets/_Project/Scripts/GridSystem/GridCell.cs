using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public readonly Dictionary<Position, GridCell> Neighbours = new Dictionary<Position, GridCell>();

    public List<GridCell> ListWithAllNeighbours = new List<GridCell>();

    public bool Occupied { get; set; }
    public Vector2 Position { get; set; }
    


    public GridCell(Vector2 position)
    {
        Position = new Vector2(position.x + 0.5f, position.y + 0.5f);
    }

    public GridCell OnReturnCellByPlayerDirection(Position cellPositionInSpace)
    {
        return Neighbours[cellPositionInSpace];
    }
}

public enum Position { Forward, Backward, Left, Right }
