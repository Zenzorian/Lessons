using Scripts.ScriptableObjects;

namespace Scripts.Services
{
    public interface IConfigDataService
    {
        void Load();
        
        CharacterConfig GetCharacterConfig(); 
        SoundConfig GetSoundConfig();
    }
}