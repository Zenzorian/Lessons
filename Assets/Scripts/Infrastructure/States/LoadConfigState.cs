using Scripts.ScriptableObjects;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadConfigState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigDataService _configDataService;
       
        public LoadConfigState(GameStateMachine gameStateMachine, IConfigDataService configDataService)
        {
            _gameStateMachine = gameStateMachine;           
            _configDataService = configDataService;
        }

        public void Enter()
        {
            Debug.Log("Load ConfigState");    
            
            _configDataService.Load();
            
            var characterConfig = _configDataService.GetCharacterConfig();
            var soundConfig = _configDataService.GetSoundConfig();

            ConfigData configData = new ConfigData(characterConfig, soundConfig);
            
            _gameStateMachine.Enter<MainMenuState, ConfigData>(configData);
        }

        public void Exit()
        {

        }  
    }
    public class ConfigData
    {
        public CharacterConfig characterConfig;
        public SoundConfig soundConfig;
        public ConfigData(CharacterConfig characterConfig, SoundConfig soundConfig)
        {
            this.characterConfig = characterConfig;
            this.soundConfig = soundConfig;
        }
    }
}