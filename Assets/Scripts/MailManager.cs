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

    private AudioSource _audioSource;
    private List<MailBox> emptyBoxes = new List<MailBox>();

    private IEnumerator Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_mailBoxes.Length == 0)
        {
            _mailBoxes = GetComponentsInChildren<MailBox>();
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 1.3f));

        StartCoroutine(FillMailBox(GetRandomMailBox(), GetRandomMail()));
    }

    private IEnumerator FillMailBox(MailBox mailBox, Mail mail)
    {
        while (true)
        {
            if (mailBox == null || GameManager.Instance.PlayerDied)
            {
                GameManager.Instance.GameOver();

                yield break;
            }

            mailBox.FillBox(mail);
            _audioSource.Play();

            mailBox = GetRandomEmptyMailBox();
            mail = GetRandomMail();
            yield return new WaitForSeconds(Random.Range(_minMaxDelay.x, _minMaxDelay.y));
        }
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
