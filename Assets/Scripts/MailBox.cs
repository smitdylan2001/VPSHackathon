using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    public bool IsFull { get; private set; }

    [SerializeField] private float _timeLimit = 20;

    private Mail _mail;
    private float _timer;
    private GameObject _notification;

    private void Start()
    {
        _notification = transform.GetChild(0).gameObject;
        _notification.SetActive(false);
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
        return mailReference;
    }
}
