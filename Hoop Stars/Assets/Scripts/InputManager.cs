using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    void Start()
    {
        
    }

    public void RightTap()
    {
        _player.Tap(1);
    }
    public void LeftTap()
    {
        _player.Tap(-1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _player.Tap(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _player.Tap(1);
        }
    }
}
