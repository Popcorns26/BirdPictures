using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _birdsSpawnedText;
    [SerializeField] private TextMeshProUGUI _birdsPicturesTakenText;
        

    public void SetBirdsPicturesTakenText(string text)
    {
        _birdsPicturesTakenText.text = text;
        _birdsPicturesTakenText.transform.DOScale(new Vector3(3, 3, 1), 1f).OnComplete(() => _birdsPicturesTakenText.transform.DORewind());
    }

    public void SetBirdsSpawnedText(string text)
    {
        _birdsSpawnedText.text = text;
            
    }
}