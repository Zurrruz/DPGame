using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _box;
    [SerializeField]
    private Transform _enemy;
    [SerializeField]
    private Transform _store;

    private Transform _target;

    private Vector2 _startPos;
    private Camera _cam;

    [SerializeField]
    private float _speed;

    public static bool inBoxRoom;
    public static bool inArenaRoom;
    public static bool inStore;

    void Start()
    {
        _target = _player;
        _cam = GetComponent<Camera>();
        OpenBox.cameraPlayer += PositionPlayer;
        transform.position = new Vector3(0, 2.7f, transform.position.z);
    }
    private void OnDestroy()
    {
        OpenBox.cameraPlayer -= PositionPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isMove)
        {
            if (_player.position.y > transform.position.y + 3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _player.position.y, transform.position.z), _speed * Time.deltaTime);
            }
        }
        if (!inBoxRoom && !inArenaRoom && !inStore && !GameManager.isMove)
        {
            if (Input.GetMouseButtonDown(0))
                _startPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            else if (Input.GetMouseButton(0))
            {
                float pos = _cam.ScreenToWorldPoint(Input.mousePosition).y - _startPos.y;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - pos, 2.6f, 45.97f), transform.position.z);
            }
        }
    }

    public void OpenChest()
    {
        transform.position = _box.position;
        inBoxRoom = true;
    }

    public void GoToStore()
    {
        transform.position = _store.position;
        inStore = true;
    }

    public void ToTheArena()
    {
        transform.position = _enemy.position;
        inArenaRoom = true;
    }

    public void PositionPlayer()
    {
        StartCoroutine(PosPlayer());
    }
    IEnumerator PosPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        if (_player.position.y > 2f)
            transform.position = new Vector3(0, _player.position.y, transform.position.z);
        else if (_player.position.y < 1.35f)
            transform.position = new Vector3(0, 2.6f, transform.position.z);
    }
}

