using System;
using System.Collections;
using _Project.Scripts.Handlers;
using UnityEngine;

public class NpcCore : MonoBehaviour
{
    private Transform _ownTransform;
    private GridSystem _gridSystem;
    private Movement _movement;
    
    private Position _currentPositionInSpace;
    private Position _futurePositionInSpace;
    
    private GridCell _currentCell;
    private GridCell _futureCell;
    
    private bool _isInSearching;
    private bool _isMovingVertically = false;

    private void Awake()
    {
        _ownTransform = transform;
        _movement = FindObjectOfType<Movement>();
        _gridSystem = FindObjectOfType<GridSystem>();
    }

    private void Start()
    {
        _currentCell = _gridSystem.GetClosestCell(_ownTransform.position);
        _ownTransform.position = _currentCell.Position;
    }

    public void StartCalculateDirection(Vector2 targetPosition)
    {
        _isMovingVertically = !_isMovingVertically;
        Vector2 playerPosition = _ownTransform.position;
        float xDifference = targetPosition.x - playerPosition.x;
        float yDifference = targetPosition.y - playerPosition.y;

        if (_isMovingVertically) _futurePositionInSpace  = xDifference > 0 ? Position.Right : Position.Left;
        else  _futurePositionInSpace  = yDifference > 0 ? Position.Forward : Position.Backward;
        
        Move();
    }
    

    private void Move()
    {
        if (!_isInSearching) StartCoroutine(MoveAndSearch(_futurePositionInSpace));
    }

    private IEnumerator MoveAndSearch(Position playerPosition)
    {
        _isInSearching = true;
        _currentPositionInSpace = playerPosition;
        _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentPositionInSpace);

        while (_futureCell is { Occupied: false })
        {
            yield return _movement.MoveTo(_futureCell.Position);
            // ReSharper disable once RedundantCheckBeforeAssignment
            if (_futurePositionInSpace != _currentPositionInSpace)
            {
                _currentPositionInSpace = _futurePositionInSpace;
            }
            
            OnSearchNextCell();
        }
        
        _isInSearching = false;
    }

    private void OnSearchNextCell()
    {
        _currentCell = _futureCell;
        _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentPositionInSpace);
    }
}
