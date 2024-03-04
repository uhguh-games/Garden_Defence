using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
   [HideInInspector] public Slot slot;
   public Item ItemInSlot;
   public RawImage icon;
   public TextMeshProUGUI valueText;
   public GameObject prefab;
   public TextMeshProUGUI nameText;
   public int valueAmount;

   private void Start() 
   {
      SetStats();
   }
 
   private void SetStats() 
   {
      icon.texture = ItemInSlot.icon;
      valueAmount = ItemInSlot.value;
      prefab = ItemInSlot.prefab;
      nameText.text = $"{ItemInSlot.itemName}";
      valueText.text = $"{valueAmount}";
   }
}
