using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
