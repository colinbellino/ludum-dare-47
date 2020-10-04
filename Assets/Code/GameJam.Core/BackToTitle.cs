using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTitle : MonoBehaviour
{
	public void Onclick()
    {
	    GameEvents.BackToTitle?.Invoke();
    }
}
