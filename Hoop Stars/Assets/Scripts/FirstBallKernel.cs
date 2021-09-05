using UnityEngine;
using System.Collections;
public class FirstBallKernel : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private StressReceiver _camShake;
    public bool isAlreadyPlayerDetected = false;
    public bool isAlreadyBotDetected = false;

    [SerializeField] private float _xSpawnOffset;
    [SerializeField] private float _ySpawnOffset;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _botTransform;
    [SerializeField] private Transform _ballSkinTransform;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTrigger"&&!isAlreadyPlayerDetected)
        {
            PlayerTouchdown();
        }
        if (other.gameObject.tag == "BotTrigger" && !isAlreadyBotDetected)
        {
            BotTouchdown();
        }
    }

    private void RelocateBall()
    {
       

        StartCoroutine(RelocationAnimation());
    }
     IEnumerator RelocationAnimation()
    {
        float disappearanceTime = .5f;
        float appearanceTime = .5f;
        float animationStep = .05f;

        Vector3 scale=_ballSkinTransform.localScale;

        for(float i = disappearanceTime; i > 0; i -= animationStep)
        {
            var s = i / disappearanceTime;

            scale.x = s;
            scale.y = s;
            scale.z = s;

            _ballSkinTransform.localScale = scale;
            yield return new WaitForSeconds(animationStep);
        }

        Vector3 newPos;
        //do
        //{
        //    newPos = new Vector3(Random.Range(-_xSpawnOffset, _xSpawnOffset), Random.Range(-_ySpawnOffset + 2, _ySpawnOffset + 2), 0);
        //} while (Vector3.Distance(newPos, _playerTransform.position) < 2 && Vector3.Distance(newPos, _botTransform.position) < 2);
        newPos = new Vector3(Random.Range(-_xSpawnOffset, _xSpawnOffset), Random.Range(-_ySpawnOffset + 2, _ySpawnOffset + 2), 0);
        transform.parent.position = newPos;
        transform.parent.GetComponent<SpringJoint>().connectedAnchor = newPos;

        for (float i = 0; i <appearanceTime; i += animationStep)
        {
            var s = i / disappearanceTime;

            scale.x = s;
            scale.y = s;
            scale.z = s;

            _ballSkinTransform.localScale = scale;
            yield return new WaitForSeconds(animationStep);
        }
        yield return null;
    }
    public void PlayerTouchdown()
    {
        isAlreadyPlayerDetected = true;
        gameManager.IncreasePlayerScore();
        PlayEffects();

        RelocateBall();
    }

    public void BotTouchdown()
    {
        isAlreadyBotDetected = true;
        gameManager.IncreaseBotScore();

        RelocateBall();
    }

    private void PlayEffects()
    {
        _camShake.InduceStress(.5f);
    }
}
