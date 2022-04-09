using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods 
{
    public static GridCell GetClosestCell(this IEnumerable<GridCell> cells, Vector2 position)
    {
        GridCell closest = cells.OrderBy(x => Vector2.Distance(x.Position, position)).First();
        return Vector2.Distance(closest.Position, position) < 0.1f ? closest : null;
    }
}
