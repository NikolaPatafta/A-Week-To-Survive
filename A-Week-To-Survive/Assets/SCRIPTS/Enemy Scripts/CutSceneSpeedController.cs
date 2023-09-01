using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class CutSceneSpeedController : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    private void OnEnable()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0.5f);
    }

    private void OnDisable()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1f);
    }

}
