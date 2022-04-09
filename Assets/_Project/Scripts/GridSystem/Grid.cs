using UnityEngine;

public class Grid
{
    System.Random _rnd = new System.Random();
    
    
    private int[,] _gridArray;
    private int _width = 7;
    private int _height = 7;
    private int _cellSize = 1;

    public Grid()
    {
        _gridArray = new int[_width, _height];
        
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        for (int y = 0; y < _gridArray.GetLength(1); y++)
        {
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
        }
        
        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
    }


    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }

    // just checking
    public Vector2 GetRandomWorldPosition()
    {
        int randomWidth = _rnd.Next(1, 7);
        int randomHeight = _rnd.Next(1, 7);
        Debug.Log(randomHeight + " " + randomWidth);
        return new Vector2(randomWidth - 0.5f, randomHeight - 0.5f);
    }
    
    
    
}
