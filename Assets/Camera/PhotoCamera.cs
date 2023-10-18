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
        private Texture2D _lastImage;
        private float _showLastImageTimeRemaining;
        public static float timeToShowLastImage { get; } = 2f;
        private int _picturesTakenNb;

        public List<Texture2D> _shotsTaken = new();

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
            //Graphics.CopyTexture(_cameraRT, _lastImage);
            if (_showLastImageTimeRemaining > 0)
            {
                return;
            }
            AsyncGPUReadback.Request(_cameraRT, 0, request =>
            {
                
                _lastImage.SetPixelData(request.GetData<byte>(), 0);
                _lastImage.Apply();
                _lastTakenImage.texture = _lastImage;
                
                _shotsTaken.Add(_lastImage);
            
                //Save the image to file
                var currentTime = DateTime.Now;
                string fileName = $"Picture_{currentTime:d}_{_picturesTakenNb+1}.jpeg";
                fileName = System.IO.Path.Combine(Application.persistentDataPath, fileName);
                System.IO.File.WriteAllBytes(fileName, _lastImage.EncodeToJPG());
                Debug.Log("Filename: "+ fileName);
                _picturesTakenNb++;
            });
            _showLastImageTimeRemaining = timeToShowLastImage;
            _liveDisplayImage.gameObject.SetActive(false);
            _lastTakenImage.gameObject.SetActive(true);
        }
    }
}