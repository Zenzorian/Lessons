using System.Collections;
using UnityEngine;
using Scripts.Services; 

namespace Scripts.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;
   
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _currentAnimation;

    public void Construct(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;  
    }
    
    public void Show()
    {
      gameObject.SetActive(true);      
      Curtain.alpha = 1;
      if(_currentAnimation !=null)
        _coroutineRunner.StopCoroutine(_currentAnimation);
    }

    public void Hide()
    {
      if (_currentAnimation != null)
        _coroutineRunner.StopCoroutine(_currentAnimation);
        
      _currentAnimation = _coroutineRunner.StartCoroutine(DoFadeIn());
    }
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(0.03f);
      }
      
      gameObject.SetActive(false);
    }
    
  }
}