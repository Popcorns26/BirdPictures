using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private MainUIView _view;
    public void UpdateBirdsSpawned(string text)
    {
        _view.SetBirdsSpawnedText(text);
    }

    public void UpdateBirdsPicturesTaken(string text)
    {
        _view.SetBirdsPicturesTakenText(text);
    }
}
