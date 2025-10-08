using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        public List<Sound> Sounds = new List<Sound>();
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0.5f, 2f)] public float pitch;

        [HideInInspector] public AudioSource source;
    }
}