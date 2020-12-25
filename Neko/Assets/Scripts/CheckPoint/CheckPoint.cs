using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool flag;
    public bool Flag { get => flag; private set => flag = value; }

    // このチェックポイントを通っていた時死んだら戻すswitch
    private SwitchObject[] switchObjects;

    private UncertainObject[] uncertainObjects;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;

        switchObjects = GetComponentsInChildren<SwitchObject>();

        uncertainObjects = GetComponentsInChildren<UncertainObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; }

        var playerState = other.gameObject.GetComponent<PlayerController>().State;
        if (playerState == PlayerState.Hurt || playerState == PlayerState.Dead)
            return;

        flag = true;
        Debug.Log("CheckPoint : " + name);
    }

    /// <summary>
    /// スイッチを押す前に戻す
    /// </summary>
    public void Reload()
    {
        // 子オブジェクトのswitchをすべて戻す
        foreach(var s in switchObjects)
        {
            s.action_off();
        }

        foreach(var u in uncertainObjects)
        {
            u.ResetObject();
        }
    }
}
