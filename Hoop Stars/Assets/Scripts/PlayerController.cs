using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private bool _isGrounded = false;
    private Vector3 _velocity;

    [SerializeField] private float _forcePower = 10f;
    [SerializeField] private float _yForceDir=2f;
    [SerializeField] private float _downwardAcceleration = .8f;
    [SerializeField] private float _maxOffsetX = 4;
    [SerializeField] private float _maxPosY;

    [SerializeField] private float _reloadingTapTime=.15f;
    private float _lastTapTime=0;
    private bool _isCanTap = true;

    [SerializeField] private GameManager gameManager;
    [SerializeField]private Animator _spawnerAnimator;
    [SerializeField] private GameObject _playerEffects;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        _lastTapTime += Time.deltaTime;
        _isCanTap = _lastTapTime >= _reloadingTapTime ? true : false;
        

        
    }

   

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag=="Wall")
    //    {

    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_isGrounded)
        {
            _velocity = _rb.velocity;
            _velocity.y -= _downwardAcceleration;
            _rb.velocity = _velocity;
        }
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
        if (!_isCanTap||!gameManager.isGame)
            return;
        if (_rb.isKinematic)
            _rb.isKinematic = false;
        Instantiate(_playerEffects, transform.position, Quaternion.identity);
        //_isGrounded = false;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(new Vector3(xDirection, _yForceDir, 0) * _forcePower, ForceMode.VelocityChange);
        _lastTapTime = 0;
        
    }
}
