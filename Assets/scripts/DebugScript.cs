
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{

    CollisionHandler collision_script;
    Movement_v2 movement_script;

    // Update is called once per frame
    private void Start() 
    {
        collision_script = GetComponent<CollisionHandler>();
        movement_script = GetComponent<Movement_v2>();
    }
    
    public void debug_god_mode()
    {
        Debug.Log("toggle god mode");
        debug_toggle_collision();
        debug_toggle_infinite_fuel();
    }
    public void debug_toggle_infinite_fuel()
    {
        Debug.Log("infinite fuel");
        movement_script.toggle_infinite_fuel();
    }

    public void debug_toggle_collision()
    {
        Debug.Log("toggle collison");
        collision_script.toggle_collision();
    }

    public static void LoadNextScene()
    {
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;
        int max_scenes = SceneManager.sceneCountInBuildSettings;

        if (next_scene_index >= max_scenes)
            next_scene_index = 0;

        SceneManager.LoadScene(next_scene_index);
    }
}
