using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    private CheckPoint[] checkPoints;

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = GetComponentsInChildren<CheckPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().State == PlayerState.Dead)
        {
            bool flag = false;

            if(checkPoints.Length != 0)
            {
                for (int i = checkPoints.Length - 1; i >= 0; i--)
                {
                    // 終わりの方のチェックポイントからフラグを見る
                    if (checkPoints[i].GetFlag())
                    {
                        flag = true;
                        player.transform.position = checkPoints[i].transform.position;
                        player.GetComponent<PlayerController>().ChangeState(PlayerState.Nuetral);
                        break;
                    }
                }
            }

            // チェックポイントを通ってなかったら初めから
            if (flag == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
