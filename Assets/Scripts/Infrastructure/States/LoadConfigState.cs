using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadConfigState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
       
        public LoadConfigState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;           
        }

        public void Enter()
        {
            Debug.Log("Load ConfigState");    
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {

        }  
    }
}