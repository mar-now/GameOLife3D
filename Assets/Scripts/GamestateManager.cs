using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamestateManager : Singleton<GamestateManager>
{
    [SerializeField] TMP_Text speedDisplay;
    [SerializeField] Button pauseButton;
    Image pauseButtonImage;
    private static float _simulationSpeed = 2f;
    private static bool _isSimulationPaused = false;

    public static float SimulationSpeed { get => _simulationSpeed; set => _simulationSpeed = value; }
    public static bool IsSimulationPaused { get => _isSimulationPaused; set => _isSimulationPaused = value; }

    private void Awake()
    {
        base.Awake();
        pauseButtonImage = pauseButton.GetComponent<Image>();
        speedDisplay.text = "Speed: x" + _simulationSpeed;
    }
    public void TogglePause()
    {
        if (_isSimulationPaused == true)
        {
            _isSimulationPaused = false;
            pauseButtonImage.color = Color.white;
        }
        else if (_isSimulationPaused == false)
        {
            _isSimulationPaused = true;
            pauseButtonImage.color = Color.cyan;
        }
    }

    public void IncreaseSimSpeed()
    {
        if (_simulationSpeed < 16)
            _simulationSpeed *= 2;

        speedDisplay.text = "Speed: x" + _simulationSpeed;
    }

    public void LowerSimSpeed()
    {
        if (_simulationSpeed > 0.5f)
            _simulationSpeed /= 2;

        speedDisplay.text = "Speed: x" + _simulationSpeed;
    }
}
