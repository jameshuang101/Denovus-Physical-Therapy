using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject StartScreenPanel;
    public GameObject TaskListPanel;
    public GameObject ProgressPanel;

    void Start()
    {
        StartScreenPanel.SetActive(true);
        TaskListPanel.SetActive(false);
        ProgressPanel.SetActive(false);
    }

    public void SwitchPanelToStart()
    {
        StartCoroutine(DelaySwitchPanelToStart());
    }

    IEnumerator DelaySwitchPanelToStart()
    {
        yield return new WaitForSecondsRealtime(0.25F);
        StartScreenPanel.SetActive(true);
        TaskListPanel.SetActive(false);
        ProgressPanel.SetActive(false);
    }

    public void SwitchPanelToTask()
    {
        StartCoroutine(DelaySwitchPanelToTask());
    }

    IEnumerator DelaySwitchPanelToTask()
    {
        yield return new WaitForSecondsRealtime(0.25F);
        StartScreenPanel.SetActive(false);
        TaskListPanel.SetActive(true);
        ProgressPanel.SetActive(false);
    }

    public void SwitchPanelToProgress()
    {
        StartCoroutine(DelaySwitchPanelToProgress());
    }

    IEnumerator DelaySwitchPanelToProgress()
    {
        yield return new WaitForSecondsRealtime(0.25F);
        StartScreenPanel.SetActive(false);
        TaskListPanel.SetActive(false); 
        ProgressPanel.SetActive(true);
    }

}
