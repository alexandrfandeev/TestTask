using UnityEngine;

public class ObstaclesHandler : MonoBehaviour
{
    [SerializeField, Range(1, 18)] private int obstaclesCount = 18;
    [SerializeField] private Obstacle obstacleRoot;

    public int Count => obstaclesCount;
    
    private Obstacle[] _obstaclesPoolArray;
    private int _currentObstacleIndex;

    public void Initialize()
    {
        _obstaclesPoolArray = new Obstacle[obstaclesCount];

        for (int i = 0; i < obstaclesCount; i++)
        {
            _obstaclesPoolArray[i] = Instantiate(obstacleRoot, transform);
            _obstaclesPoolArray[i].gameObject.SetActive(false);
        }
    }

    public void SetObstaclePosition(Vector2 position)
    {
        Obstacle currentObstacle = _obstaclesPoolArray[_currentObstacleIndex];
        currentObstacle.gameObject.SetActive(true);
        currentObstacle.transform.position = position;
        _currentObstacleIndex++;
    }
}
