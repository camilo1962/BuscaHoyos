using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropDown;
    public Dropdown graphicsDropDown;
    public Slider volumeSlider;
    public Toggle fullScreen;
    

    private void Start()
    {
        //resolutions = RemoveDuplicateResolutions(Screen.resolutions);
        //resolutionDropDown.ClearOptions();

        //List<string> options = new List<string>();
        //
        //int currentResolutionIndex = 0;
        //
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        //        currentResolutionIndex = i;
        //
        //    options.Add(resolutions[i].width + " x " + resolutions[i].height);
        //}
        //    
        //resolutionDropDown.AddOptions(options);
        //resolutionDropDown.value = currentResolutionIndex;
        //resolutionDropDown.RefreshShownValue();
        RevisarResolucion();
        graphicsDropDown.value = 2;
        QualitySettings.SetQualityLevel(2);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        fullScreen.isOn = Screen.fullScreen;
    }
    //private void Start()
    //{
    //    // Llamar al método para cargar las resoluciones después de un breve retraso
    //    Invoke("LoadResolutions", 3f);
    //    resolutionDropDown.value = 2;
    //    graphicsDropDown.value = 2;
    //    QualitySettings.SetQualityLevel(2);
    //    volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    //    fullScreen.isOn = Screen.fullScreen;
    //}
    //
    //// Método para cargar las resoluciones
    //private void LoadResolutions()
    //{
    //    resolutions = RemoveDuplicateResolutions(Screen.resolutions);
    //
    //    if (resolutionDropDown != null)
    //    {
    //        resolutionDropDown.ClearOptions();
    //
    //        List<string> options = new List<string>();
    //
    //        int currentResolutionIndex = 0;
    //
    //        for (int i = 0; i < resolutions.Length; i++)
    //        {
    //            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
    //                currentResolutionIndex = i;
    //
    //            options.Add(resolutions[i].width + " x " + resolutions[i].height);
    //        }
    //
    //        resolutionDropDown.AddOptions(options);
    //        resolutionDropDown.value = currentResolutionIndex;
    //        resolutionDropDown.RefreshShownValue();
    //    }
    //}

    private Resolution[] RemoveDuplicateResolutions(Resolution[] resArr)
    {
        Resolution res = resArr[0];
        List<Resolution> lstRes = new List<Resolution>();

        for (int i = 0; i < resArr.Length; i++)
        {
            if (!((resArr[i].width == res.width) && (resArr[i].height == res.height)))
            {
                lstRes.Add(res);
                res = resArr[i];
            }
        }

        return lstRes.ToArray();
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("Volume",volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetScreenMode(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
   
    public void SetResolution(int resolutionIndex)
    {
        // Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height,Screen.fullScreen);
        Debug.Log("SetResolution() llamado con el índice de resolución: " + resolutionIndex);
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        SceneManager.LoadScene(0);
    }

    public void RevisarResolucion()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string opcion = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(opcion);


            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }

        resolutionDropDown.AddOptions(opciones);
        resolutionDropDown.value = resolucionActual;
        resolutionDropDown.RefreshShownValue();


        //
        resolutionDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
        //
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        //
        PlayerPrefs.SetInt("numeroResolucion", resolutionDropDown.value);
        //


        Resolution resolution = resolutions[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
}



