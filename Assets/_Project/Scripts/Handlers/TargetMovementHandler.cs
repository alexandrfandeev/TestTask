using UnityEngine;

public class TargetMovementHandler : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    
    private Target _target;
    private GridSystem _gridSystem;
    private NpcCore _npcCore;

    private void Start()
    {
        _npcCore = FindObjectOfType<NpcCore>();
        _gridSystem = GetComponent<GridSystem>();
        _target = Instantiate(targetPrefab);
    }

    public void SetTarget()
    {
      GridCell pos =  _gridSystem.GridCells.Find(x => _gridSystem.FindPositionInNormals(x.Position));
      _target.transform.position = pos.Position;
      _npcCore.StartCalculateDirection(pos.Position);
    }
}
