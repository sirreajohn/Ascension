using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{  

    PlayerHealth playerHealth_script;
    [SerializeField] float reduction_on_bump = 50f;
    
    private void Start() 
    {
        playerHealth_script = FindObjectOfType<PlayerHealth>();
    }

    void reduce_health()
    {
        bool is_dead = playerHealth_script.reduceHealth(reduction_on_bump);
        if (is_dead)
        {
            int current_scene_index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current_scene_index);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("collided a finish obj");    
                break;

            case "obstacle":
                Debug.Log("reducing HP");
                reduce_health();
                break;

            case "friendly":
                Debug.Log("bumped into a friendly");
                break;

            default:
                Debug.Log("not cool fam.");
                SceneManager.LoadScene("SampleScene");
                break;
        }

    }

}
