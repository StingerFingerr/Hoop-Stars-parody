using UnityEngine;

public class BotController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _velocity;

    [SerializeField] private float _forcePower = 10f;
    [SerializeField] private float _yForceDir = 2f;
    [SerializeField] private float _downwardAcceleration = .8f;
    [SerializeField] private float _maxOffsetX = 4;
    [SerializeField] private float _maxPosY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

   

    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        _velocity.y -= _downwardAcceleration;
        _rb.velocity = _velocity;

        _velocity = _rb.velocity;
        var pos = transform.position;
        if (pos.x <= -_maxOffsetX || pos.x > _maxOffsetX)
        {
            transform.position = new Vector3(Mathf.Clamp(pos.x, -_maxOffsetX, _maxOffsetX), pos.y, 0);
            _velocity.x *= -1f;
            _rb.velocity = _velocity;
        }
        if (pos.y >= _maxPosY)
        {
            pos.y = Mathf.Clamp(pos.y, -_maxPosY, _maxPosY);
            transform.position = pos;
        }
    }
    public void Tap(int xDirection)
    {
        _rb.velocity = Vector3.zero;
        _rb.AddForce(new Vector3(Mathf.Clamp(xDirection*1000,-1,1), _yForceDir, 0) * _forcePower, ForceMode.VelocityChange);

    }
}
