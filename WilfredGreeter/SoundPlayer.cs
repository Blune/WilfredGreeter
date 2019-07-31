using System;
using System.Media;

namespace WilfredGreeter
{
    public class SoundPlayer
    {
        private readonly Action _playSound;

        public SoundPlayer(string soundPath)
        {
            if (string.IsNullOrEmpty(soundPath))
            {
                _playSound = PlaySystemSound;
            }
            else
            {
                _playSound = () => PlayCustomSound(soundPath);
            }
        }

        public void PlaySound() => _playSound();

        private void PlaySystemSound() => SystemSounds.Hand.Play();

        private void PlayCustomSound(string path) => new System.Media.SoundPlayer(path).Play();
    }
}
