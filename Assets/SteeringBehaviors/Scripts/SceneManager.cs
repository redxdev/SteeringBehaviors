using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    public void ChangeScene(int scene)
    {
        Application.LoadLevel(scene);
        Application.ExternalCall("sceneLoaded", "ok");
    }
}
