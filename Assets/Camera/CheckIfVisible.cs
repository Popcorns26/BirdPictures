using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfVisible : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private UnityEngine.Camera _photoCamera;
    [SerializeField] private Collider _collider;
    
    private Plane[] _cameraFrustrum;

    private void Start()
    {
        //_bounds = GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(_photoCamera);
    }
    

    public bool CheckIsVisible(Vector3 cameraPosition)
    {
        if (!_renderer.isVisible)
        {
            return false;
        }

        if (!GeometryUtility.TestPlanesAABB(_cameraFrustrum, _collider.bounds))
        {
            return false;
        }
        Vector3 direction = transform.position - cameraPosition;
        Physics.Raycast(cameraPosition, direction, out RaycastHit hit,15);
        if (hit.collider.gameObject == gameObject)
        {
            Debug.Log("This bird was raycast hit: " + gameObject.name);
        }
        else
        {
            Debug.Log("Another collider was raycast hit: " + hit.collider.gameObject);
        }
        return hit.collider.gameObject == gameObject;
    }
}
