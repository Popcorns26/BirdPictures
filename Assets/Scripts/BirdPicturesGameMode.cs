using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPicturesGameMode : MonoBehaviour
{
    //Maybe bind these differently
    [SerializeField] private List<CheckIfVisible> _allBirdies;
    [SerializeField] private Collider _startGameCollider;
    [SerializeField] private FlyingObject _birdTemplate;
    [SerializeField] private GameObject[] _birdHouses;
    [SerializeField] private MainUIController _mainUIController;
    [SerializeField] private bool _spawnBirdsLoopActive;
    private FlyingObject _bird;
    public int BirdsSpawned { get; private set; }
    public int PicturesWithBirds { get; set; }

    //Move into start game area
    private void OnTriggerEnter(Collider other)
    {
        _spawnBirdsLoopActive = true;
        _startGameCollider.enabled = false;
        StartCoroutine(SpawnBird());
    }
    
    public IEnumerator SpawnBird()
    {
        while (_spawnBirdsLoopActive)
        {
            int startHouse = Random.Range(0, _birdHouses.Length);
            int endHouse = Random.Range(0, _birdHouses.Length);
            if (endHouse == startHouse)
            {
                endHouse += 1;
                endHouse %= _birdHouses.Length;
            }
            //Debug.Log($"start: {startHouse}, end: {endHouse}");
            //Spawn a bird.
            _bird = Instantiate(_birdTemplate, _birdHouses[startHouse].transform);
            AddBirdSpawned();
            _allBirdies.Add(_bird);
            //Make bird fly
            _bird.Fly(_birdHouses[startHouse].transform.position, _birdHouses[endHouse].transform.position);
            yield return new WaitForSeconds(_bird.flightDuration);
            //Make it disappear
            //Debug.Log("Despawning bird");
            _allBirdies.Remove(_bird);
            Destroy(_bird.gameObject);
            _bird = null;
            //Next in 10 seconds
            yield return new WaitForSeconds(5f);
            //Debug.Log("Ready to spawn new bird");
        }

        StopSpawningBirds();
    }

    public void StopSpawningBirds()
    {
        _startGameCollider.enabled = true;
        StopCoroutine(SpawnBird());
        _spawnBirdsLoopActive = false;
    }

    public List<CheckIfVisible> GetBirdsList()
    {
        return _allBirdies;
    }

    private void AddBirdSpawned()
    {
        BirdsSpawned++;
        _mainUIController.UpdateBirdsSpawned(BirdsSpawned.ToString());
    }

    public void AddBirdPicture()
    {
        PicturesWithBirds++;
        _mainUIController.UpdateBirdsPicturesTaken(PicturesWithBirds.ToString());
    }
}
