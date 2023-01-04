using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    public bool IsFull { get; private set; }

    [SerializeField] private float _timeLimit = 7;

    private Mail _mail;
    private float _timer;

    private void Update()
    {
        if (!IsFull) return;

        _timer += Time.deltaTime;

        if(_timer > _timeLimit)
        {
            //Kill player
        }
    }

    public void FillBox(Mail mail)
    {
        _mail = mail;
        IsFull = true;
    }

    public Mail EmptyBox()
    {
        Debug.Log(_mail);
        Mail mailReference = _mail;
        _mail = null;
        Debug.Log(mailReference);
        IsFull = false;
        _timer = 0;
        return mailReference;
    }
}
