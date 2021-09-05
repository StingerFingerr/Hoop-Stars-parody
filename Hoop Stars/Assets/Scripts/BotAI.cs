using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    private enum Area
    {
        firstArea,
        secondArea,
        thirdArea,
        fourthArea
    }

    [SerializeField] private BotController _controller;
    [SerializeField] private Transform _ballTransform;
    [SerializeField] private Animator _spawnerAnimator;
    private GameManager gameManager;

    [SerializeField, Range(0.01f, 3)] private float _nearbyRadius;
    private float _yOffsetBallLevel = -1;

    [SerializeField] private float _reloadingTime;
    private float _nextTapTime;
    private Area _area;
    

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void StartGame()
    {
        _spawnerAnimator.enabled = false;
        StartCoroutine(BotLaunch());
    }
    IEnumerator BotLaunch()
    {
        while (gameManager.isGame)
        {
            if (Vector3.Distance(_ballTransform.position, transform.position) > _nearbyRadius)
            {
                if (_ballTransform.position.y - transform.position.y + _yOffsetBallLevel> 0)
                {
                    _area = Area.firstArea;
                }
                else
                {
                    _area = Area.secondArea;
                }
            }
            else
            {
                if (_ballTransform.position.y - transform.position.y + _yOffsetBallLevel > 0)
                {
                    _area = Area.thirdArea;
                }
                else
                {
                    _area = Area.fourthArea;
                }

            }

            switch (_area)
            {
                case Area.firstArea:
                    {
                        _controller.Tap(Random.Range(-1, 1) > 0 ? 1 : -1);
                        _nextTapTime = _reloadingTime;
                        break;
                    }
                case Area.secondArea:
                    {
                        _controller.Tap(_ballTransform.position.x - transform.position.x > 0 ? 1 : -1);
                        _nextTapTime = _reloadingTime*2;
                        break;
                    }
                case Area.thirdArea:
                    {
                        _controller.Tap(_ballTransform.position.x - transform.position.x>0?1:-1);
                        _nextTapTime = _reloadingTime*2;
                        break;
                    }
                case Area.fourthArea:
                    {
                        _nextTapTime = _reloadingTime;
                        break;
                    }
            }

            yield return new WaitForSeconds(_nextTapTime);
        }
    }
 
}
