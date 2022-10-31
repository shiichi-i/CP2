using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNav : MonoBehaviour
{
    public string redirect;

    public void Nav()
    {
        SceneManager.LoadScene(redirect);
    }
}
