using UnityEngine;

public class SongPlayer : MonoBehaviour 
{
    [SerializeField] private string _songName;
    private void Awake()
    {
        AudioManager.instance.PlayMusic(this._songName);
    }
}
