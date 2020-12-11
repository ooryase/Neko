using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    private PlayerController playerController;
    [SerializeField] private PlayerFollow playerFollow = null;
    [SerializeField] private Transition transition = null;

    private CheckPoint[] checkPoints;

    private bool deadReset = false;

    /// <summary>
    /// メインカメラに付いてるPlayerFollow
    /// 死亡時のリセット時にこれいらん気がしてきた
    /// </summary>
    private PlayerFollow camerasPlayerFollow;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        checkPoints = GetComponentsInChildren<CheckPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.State == PlayerState.Dead)
        {
            if(deadReset == false)
            {
                StartCoroutine(PlayerReborn());
                deadReset = true;
            }
        }
    }

    private IEnumerator PlayerReborn()
    {

        transition.FadeIn_Dead(2.0f);
        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(0.3f);

        Time.timeScale = 1.0f;


        transition.FadeOut(2.0f);
        yield return new WaitForSeconds(1.66f);

        bool flag = false;

        if (checkPoints.Length != 0)
        {
            for (int i = checkPoints.Length - 1; i >= 0; i--)
            {
                // 終わりの方のチェックポイントからフラグを見る
                if (checkPoints[i].Flag)
                {
                    flag = true;

                    // プレイヤーをチェックポイントまで戻す
                    player.transform.position = checkPoints[i].transform.position;
                    playerController.ResetStatus();

                    // カメラも持ってくる
                    playerFollow.Follow();
                    transition.FadeIn();

                    // チェックポイントの中のスイッチを元の状態に戻す
                    checkPoints[i].Reload();

                    deadReset = false;

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
