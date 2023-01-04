using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MailColor
{
    Green,
    Orange,
    Black
}

[CreateAssetMenu(fileName = "Mail", menuName = "ScriptableObjects/NewMail", order = 1)]
public class MailInfo : ScriptableObject
{
    public MailColor MailColor;
    public Color Color;
}
