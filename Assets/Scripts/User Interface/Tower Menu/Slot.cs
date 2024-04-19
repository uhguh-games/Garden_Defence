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
   private int valueAmount; // set by economy manager
   private EconomyManager economyManager;

   private void Start() 
   {
      economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
      SetStats();
   }
 
   private void SetStats() 
   {
      icon.texture = ItemInSlot.icon;
    
      prefab = ItemInSlot.prefab;

      if (ItemInSlot.itemName == "Junk") // if item is junk economy manager defines the value
      {
         valueAmount = economyManager.GetItemValue(ItemInSlot.itemName);
      } 
      else // if item is something else e.g. health or gold the sriptable object defines the value
      {
         valueAmount = ItemInSlot.value;
      }

      nameText.text = $"{ItemInSlot.itemName}";
      valueText.text = $"{valueAmount}";
   }
}
