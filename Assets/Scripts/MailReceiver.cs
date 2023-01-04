using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailReceiver : MonoBehaviour
{
    [SerializeField] private MailInfo _requiredMailInfo;
    [SerializeField] private GameObject _mouth;
    [SerializeField] private float _animationTime = 0.25f;
    float timer;
    private void Start()
    {
        _mouth.GetComponent<MeshRenderer>().material.color = _requiredMailInfo.Color;
        StartCoroutine(EatMailAnimation());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Mail"))
        {
            return;
        }

        var mail = collision.GetComponent<Mail>();

        if(!mail || mail.MailInfo.MailColor != _requiredMailInfo.MailColor)
        {
            //Throw wrong color warning

            return;
        }

        Destroy(mail.gameObject);
        StartCoroutine(EatMailAnimation());

        //IncreasePoint
        ScoreManager.Instance.IncreaseScore(1);
    }

    IEnumerator EatMailAnimation()
    {
        var ogscale = _mouth.transform.localScale;
        var newScale = ogscale + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

        timer = 0;
        while (timer < 1)
        {
            _mouth.transform.localScale = Vector3.Lerp(ogscale, newScale, timer);
            timer += Time.deltaTime / _animationTime;
            yield return null;
        }

        timer = 0;
        while (timer < 1)
        {
            _mouth.transform.localScale = Vector3.Lerp(newScale, ogscale, timer);
            timer += Time.deltaTime / _animationTime;
            yield return null;
        }

        timer = 0;
        while (timer < 1)
        {
            _mouth.transform.localScale = Vector3.Lerp(ogscale, newScale, timer);
            timer += Time.deltaTime / _animationTime;
            yield return null;
        }

        timer = 0;
        while (timer < 1)
        {
            _mouth.transform.localScale = Vector3.Lerp(newScale, ogscale, timer);
            timer += Time.deltaTime / _animationTime;
            yield return null;
        }
    }
}
