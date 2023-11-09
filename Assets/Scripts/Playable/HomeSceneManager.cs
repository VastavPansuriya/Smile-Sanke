using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.Enum;
using Snake.SceneLoad;

public class HomeSceneManager : MonoBehaviour
{
   
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayEffect(SoundType.UI);
            SceneLoadManager.LoadScene(LoadScene.Game);            
        }   
    }
}
