using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
	    GameEvents.StartGame?.Invoke();
    }

    public void QuitGame()
    {
	    GameEvents.QuitGame?.Invoke();
    }
}
