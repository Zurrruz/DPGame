using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardForVictory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _rewardLow;
    [SerializeField]
    private List<GameObject> _rewarMedium;
    [SerializeField]
    private List<GameObject> _rewarHard;

    [SerializeField]
    private List<Transform> _spawnReward;

    private void Start()
    {
        
    }

    public void ShowRewardLow(bool low, bool medium, bool hard)
    {
        for (int i = 0; i < _spawnReward.Count; i++)
        {
            if (low)
            {
                int r = Random.Range(0, _rewardLow.Count);
                Instantiate(_rewardLow[r], _spawnReward[i]);
            }
            else if(medium)
            {
                int r = Random.Range(0, _rewarMedium.Count);
                Instantiate(_rewarMedium[r], _spawnReward[i]);
            }
            else if(hard)
            {
                int r = Random.Range(0, _rewarHard.Count);
                Instantiate(_rewarHard[r], _spawnReward[i]);
            }
        }
    }

}
