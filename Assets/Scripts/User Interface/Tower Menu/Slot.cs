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
      // valueAmount = ItemInSlot.value;
      valueAmount = economyManager.GetItemValue(ItemInSlot.itemName);
      prefab = ItemInSlot.prefab;
      nameText.text = $"{ItemInSlot.itemName}";
      valueText.text = $"{valueAmount}";
   }
}
