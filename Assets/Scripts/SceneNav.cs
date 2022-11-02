using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public string redirect;
    public bool navload;

    public void Nav()
    {
        StateController.load = navload;
        SceneManager.LoadScene(redirect);
    }
}
