using System;
using System.Collections;
using _Project.Scripts.Handlers;
using UnityEngine;

public class NpcCore : MonoBehaviour
{
    private GridSystem _gridSystem;
    private Movement _movement;
    
    private Position _currentPositionInSpace;
    private Position _futurePositionInSpace;
    
    private GridCell _currentCell;
    private GridCell _futureCell;
    
    private bool _isInSearching;

    private void Awake()
    {
        _movement = FindObjectOfType<Movement>();
        _gridSystem = FindObjectOfType<GridSystem>();
    }

    private void Start()
    {
        _currentCell = _gridSystem.GetClosestCell(transform.position);
        transform.position = _currentCell.Position;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.L)) Move(Position.Left);
        if (Input.GetKey(KeyCode.R)) Move(Position.Right);
        if (Input.GetKey(KeyCode.F)) Move(Position.Forward);
        if (Input.GetKey(KeyCode.B)) Move(Position.Backward);
    }

    private void Move(Position direction)
    {
        _futurePositionInSpace = direction;
        if (!_isInSearching) StartCoroutine(MoveAndSearch(direction));
    }

    private IEnumerator MoveAndSearch(Position playerPosition)
    {
        _isInSearching = true;
        _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentPositionInSpace);
        _currentPositionInSpace = playerPosition;
        
        while (!_futureCell.Occupied)
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