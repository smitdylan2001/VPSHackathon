using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    public bool IsFull { get; private set; }

    [SerializeField] private float _timeLimit = 20, _animationRotateAmount = 2, _animationTime = 1f;
    [SerializeField] private AudioSource _audioSource;

    private Mail _mail;
    private float _timer;
    private GameObject _notification;
    private Vector3 _rotatePivot;

    private void Start()
    {
        _notification = transform.GetChild(0).gameObject;
        _notification.SetActive(false);

        _rotatePivot = transform.position + (transform.up * (transform.localScale.x / 2));
    }

    private void Update()
    {
        if (!IsFull) return;

        _timer += Time.deltaTime;

        if(_timer > _timeLimit)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void FillBox(Mail mail)
    {
        _mail = mail;
        IsFull = true;
        _notification.SetActive(true);
    }

    public Mail EmptyBox()
    {
        Mail mailReference = _mail;
        _mail = null;
        IsFull = false;
        _timer = 0;
        _notification.SetActive(false);
        StartCoroutine(EmptyAnimation());
        return mailReference;
    }

    IEnumerator EmptyAnimation()
    {
        var timer = 0f;
        var _rotateAxis = -transform.right;
        _audioSource.Play();

        while (timer < _animationTime)
        {
            transform.RotateAround(_rotatePivot, _rotateAxis, _animationRotateAmount * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        while (timer < _animationTime)
        {
            transform.RotateAround(_rotatePivot, _rotateAxis, -_animationRotateAmount * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
