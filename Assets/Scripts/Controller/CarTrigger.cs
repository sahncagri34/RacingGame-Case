using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    public const string END_OF_ROAD_TAG = "EndOfRoadPoint";

    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag.Equals(END_OF_ROAD_TAG))
            GameController.Instance.FinishTheGame();
    }
}
