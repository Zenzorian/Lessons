using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Services
{
    public class ConfigDataService : IConfigDataService
    {
        private const string CHARACTER_CONFIG_PATH = "Configs/CharacterConfig";
        private const string SOUND_CONFIG_PATH  = "Configs/SoundConfig";
             
        private CharacterConfig _characterConfig;
        private SoundConfig _soundConfig;

        public void Load()
        {
            _characterConfig = Resources
                .Load<CharacterConfig>(CHARACTER_CONFIG_PATH);   
            
            _soundConfig =  Resources
                .Load<SoundConfig>(SOUND_CONFIG_PATH);   
        }
        
        public CharacterConfig GetCharacterConfig() => _characterConfig;
        
        public SoundConfig  GetSoundConfig() => _soundConfig;
    }
}