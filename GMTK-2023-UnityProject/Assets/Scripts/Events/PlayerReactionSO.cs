using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPlayerReaction", menuName = "Event/New PlayerReaction")]
public class PlayerReactionSO : ScriptableObject
{
    [SerializeField] public string Title;
    [SerializeField, TextArea] public string Description;
    [SerializeField] public EVENTTYPE Eventtype;
}