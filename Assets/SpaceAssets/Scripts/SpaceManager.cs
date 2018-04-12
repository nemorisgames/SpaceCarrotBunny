using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollDirection { LeftToRight, RightToLeft, DownToUp, UpToDown };

public class SpaceManager : MonoBehaviour {
    //Set the direction that the screen or the camera is moving
    public ScrollDirection scrollDirection = ScrollDirection.LeftToRight;

    public static SpaceManager instance = null;

    void Start () {
        instance = this;
    }
	
	void Update () {
		
	}
}
