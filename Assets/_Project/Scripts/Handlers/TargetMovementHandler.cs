using UnityEngine;

public class TargetMovementHandler : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    
    private Target _target;
    private GridSystem _gridSystem;

    private void Start()
    {
        _gridSystem = GetComponent<GridSystem>();
        _target = Instantiate(targetPrefab);
    }

    public void SetTarget()
    {
      var pos =  _gridSystem.GridCells.Find(x => _gridSystem.FindPositionInNormals(x.Position));
        _target.transform.position = pos.Position;
    }
}
