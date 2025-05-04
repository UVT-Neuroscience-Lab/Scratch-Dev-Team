using Gtec.UnityInterface;
using System;
using UnityEngine;
using static Gtec.UnityInterface.BCIManager;

public class ClassSelectionAvailableExample : MonoBehaviour
{
    private uint _selectedClass = 0;
    private bool _update = false;

    public AudioSource[] classAudioSources; // Array for 4 audio sources

    void Start()
    {
        BCIManager.Instance.ClassSelectionAvailable += OnClassSelectionAvailable;

        // Optional: Validate array size
        if (classAudioSources.Length < 4)
        {
            Debug.LogWarning("Not enough audio sources assigned to handle all classes.");
        }
    }

    void OnApplicationQuit()
    {
        BCIManager.Instance.ClassSelectionAvailable -= OnClassSelectionAvailable;
    }

    void Update()
    {
        if (_update)
        {
            if (_selectedClass < classAudioSources.Length && classAudioSources[(int)_selectedClass] != null)
            {
                classAudioSources[(int)_selectedClass].Play();
                Debug.Log("Playing audio for class " + _selectedClass);
            }
            else
            {
                Debug.LogWarning("No audio assigned for class " + _selectedClass);
            }

            _update = false;
        }
    }

    private void OnClassSelectionAvailable(object sender, EventArgs e)
    {
        ClassSelectionAvailableEventArgs ea = (ClassSelectionAvailableEventArgs)e;
        _selectedClass = ea.Class;
        _update = true;
        Debug.Log(string.Format("Selected class: {0}", ea.Class));
    }
}
