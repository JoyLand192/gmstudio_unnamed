using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Watcher : MonoBehaviour, ITriggerableMonster
{
    int alertState;
    public int AlertState
    {
        get => alertState;
        set
        {
            alertState = value;
            Debug.Log($"몬스터의 인식 단계가 {value}로 설정되었습니다");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ...
            OnTriggeredPlayer?.Invoke();
            Debug.Log($"플레이어와 충돌");
        }
    }
    public event System.Action OnTriggeredPlayer;
}