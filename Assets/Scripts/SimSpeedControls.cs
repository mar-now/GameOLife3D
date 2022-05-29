
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimSpeedControls : MonoBehaviour
{
    [SerializeField] private Image _pauseButtonImage;
    [SerializeField] private TMP_Text _speedDisplay;


    public void TogglePause()
    {
        if (_pauseButtonImage == null)
            return;

        if (CellManager.Instance.IsSimulationPaused == true)
        {
            CellManager.Instance.IsSimulationPaused = false;
            _pauseButtonImage.color = Color.white;
        }
        else if (CellManager.Instance.IsSimulationPaused == false)
        {
            CellManager.Instance.IsSimulationPaused = true;
            _pauseButtonImage.color = Color.cyan;
        }
    }

    public void IncreaseSimSpeed()
    {
        if (CellManager.Instance.SimulationSpeed < 16)
            CellManager.Instance.SimulationSpeed *= 2;

        if (_speedDisplay != null)
            _speedDisplay.text = "Speed: x" + CellManager.Instance.SimulationSpeed;
    }

    public void LowerSimSpeed()
    {
        if (CellManager.Instance.SimulationSpeed > 0.5f)
            CellManager.Instance.SimulationSpeed /= 2;

        if (_speedDisplay != null)
            _speedDisplay.text = "Speed: x" + CellManager.Instance.SimulationSpeed;
    }
}
