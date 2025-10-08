using Scripts.Infrastructure.States;
using Scripts.Logic;
using UnityEngine;
using Scripts.Services;
using Scripts.Infrastructure.AssetManagement;
using Zenject;

public class BootstrappInstaller : MonoInstaller
{
    [SerializeField] private LoadingCurtain _curtainPrefab;
        
    private ISceneLoaderService _sceneLoaderService;
    private ICoroutineRunner _coroutineRunner;
       
    public override void InstallBindings()
    {
        Debug.Log("Game Started");
           
        RegisterServices();
           
        CreateGameStateMachine();
    }
        
    private void RegisterServices()
    {
        BindCoroutineRunner();      
        BindSceneLoaderService();
        BindConfigDataService();
        BindAssetProvider();
        BindInputService();       
        BindGameFactoryService(); 
    }        
      
    private void BindCoroutineRunner()
    {            
        var coroutineRunnerObject = new GameObject("Coroutine Runner");
        GameObject.DontDestroyOnLoad(coroutineRunnerObject);
           
        _coroutineRunner = coroutineRunnerObject.AddComponent<CoroutineRunner>();
            
        Container
            .Bind<ICoroutineRunner>()
            .FromInstance(_coroutineRunner)
            .AsSingle()
            .NonLazy();            
    }
       
    private void BindSceneLoaderService()
    {            
        var loaderCurtain = Instantiate(_curtainPrefab);
        loaderCurtain.Construct(_coroutineRunner);
        GameObject.DontDestroyOnLoad(loaderCurtain);
            
        _sceneLoaderService = new SceneLoaderService(_coroutineRunner, loaderCurtain);
            
        Container
            .Bind<ISceneLoaderService>()
            .FromInstance(_sceneLoaderService)
            .AsSingle()
            .NonLazy();
    }

    private void BindConfigDataService()
    {
        Container
            .BindInterfacesAndSelfTo<ConfigDataService>()
            .AsSingle()
            .NonLazy();
        
    }
    
    private void BindAssetProvider()
    {
        Container
            .BindInterfacesAndSelfTo<AssetProvider>()
            .AsSingle()
            .NonLazy();
            
        // Clean up any previous asset references
        Container
            .Resolve<AssetProvider>()
            .Cleanup();

        // Initialize the asset system
        Container
            .Resolve<AssetProvider>()
            .Initialize();
    }
    
    private void BindInputService()
    {
        Container
            .BindInterfacesAndSelfTo<InputManagerService>()
            .AsSingle()
            .NonLazy();
    } 
   
    private void BindGameFactoryService()
    {
        Container
            .BindInterfacesAndSelfTo<GameFactoryService>()
            .AsSingle()
            .NonLazy();
    }       

    private void CreateGameStateMachine()
    { 
        _sceneLoaderService.ShowLoadingCurtain();
            
        Container
            .Bind<GameStateMachine>()  
            .AsSingle()
            .NonLazy();          
    }
}