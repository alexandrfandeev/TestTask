using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private ObstaclesHandler _obstaclesHandler;
    private Grid _grid;

    private void Awake()
    {
        _grid = new Grid();
        _obstaclesHandler = FindObjectOfType<ObstaclesHandler>();
        _obstaclesHandler.Initialize();
    }

    private void Start()
    {
        _obstaclesHandler.SetObstaclePosition(_grid.GetRandomWorldPosition());
        _obstaclesHandler.SetObstaclePosition(_grid.GetRandomWorldPosition());
    }
}
