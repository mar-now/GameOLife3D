using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] TMP_Text speedDisplay;
    [SerializeField] Button pauseButton;
    Image pauseButtonImage;
    private float _simulationSpeed = 4f;
    private bool _isSimulationPaused = false;

    public float SimulationSpeed { get => _simulationSpeed; set => _simulationSpeed = value; }
    public bool IsSimulationPaused { get => _isSimulationPaused; set => _isSimulationPaused = value; }

    private void Awake()
    {
        base.Awake();

        if(pauseButton != null)
            pauseButtonImage = pauseButton.GetComponent<Image>();
        if(speedDisplay != null)
            speedDisplay.text = "Speed: x" + _simulationSpeed;
    }
    public void TogglePause()
    {
        if (pauseButton == null)
            return;

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

        if(speedDisplay != null)
            speedDisplay.text = "Speed: x" + _simulationSpeed;
    }

    public void LowerSimSpeed()
    {
        if (_simulationSpeed > 0.5f)
            _simulationSpeed /= 2;

        if (speedDisplay != null)
            speedDisplay.text = "Speed: x" + _simulationSpeed;
    }
}
