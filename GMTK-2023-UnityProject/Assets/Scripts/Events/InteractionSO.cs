using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Interaction", menuName = "Event/New Interaction")]
public class InteractionSO : ScriptableObject
{
    [SerializeField] public string Title;
    [SerializeField, TextArea] public string Description;
    [SerializeField] public PlayerReactionSO[] PlayerReactions;
}