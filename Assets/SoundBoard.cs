using UnityEngine;

public class SoundBoard : MonoBehaviour
{
    public static SoundBoard Instance { get; private set; }

    public AudioSource buttonClick;
    public AudioSource spawnObjectSmall;
    public AudioSource spawnObjectLarge;
    public AudioSource addAttribute;
    public AudioSource textType;

    private void Awake()
    {
        if (Instance)
        {
            Debug.Log("Found existing SoundBoard. Destroying copy.");
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
