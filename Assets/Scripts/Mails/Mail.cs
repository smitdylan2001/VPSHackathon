using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public MailInfo MailInfo;

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
