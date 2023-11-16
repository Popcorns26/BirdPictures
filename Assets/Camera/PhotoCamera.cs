using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Camera
{
    public class PhotoCamera : MonoBehaviour
    {
        [SerializeField] private RawImage _liveDisplayImage;
        [SerializeField] private RawImage _lastTakenImage;
        [SerializeField] private RenderTexture _cameraRT;
        [SerializeField] private CheckIfVisible[] _birds;
        [SerializeField] private UnityEngine.Camera _camera;
        private Texture2D _lastImage;
        private float _showLastImageTimeRemaining;
        public static float timeToShowLastImage { get; } = 2f;
        private int _picturesTakenNb;

        public List<Texture2D> _picturesTaken = new();

        private void Start()
        {
            _lastImage = new Texture2D(_cameraRT.width, _cameraRT.height,
                                        _cameraRT.graphicsFormat,
                                        UnityEngine.Experimental.Rendering.TextureCreationFlags.None);
        }

        private void Update()
        {
            if (_showLastImageTimeRemaining > 0)
            {
                _showLastImageTimeRemaining -= Time.deltaTime;

                if (_showLastImageTimeRemaining <= 0f)
                {
                    _liveDisplayImage.gameObject.SetActive(true);
                    _lastTakenImage.gameObject.SetActive(false);
                }
            }
        }

        public void TakePicture()
        {
            if (_showLastImageTimeRemaining > 0)
            {
                return;
            }

            foreach (CheckIfVisible bird in _birds)
            {
                string answer = bird.CheckIsVisible(_camera.transform.position) ? "Yes" : "No";
                Debug.Log($"This bird is visible? {answer}");
            }
            
            AsyncGPUReadback.Request(_cameraRT, 0, request =>
            {
                
                _lastImage.SetPixelData(request.GetData<byte>(), 0);
                _lastImage.Apply();
                _lastTakenImage.texture = _lastImage;
                
                _picturesTaken.Add(_lastImage);
            
                //Save the image to file
                SaveTextureToJpeg();
                _picturesTakenNb++;
            });
            _showLastImageTimeRemaining = timeToShowLastImage;
            _liveDisplayImage.gameObject.SetActive(false);
            _lastTakenImage.gameObject.SetActive(true);
        }

        private void SaveTextureToJpeg()
        {
            var currentTime = DateTime.Now;
            string fileName = $"Picture_{currentTime:d}_{_picturesTakenNb + 1}.jpeg";
            fileName = System.IO.Path.Combine(Application.persistentDataPath, fileName);
            System.IO.File.WriteAllBytes(fileName, _lastImage.EncodeToJPG());
            Debug.Log("Picture saved at : " + fileName);
        }
    }
}