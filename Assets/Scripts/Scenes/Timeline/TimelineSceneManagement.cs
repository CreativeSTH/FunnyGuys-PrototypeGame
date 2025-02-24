using UnityEngine;

public class TimelineSceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         AudioManager.Instance.PlayMusic("timeLineMusic");
    }

    public void SkipScene(){
        GameManager.Instance.LoadNextScene();
    }
}
