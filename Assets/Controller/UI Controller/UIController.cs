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
        _selectLevelCanvas.SetActive(true);
        InactiveOtherCanvas(_selectLevelCanvas);
    }



    public void StartGame()
    {
        _gameController.StartGame();
        InactiveAllCanvas();
    }



    public void OpenRestartCanvas()
    {
        _restartCanvas.SetActive(true);
        InactiveOtherCanvas(_restartCanvas);
    }



    public void RestartButtonDown()
    {
        _gameController.StartGame();
        InactiveAllCanvas();
    }



    public void MainMenuButtonDown()
    {
        _mainMenuCanvas.SetActive(true);
        InactiveOtherCanvas(_mainMenuCanvas);
    }



    public void OpenPauseCanvas()
    {
        _pauseCanvas.SetActive(true);
        InactiveOtherCanvas(_pauseCanvas);
    }




    void InactiveOtherCanvas(GameObject activeCanvas)
    {
        if (activeCanvas != _mainMenuCanvas)
            _mainMenuCanvas.SetActive(false);
        if (activeCanvas != _selectLevelCanvas)
            _selectLevelCanvas.SetActive(false);
        if (activeCanvas != _restartCanvas)
            _restartCanvas.SetActive(false);
        if (activeCanvas != _pauseCanvas)
            _pauseCanvas.SetActive(false);
    }

    void InactiveAllCanvas()
    {
        _mainMenuCanvas.SetActive(false);
        _selectLevelCanvas.SetActive(false);
        _restartCanvas.SetActive(false);
        _pauseCanvas.SetActive(false);
    }
}
