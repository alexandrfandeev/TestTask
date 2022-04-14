using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.NPC
{
    public class NpcCore : MonoBehaviour
    {
        private Vector2 _targetPosition;
        private Transform _ownTransform;
        private GridSystem _gridSystem;
        private Movement _movement;

        private Position _initialDirection;
      [SerializeField]  private Position _currentDirection = Position.Forward;
        
        private GridCell _currentCell;
        private GridCell _futureCell;
        private GridCell _endCell;

        private bool _firstTime;
        
        
        private bool _isInSearching;
        

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
        

        private IEnumerator MoveToPath()
        {
            _isInSearching = true;
            _currentCell = _gridSystem.GetClosestCell(_ownTransform.position);
            _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentDirection);
            

            while (_futureCell is {Occupied : false} && _currentCell != _endCell)
            {
                yield return _movement.MoveTo(_futureCell.Position);
                ChangeDirection();
                print("in moving");
            }

            _isInSearching = false;
        }

        private void ChangeDirection()
        {
            _currentCell = _futureCell;
            _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentDirection);
            ChangeDirections();
            if (_currentCell == _futureCell)
            {
                print("same, blocked.");
            }
        }

        private void ChangeDirections()
        {
            if (_futureCell == null)
            {
                if (_currentDirection == Position.Left || _currentDirection == Position.Right) ChangeToVerticalDirection();
                else ChangeToHorizontalDirection();
                _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentDirection);
                
                return;
            }

            if (_futureCell.Occupied)
            { 
                if (_currentDirection == Position.Left || _currentDirection == Position.Right) ChangeToVerticalDirection();
                else ChangeToHorizontalDirection();
                _futureCell = _currentCell.OnReturnCellByPlayerDirection(_currentDirection);
            }
        }

        private void ChangeToVerticalDirection()
        {
            if (_currentCell.Neighbours.ContainsKey(Position.Backward) &&
                !_currentCell.Neighbours[Position.Backward].Occupied)
            {
                _currentDirection = Position.Backward;
                return;
            }

            if (_currentCell.Neighbours.ContainsKey(Position.Forward) &&
                !_currentCell.Neighbours[Position.Forward].Occupied)
            {
                _currentDirection = Position.Forward;
                return;
            }
            
            
        }

        private void ChangeToHorizontalDirection()
        {
            if (_currentCell.Neighbours.ContainsKey(Position.Left) && !_currentCell.Neighbours[Position.Left].Occupied)
            {
                _currentDirection = Position.Left;
            }

            if (_currentCell.Neighbours.ContainsKey(Position.Right) &&
                !_currentCell.Neighbours[Position.Right].Occupied)
            {
                _currentDirection = Position.Right;
            }
            
            
        }

        public void MoveToTarget(Vector2 targetPosition)
        {
            _targetPosition = targetPosition;
            _endCell = _gridSystem.GetClosestCell(targetPosition);
            if (_firstTime)
            {
                _currentDirection = _targetPosition.x - _ownTransform.position.x < 0 ? Position.Left : Position.Right;
                _firstTime = true;
            }
            
            if (!_isInSearching) StartCoroutine(MoveToPath());
        }
    }
}
