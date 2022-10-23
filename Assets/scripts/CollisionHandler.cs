using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    PlayerHealth playerHealth_script;
    Movement movement_script;
    AudioSource audio_player;

    bool not_crashed = true;
    [SerializeField] float reduction_on_bump = 50f;
    [SerializeField] float reload_delay_time = 2f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip next_level;

    [SerializeField] ParticleSystem crash_particles;
    [SerializeField] ParticleSystem success_particles;

    private void Start() 
    {
        playerHealth_script = FindObjectOfType<PlayerHealth>();
        movement_script = FindObjectOfType<Movement>();
        audio_player = GetComponent<AudioSource>();

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
    void disable_movement()
    {
        if(movement_script.enabled)
            movement_script.enabled = false;
    }

    void start_crash_seq()
    {
        audio_player.Stop();
        audio_player.PlayOneShot(explosion);
        crash_particles.Play();
        disable_movement();
        Invoke("load_same_scene", reload_delay_time);
    }

    void start_success_seq()
    {
        audio_player.PlayOneShot(next_level);
        success_particles.Play();
        disable_movement();
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
        if (not_crashed)
        {
            not_crashed = false;
            switch(other.gameObject.tag)
            {
                case "Finish":
                    start_success_seq();
                    break;

                case "obstacle":
                    Debug.Log("reducing HP");
                    reduce_health();
                    not_crashed = true;
                    break;

                case "friendly":
                    Debug.Log("bumped into a friendly");
                    not_crashed = true;
                    break;

                default:
                    Debug.Log("not cool fam.");
                    start_crash_seq();
                    break;
            }
        }

    }

}
