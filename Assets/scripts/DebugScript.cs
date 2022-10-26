
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{

    CollisionHandler collision_script;
    Movement movement_script;

    // Update is called once per frame
    private void Start() 
    {
        collision_script = GetComponent<CollisionHandler>();
        movement_script = GetComponent<Movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("loading next level");
            LoadNextScene();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            debug_toggle_collision();
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            debug_toggle_infinite_fuel();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            debug_god_mode();
        }
    }

    private void debug_god_mode()
    {
        Debug.Log("toggle god mode");
        debug_toggle_collision();
        debug_toggle_infinite_fuel();
    }
    private void debug_toggle_infinite_fuel()
    {
        Debug.Log("infinite fuel");
        movement_script.toggle_infinite_fuel();
    }

    private void debug_toggle_collision()
    {
        Debug.Log("toggle collison");
        collision_script.toggle_collision();
    }

    private static void LoadNextScene()
    {
        int next_scene_index = SceneManager.GetActiveScene().buildIndex + 1;
        int max_scenes = SceneManager.sceneCountInBuildSettings;

        if (next_scene_index >= max_scenes)
            next_scene_index = 0;

        SceneManager.LoadScene(next_scene_index);
    }
}
