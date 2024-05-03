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
        AudioManager.instance.PlayMusic(1);
    }
    public void PlayTowerBuild()
    {
        AudioManager.instance.PlaySFX(2);
    }
    public void PlaySelectTower()
    {
        AudioManager.instance.PlaySFX(3);
    }
    public void PlaySelectUI() 
    {
        AudioManager.instance.PlaySFX(4);
    }
    public void PlayJunkCollect()
    {
        AudioManager.instance.PlaySFX(5);
    }
    public void PlayEatenCarrot()
    {
        AudioManager.instance.PlaySFX(6);
    }
    public void PlayBackUI()
    {
        AudioManager.instance.PlaySFX(7);
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