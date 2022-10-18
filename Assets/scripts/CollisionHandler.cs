using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    PlayerHealth playerHealth_script;
    Movement movement_script;
    [SerializeField] float reduction_on_bump = 50f;
    [SerializeField] float reload_delay_time = 2f;
    
    private void Start() 
    {
        playerHealth_script = FindObjectOfType<PlayerHealth>();
        movement_script = FindObjectOfType<Movement>();
    }

    void reduce_health()
    {
        bool is_dead = playerHealth_script.reduceHealth(reduction_on_bump);
        if (is_dead)
            start_crash_seq();
    }

    void load_same_scene()
    {
        load_scene(isnext: 0);
    }

    void load_next_scene()
    {
        load_scene(isnext: 1);
    }
    void start_crash_seq()
    {
        // movement_script.disable_audio();
        // add explosion after crash 
        // add particle FX after crash
        movement_script.enabled = false;
        Invoke("load_same_scene", reload_delay_time);
    }

    void start_success_seq()
    {
        // movement_script.disable_audio();
        // add explosion after crash 
        // add particle FX after crash
        movement_script.enabled = false;
        Invoke("load_next_scene", reload_delay_time);

    }

    void load_scene(int isnext = 1)
    {        
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + isnext;
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
                start_success_seq();
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
                // movement_script.sound_explode_play(); // delayed sound, fix this
                start_crash_seq();
                break;
        }

    }

}
