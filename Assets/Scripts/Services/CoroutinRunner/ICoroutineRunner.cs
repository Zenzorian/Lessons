﻿using System.Collections;
using UnityEngine;

namespace Scripts.Services
{
  public interface ICoroutineRunner 
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine coroutine);
  }
}