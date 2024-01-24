using UnityEngine;

namespace DefaultNamespace
{
    public class StopGameMode : MonoBehaviour
    {
        [SerializeField] private BirdPicturesGameMode _gameMode;
        private void OnTriggerEnter(Collider other)
        {
            _gameMode.StopSpawningBirds();
        }
    }
}