using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(0.05f, 0.5f)] private float movingSpeed = 0.5f;

        private readonly WaitForSeconds _waitingForNextRotation = new WaitForSeconds(0.1f);
        
        private Vector2 _position;
        private const float ApproximateLimit = 0.01f;
    
        public IEnumerator MoveTo(Vector2 targetPosition)
        {
            _position = targetPosition;

            while (Vector2.Distance(_position, transform.position) > ApproximateLimit)
            {
                transform.position = Vector3.MoveTowards(transform.position, _position, Time.deltaTime * movingSpeed);
                yield return null;
            }
        }

        public IEnumerator LookAtDirection(Position direction)
        {
            
            yield return _waitingForNextRotation;
        }
    }
}
