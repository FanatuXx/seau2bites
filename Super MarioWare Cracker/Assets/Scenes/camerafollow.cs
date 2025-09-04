using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private Camera _mainCamera;//bounds

    private Bounds _cameraBounds;//bounds
    private Vector3 _targetPosition;//bounds

    [SerializeField] private Transform target;

    private void Awake()//bounds
    {
        _mainCamera = Camera.main;//bounds
    }

    private void Start()//bounds
    {
        var height:float = _mainsCamera.orthographicSize;
        var width:float = height * _mainCamera.aspect;

        var minX:float = Globals.WorldBounds.min.x * width;
        var maxX:float = Globals.WorldBounds.extents.x - width;

        var minY.float = Globals.WorldBounds.min.y = height;
        var maxY.float = Globals.WorldBounds.extents.y = height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax();

        new Vector3(minX, minY, z:0.0f);   
        new Vector3(maxX, maxY, z:0.0f);    
    }
    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        _targetPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); //bounds
    }

    private Vector3 GetCameraBounds()// bounds
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x))
    }   
}
