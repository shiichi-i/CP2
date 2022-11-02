using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public int level;
    public GameObject popUp;
    public Sprite[] sprites;
    public string[] sentences;
    public int indxx;

    public GameObject img;
    public GameObject badg;
    public Text txt;

    public GameObject Exit;

    void Start(){
        indxx = 0;
        ShowPop();
        badg.SetActive(false);
        Exit.SetActive(false);
        img.SetActive(true);
    }

    public void ShowPop(){
        img.GetComponent<Image>().sprite = sprites[indxx];
        txt.text = sentences[indxx];
        popUp.SetActive(true);
        if(indxx >= sprites.Length-1){
            EndExit();
        }
    }

    public void HidePop(){
        popUp.SetActive(false);
        indxx++;
    }

    void EndExit(){
        badg.SetActive(true);
        Exit.SetActive(true);
        img.SetActive(false);
        if(level == 1){
            StateController.level1 = true;
        }else if(level == 2){
            StateController.level2 = true;
        }else if(level == 3){
            StateController.level3 = true;
        }
        
    }
}
