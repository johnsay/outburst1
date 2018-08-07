
using UnityEngine;

namespace FoundationFramework.Audio
{
    public class EffectSoundTrigger : MonoBehaviour
    {
        [SerializeField] private bool _playOnStart;

        [SerializeField] private SoundConfig _sound;


        private int _soundId = -1;

        private void Start()
        {
            if(_playOnStart) TriggerSound();
        }

        public void TriggerSound()
        {
            _soundId = EffectSoundPlayer.PlayEffectSound(_sound.name);
        }

        public void StopEffectSound()
        {
            if(_soundId != -1)
                EffectSoundPlayer.StopEffectSound(_soundId); 
        }

    }
}
