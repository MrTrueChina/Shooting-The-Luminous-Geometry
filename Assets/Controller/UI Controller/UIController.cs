using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameController _gameController;

    [SerializeField]
    GameObject _mainMenuCanvas;
    [SerializeField]
    GameObject _selectLevelCanvas;
    [SerializeField]
    GameObject _restartCanvas;
    [SerializeField]
    GameObject _pauseCanvas;



    public void PlayButtonDown()
    {
        //此处应该接选关，但选关还不完善，接开始
        StartGame();
    }



    void StartGame()
    {
        _gameController.StartGame();

        _mainMenuCanvas.SetActive(false);       //这句在流程完成后多余
        _selectLevelCanvas.SetActive(false);
    }



    public void OpenRestartCanvas()
    {
        _restartCanvas.SetActive(true);
    }



    public void RestartButtonDown()
    {
        Restart();
    }

    void Restart()
    {
        _gameController.StartGame();

        _restartCanvas.SetActive(false);
    }


    public void MainMenuButtonDown()
    {
        _mainMenuCanvas.SetActive(true);

        _pauseCanvas.SetActive(false);
        _restartCanvas.SetActive(false);
        _selectLevelCanvas.SetActive(false);
    }
}
