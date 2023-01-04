using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailReceiver : MonoBehaviour
{
    [SerializeField] private MailInfo requiredMailInfo;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = requiredMailInfo.Color;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Mail"))
        {
            return;
        }

        var mail = collision.GetComponent<Mail>();

        if(!mail || mail.MailInfo.MailColor != requiredMailInfo.MailColor)
        {
            //Throw wrong color warning

            return;
        }

        Destroy(mail.gameObject);

        //IncreasePoint
        ScoreManager.Instance.IncreaseScore(1);
    }
}
