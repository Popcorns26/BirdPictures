using System.Linq;
using Camera;
using UnityEngine;
using UnityEngine.UI;

public class BookWithPictures : MonoBehaviour
{
    [SerializeField] private PhotoCamera _photoCamera;
    [SerializeField] private RawImage _rawImage;
    public void Update()
    {
        if (_photoCamera._picturesTaken.Count < 1)
        {
            _rawImage.gameObject.SetActive(false);
            return;
        }
        _rawImage.gameObject.SetActive(true);
        _rawImage.texture = _photoCamera._picturesTaken.Last();
    }
}
