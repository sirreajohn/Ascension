using UnityEngine;

<<<<<<< Updated upstream
public class CollisionHandler : MonoBehaviour
{
=======
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
            reload_scene();
    }
    void reload_scene()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_index);
    }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
                reload_scene();
>>>>>>> Stashed changes
                break;
        }

    }

}
