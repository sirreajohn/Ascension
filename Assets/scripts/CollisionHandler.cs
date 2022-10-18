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
            reload_scene();
    }
    void reload_scene()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene_index);
    }
    void load_next_scene()
    {
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;
        int max_scenes = SceneManager.sceneCountInBuildSettings;

        if (next_scene_index >= max_scenes)
            next_scene_index = 0;
        
        SceneManager.LoadScene(next_scene_index);
    }

    private void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                load_next_scene();
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
                reload_scene();
                break;
        }

    }

}
