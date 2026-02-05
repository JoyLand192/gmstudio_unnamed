using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerableMonster
{
    int AlertState { get; set; }
    event System.Action OnTriggeredPlayer;
}
