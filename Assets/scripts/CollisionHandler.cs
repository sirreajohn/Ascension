using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("collided a finish obj");    
                break;

            case "obstacle":
                Debug.Log("reducing HP");
                break;

            case "friendly":
                Debug.Log("bumped into a friendly");
                break;

            default:
                Debug.Log("not cool fam.");
                break;
        }

    }

}
