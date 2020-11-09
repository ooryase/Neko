using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerFollow playerFollow;
    [SerializeField] private Transition transition = null;

    private CheckPoint[] checkPoints;
    private AudioSource se_dead;

    // Start is called before the first frame update
    void Start()
    {
        checkPoints = GetComponentsInChildren<CheckPoint>();
        se_dead = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerControll>().GetState() == State.Dead)
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
                        playerFollow.Follow();
                        transition.FadeIn_Dead();
                        se_dead.Play();

                        player.GetComponent<PlayerControll>().ChangeState(State.Nuetral);
                        break;
                    }
                }
            }

            // チェックポイントを通ってなかったら初めから（使わないかも）
            if (flag == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
