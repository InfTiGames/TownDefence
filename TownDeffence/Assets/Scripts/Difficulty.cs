using UnityEngine;
using UnityEngine.UI;
public class Difficulty : MonoBehaviour
{
    public int difficulty;
    GameManager _gameManager;
    Button _btn;    

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(SetDifficulty);               
    }
    void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }
}