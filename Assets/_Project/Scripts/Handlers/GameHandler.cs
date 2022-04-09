using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private ObstaclesHandler _obstaclesHandler;
    private GridSystem _gridSystem;

    private void Awake()
    {
        _gridSystem = FindObjectOfType<GridSystem>();
        _obstaclesHandler = FindObjectOfType<ObstaclesHandler>();
        _gridSystem.Initialize();
        _obstaclesHandler.Initialize();
    }
    

    private void Start()
    {
        // _obstaclesHandler.SetObstaclePosition(_gridSystem.GetRandomWorldPosition());
        // _obstaclesHandler.SetObstaclePosition(_gridSystem.GetRandomWorldPosition());
    //     _obstaclesHandler.SetObstaclePosition(_gridSystem.GetCenterPosition());

        
        // for (float x = 1.5f; x < 6; x += 1f)
        // {
        //     for (float y = 1.5f; y < 6; y += 1f)
        //     {
        //         _obstaclesHandler.SetObstaclePosition(new Vector2(x, y));
        //     }
        // }
    }
}
