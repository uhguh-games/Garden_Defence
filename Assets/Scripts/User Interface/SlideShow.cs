using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI pageText;
    [SerializeField] RawImage tutorialImage;
    [SerializeField] int[] page = new int[] {1, 2, 3, 4};
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] List <string> tutorialTexts = new List<string>();
    [SerializeField] List <Texture> tutorialSprites = new List<Texture>();
    [SerializeField] int currentIndex = 0;

    void Start()
    {
        PopulateTexts();
        PopulateImages();

        tutorialText.text = tutorialTexts[currentIndex];
        tutorialImage.texture = tutorialSprites[currentIndex];
    }

    public void NextPage() 
    {
        if (currentIndex == 3) 
        {
            currentIndex = 0;
            tutorialText.text = tutorialTexts[currentIndex];
            tutorialImage.texture = tutorialSprites[currentIndex];
            pageText.text = $"{currentIndex + 1}/{tutorialTexts.Count}";
        }
        else if (currentIndex < tutorialTexts.Count) 
        {
            currentIndex++;
            tutorialText.text = tutorialTexts[currentIndex];
            tutorialImage.texture = tutorialSprites[currentIndex];
            pageText.text = $"{currentIndex + 1}/{tutorialTexts.Count}";
        }

    }

    public void PreviousPage() 
    {
        if (currentIndex == 0) 
        {
            currentIndex = 3;
            tutorialText.text = tutorialTexts[currentIndex];
            tutorialImage.texture = tutorialSprites[currentIndex];
            pageText.text = $"{currentIndex + 1}/{tutorialTexts.Count}";
        }
        else if (currentIndex < tutorialTexts.Count) 
        {
            currentIndex--;
            tutorialText.text = tutorialTexts[currentIndex];
            tutorialImage.texture = tutorialSprites[currentIndex];
            pageText.text = $"{currentIndex + 1}/{tutorialTexts.Count}";
        }
    }

    void PopulateTexts() 
    {
        string page1 = "Save the crops! \n \n \n \n \n \n Attack bugs by dragging towers onto the map.";
        string page2 = "Bugs drop scrap when they are hit. \n \n \n \n \n Tap to pick it up.";
        string page3 = "During the night, towers can't see! \n \n \n \n \n Place bonfires to help out.";
        string page4 = "Use scrap to relight bonfires.";

        tutorialTexts.Add(page1);
        tutorialTexts.Add(page2);
        tutorialTexts.Add(page3);
        tutorialTexts.Add(page4);
    }

    void PopulateImages() 
    {
        Texture sprite1 = Resources.Load("Sprites/Tutorial_0") as Texture;
        Texture sprite2 = Resources.Load("Sprites/Tutorial_1") as Texture;
        Texture sprite3 = Resources.Load("Sprites/Tutorial_2") as Texture;
        Texture sprite4 = Resources.Load("Sprites/Tutorial_3") as Texture;

        tutorialSprites.Add(sprite1);
        tutorialSprites.Add(sprite2);
        tutorialSprites.Add(sprite3);
        tutorialSprites.Add(sprite4);
    }
}
