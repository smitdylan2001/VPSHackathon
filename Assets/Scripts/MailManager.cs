using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MailManager : MonoBehaviour
{
    [SerializeField] MailBox[] _mailBoxes;
    [SerializeField] MailInfo[] _mailTypes;
    [SerializeField] GameObject _mailPrefab;
    [SerializeField] Vector2Int _minMaxDelay;

    private List<MailBox> emptyBoxes = new List<MailBox>();

    private async void Start()
    {
        if (_mailBoxes.Length == 0)
        {
            _mailBoxes = GetComponentsInChildren<MailBox>();
        }

        await Task.Delay(Random.Range(200, 400));

        FillMailBox(GetRandomMailBox(), GetRandomMail());
    }

    private async void FillMailBox(MailBox mailBox, Mail mail)
    {
        if(mailBox == null || GameManager.Instance.PlayerDied)
        {
            GameManager.Instance.GameOver();

            return;
        }

        mailBox.FillBox(mail);
        //mailBox.GetComponent<MeshRenderer>().material.color = Color.red;

        await Task.Delay(Random.Range(_minMaxDelay.x, _minMaxDelay.y));

        FillMailBox(GetRandomEmptyMailBox(), GetRandomMail());
    }

    private MailBox GetRandomMailBox()
    {
        return _mailBoxes[Random.Range(0, _mailBoxes.Length)];
    }

    private MailBox GetRandomEmptyMailBox()
    {
        emptyBoxes.Clear();

        foreach (MailBox mailBox in _mailBoxes)
        {
            if(!mailBox.IsFull) emptyBoxes.Add(mailBox);
        }

        if(emptyBoxes.Count == 0)
        {
            return null;
        }

        return emptyBoxes[Random.Range(0, emptyBoxes.Count)];
    }

    private Mail GetRandomMail()
    {
        GameObject newMailObject = Instantiate(_mailPrefab);
        var mail = newMailObject.GetComponent<Mail>();
        mail.MailInfo = GetRandomMailType();
        var mr = newMailObject.GetComponent<MeshRenderer>();
        mr.material.color = mail.MailInfo.Color;
        mr.enabled = false;
        return mail;
    }

    private MailInfo GetRandomMailType()
    {
        return _mailTypes[Random.Range(0, _mailTypes.Length)];
    }
}
