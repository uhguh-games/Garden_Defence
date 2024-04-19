using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTestButtons : MonoBehaviour
{
    private Button button;
    //public ButtonType type;
    //private ButtonType typeButton;
 
    //public void Press()
    //{
    //    switch (typeButton)
    //    {
    //        case ButtonType.Music:
    //            AudioManager.instance.PlayMusic("TestMusic");
    //            break;
    //        case ButtonType.TowerBuild:
    //            AudioManager.instance.PlaySFX("TowerBuild");
    //            break;
    //        case ButtonType.SelectTower:
    //            AudioManager.instance.PlaySFX("SelectTower");
    //            break;
    //        case ButtonType.SelectUI:
    //            AudioManager.instance.PlaySFX("SelectUI");
    //            break;
    //        case ButtonType.JunkCollect:
    //            AudioManager.instance.PlaySFX("JunkCollect");
    //            break;
    //        case ButtonType.EatenCarrot:
    //            AudioManager.instance.PlaySFX("EatenCarrot");
    //            break;
    //        case ButtonType.BackUI:
    //            AudioManager.instance.PlaySFX("BackUI");
    //            break;
    //        default:
    //            Debug.Log("Agfawurqiweheroiqw3ujroi2ujeoi2reioruqw3iur");
    //            break;
    //    }
        
    //}
    public void PlayMusic()
    {
        AudioManager.instance.PlayMusic("TestMusic");
    }
    public void PlayTowerBuild()
    {
        AudioManager.instance.PlaySFX("TowerBuild");
    }
    public void PlaySelectTower()
    {
        AudioManager.instance.PlaySFX("SelectTower");
    }
    public void PlaySelectUI() 
    {
        AudioManager.instance.PlaySFX("SelectUI");
    }
    public void PlayJunkCollect()
    {
        AudioManager.instance.PlaySFX("JunkCollect");
    }
    public void PlayEatenCarrot()
    {
        AudioManager.instance.PlaySFX("EatenCarrot");
    }
    public void PlayBackUI()
    {
        AudioManager.instance.PlaySFX("BackUI");
    }

}

//public enum ButtonType
//{
//    Music,
//    TowerBuild,
//    SelectTower,
//    SelectUI,
//    JunkCollect,
//    EatenCarrot,
//    BackUI
//}