using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    public float smoothSpeed;
    public Vector3 offset;

    public float maxPlayerY;
    public float minPlayerY;
    private float _playerDist;

    public float maxCamY;
    public float minCamY;
    private float _camDist;

    private float _intermediateY;


    private void Start()
    {
        _playerDist = maxPlayerY - minPlayerY;
        _camDist = maxCamY - minCamY;

        CalcAndSetNewPosition();
    }
    private void FixedUpdate()
    {
        CalcAndSetNewPosition();

    }
    void CalcAndSetNewPosition()
    {
        _intermediateY = (_player.position.y - minPlayerY) / _playerDist;
        Vector3 newCamPos = new Vector3(0, _intermediateY * _camDist + minCamY, 0) + offset;
        transform.position = Vector3.Lerp(transform.position, newCamPos, smoothSpeed * Time.deltaTime);
    }
}
