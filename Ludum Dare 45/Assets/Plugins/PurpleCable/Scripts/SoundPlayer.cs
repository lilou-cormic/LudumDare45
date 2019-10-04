using UnityEngine;

namespace PurpleCable
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        public static SoundPlayer Instance { get; private set; }

        private AudioSource _audioSource;

        public static float Volume { get => Instance?._audioSource?.volume ?? 1; set => Instance._audioSource.volume = value; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", _audioSource.volume);

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void Play(AudioClip clip)
        {
            if (clip != null)
                Instance._audioSource.PlayOneShot(clip);
        }
    }
}
