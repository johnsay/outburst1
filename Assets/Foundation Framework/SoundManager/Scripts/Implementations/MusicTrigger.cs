
using UnityEngine;

namespace FoundationFramework.Audio
{
    public class MusicTrigger : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart;

        [SerializeField] private SoundConfig _sound;

    

        private void Start()
        {
            if(_playOnStart) TriggerSound();
        }

        public void TriggerSound()
        {
            MusicSoundPlayer.PlayMusic(_sound.name);
        }
    }
}
