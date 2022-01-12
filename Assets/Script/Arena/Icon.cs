using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    private QueueManager queueManager;

    private void Awake()
    {
        queueManager = GameObject.FindGameObjectWithTag("QueueManager").GetComponent<QueueManager>();
    }
    void Start()
    {
        queueManager.icon.Add(gameObject);
    }

    public void Deleteicon()
    {
        Destroy(gameObject);
    }

   
}
