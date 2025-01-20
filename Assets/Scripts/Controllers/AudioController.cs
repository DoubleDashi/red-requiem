using UnityEngine;

namespace Controllers
{
    public abstract class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSourcePrefab;

        protected void PlayAudio(AudioClip[] clips, float pitch = 1f, float volume = 1f)
        {
            int index = Random.Range(0, clips.Length);
            
            PlayAudio(clips[index], pitch, volume);
        }
        
        protected void PlayAudio(AudioClip clip, float pitch = 1f, float volume = 1f)
        {
            AudioSource instance = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            instance.clip = clip;
            instance.pitch = pitch;
            instance.volume = volume;
            
            instance.Play();
            Destroy(instance.gameObject, instance.clip.length);
        }
    }
}