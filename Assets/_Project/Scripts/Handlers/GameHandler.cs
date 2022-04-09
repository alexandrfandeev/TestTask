using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GameHandler : MonoBehaviour
{
    
    private ObstaclesHandler _obstaclesHandler;
    private GridSystem _gridSystem;
    private Random _rnd = new Random();
    
    private void Awake()
    {
        _gridSystem = FindObjectOfType<GridSystem>();
        _obstaclesHandler = FindObjectOfType<ObstaclesHandler>();
        _gridSystem.Initialize();
        _obstaclesHandler.Initialize();
    }
    

    private void Start()
    {
        InitializeBlockedPositions();
        Randomize();
    }

    private void Randomize()
    {
        for (int i = 0; i < _obstaclesHandler.Count; i++)
            Search();
        
        FindObjectOfType<TargetMovementHandler>().SetTarget();
    }

    private void Search()
    {
        Vector2 pos = _gridSystem.FirstCell();
        bool randomPosition = false;
        float x = _rnd.Next(1, 8) - 0.5f;
        float y = _rnd.Next(1, 8) - 0.5f;
        
        while (!randomPosition)
        {
            if (_gridSystem.FindPositionInNormals(pos))
            {
                randomPosition = true;
                _obstaclesHandler.SetObstaclePosition(pos);
                _gridSystem.SetCell(pos);
                _gridSystem.AddNormalPosition(pos);
            }

            
            x = _rnd.Next(1, 8) - 0.5f;
            y = _rnd.Next(1, 8) - 0.5f;
            pos = new Vector2(x, y);
        }
    }

    private void InitializeBlockedPositions()
    {
        for (float y = 1.5f, x = 3.5f; y < 6; y += 1)
        {
            var pos = new Vector2(x, y);
            if (_gridSystem.FindPositionInNormals(pos))
            {
                _gridSystem.AddNormalPosition(pos);
            }
        }
        
        for (float x = 1.5f, y = 3.5f; x < 6; x += 1)
        {
            var pos = new Vector2(x, y);
            if (_gridSystem.FindPositionInNormals(pos))
            {
                _gridSystem.AddNormalPosition(pos);
            }
        }

    }
}
