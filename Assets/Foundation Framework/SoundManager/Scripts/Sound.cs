using UnityEngine;

namespace FoundationFramework.Audio
{
    public class Sound
    {
        public SoundConfig SoundConfig;
        public AudioSource AudioSource;
        public AudioSourcePool AudioSourcePool;
        
        public Sound(SoundConfig soundinfo, AudioSource audioSource, AudioSourcePool audioSourcePool)
        {
            SoundConfig = soundinfo;
            AudioSource = audioSource;
            AudioSourcePool = audioSourcePool;
        }

        public void DespawnAudioSource()
        {
            AudioSourcePool.Despawn(AudioSource);
        }
    }
}