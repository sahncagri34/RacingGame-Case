using UnityEngine;

[System.Serializable]
public class Timer
{
    private float ElapsedTime;

   
    public void Run()
    {
        ElapsedTime += Time.deltaTime;
        GameController.Instance.ShowElapsedTime(ElapsedTime);
    }
}