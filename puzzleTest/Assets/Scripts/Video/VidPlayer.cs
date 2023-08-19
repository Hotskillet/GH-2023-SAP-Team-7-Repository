using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    [SerializeField] string videoFileName;
   
   
    void Start()
    {
        PlayVideo();
    }

    public void PlayVideo() 
    {

            VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

            if (videoPlayer) 
            {
                string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
                Debug.Log(videoPath);
                videoPlayer.url = videoPath;
                videoPlayer.Play();
            }
            videoPlayer.loopPointReached += FinishVideo; 
    }
    private void FinishVideo(VideoPlayer SP) 
    {
        Debug.Log("b");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
