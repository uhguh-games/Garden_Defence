using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soundbank", menuName = "Create new soundbank")]
public class Soundbank : ScriptableObject
{
    public AudioClip[] soundbankArray;
}
