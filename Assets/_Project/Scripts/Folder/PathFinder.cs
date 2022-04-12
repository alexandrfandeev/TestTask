using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Folder
{
    public class PathFinder
    {
        public List<GridCell> FindPath(GridCell start, GridCell end)
        {
            FinalPath = new List<GridCell>();
            Visit(start,end, new List<GridCell>(), Position.Left);
            if (FinalPath.Count <= 0) Visit(start,end, new List<GridCell>(), Position.Right);
            return FinalPath;
        }

        public List<GridCell> FinalPath = new List<GridCell>();

        private void Visit(GridCell currentCell, GridCell finalCell, List<GridCell> currentPath, Position previousDirection)
        {
            if (FinalPath.Count > 0) return;
            
            if (currentCell == finalCell)
            {
                currentPath.Add(finalCell);
                FinalPath = currentPath;
                return;
            }

            List<GridCell> newCurrentPath = new List<GridCell>( currentPath);
            Debug.Log(currentCell.Neighbours.ContainsKey(previousDirection));

            if (currentCell.Neighbours.ContainsKey(previousDirection) && !currentCell.Neighbours[previousDirection].Occupied &&
                !newCurrentPath.Contains(currentCell.Neighbours[previousDirection]))
            {
                newCurrentPath.Add(currentCell);
                Visit(currentCell.Neighbours[previousDirection], finalCell, newCurrentPath, previousDirection);
            }
            
            else if (currentCell.Neighbours.ContainsKey(Position.Left) && !currentCell.Neighbours[Position.Left].Occupied &&
                     !newCurrentPath.Contains(currentCell.Neighbours[Position.Left]))
            {
                newCurrentPath.Add(currentCell);
                Visit(currentCell.Neighbours[Position.Left], finalCell, newCurrentPath, Position.Left);
            }
            
            else if (currentCell.Neighbours.ContainsKey(Position.Right) && !currentCell.Neighbours[Position.Right].Occupied &&
                     !newCurrentPath.Contains(currentCell.Neighbours[Position.Right]))
            {
                newCurrentPath.Add(currentCell);
                Visit(currentCell.Neighbours[Position.Right], finalCell, newCurrentPath, Position.Right);
            }
            
            else if (currentCell.Neighbours.ContainsKey(Position.Forward) && !currentCell.Neighbours[Position.Forward].Occupied &&
                     !newCurrentPath.Contains(currentCell.Neighbours[Position.Forward]))
            {
                newCurrentPath.Add(currentCell);
                Visit(currentCell.Neighbours[Position.Forward], finalCell, newCurrentPath, Position.Forward);
            }
            
            else if (currentCell.Neighbours.ContainsKey(Position.Backward) && !currentCell.Neighbours[Position.Backward].Occupied &&
                     !newCurrentPath.Contains(currentCell.Neighbours[Position.Backward]))
            {
                newCurrentPath.Add(currentCell);
                Visit(currentCell.Neighbours[Position.Backward], finalCell, newCurrentPath, Position.Backward);
            }
        }
        
    }
}
