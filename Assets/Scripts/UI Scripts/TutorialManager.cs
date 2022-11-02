using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Help;
    private int HelpIndex;
    bool Done = false;
    bool Done2 = false;

    void Update()
    {
        for (int i = 0; i < Help.Length; i++)
        {
            if (i != HelpIndex)
            {
                Help[i].SetActive(false);
            }

            if (Done == false && Done2 == false)
            {
                Help[i].SetActive(i == HelpIndex);
            }
            else if (Done == true && Done2 == false)
            {
                Help[i].SetActive(false);
            }

            if (Application.loadedLevelName == "Level1")
            {
                lvl1();
            } else if(Application.loadedLevelName == "Level2")
            {
                lvl2();
            }

        }
    }
    public void OnButtonPress()
    {
        Help[HelpIndex].SetActive(false);
        Done = true;
    }

    public void testButton()
    {
        Done2 = true;
    }

    //conditions
    void lvl1()
    {
        if (HelpIndex == 0)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 1)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 2)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 3)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 4)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 5)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 6)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 6)
        {
            Debug.Log("You have finished the tutorial!");
        }
    }void lvl2()
    {
        if (HelpIndex == 0)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 1)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 2)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 3)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 4)
        {
            if (Done == true && Done2 == true)
            {
                Done = false;
                Done2 = false;
                HelpIndex++;
            }
        }
        else if (HelpIndex == 5)
        {
            Debug.Log("You have finished the tutorial!");
        }
    }
}

