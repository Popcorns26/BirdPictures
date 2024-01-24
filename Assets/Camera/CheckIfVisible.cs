using UnityEngine;

public class CheckIfVisible : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;
    
    private Plane[] _cameraFrustrum;
    
    public bool CheckIsVisible(UnityEngine.Camera photoCamera)
    {
        if (!_renderer.isVisible)
        {
            return false;
        }
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(photoCamera);
        if (!GeometryUtility.TestPlanesAABB(_cameraFrustrum, _collider.bounds))
        {
            return false;
        }

        return true;
    }
}
