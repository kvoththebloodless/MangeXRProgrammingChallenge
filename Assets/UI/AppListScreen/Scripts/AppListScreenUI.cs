using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network.Data.Datamodels;
using Network.Service;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
public class AppListScreenUI : MonoBehaviour
{
    public GameObject Content;
    GameObject ApplicationPrefab;
    Texture2D DefaultIcon;
    public NetworkService NetworkService;

    private void Awake()
    {
        ApplicationPrefab = Resources.Load<GameObject>("ApplicationUI");
        DefaultIcon = Resources.Load<Texture2D>("defaultIcon");
        NetworkService.OnUIDataUpdate += OnUIDataUpdate;
    }
    // Start is called before the first frame update
    void Start()
    {


    }
    /// <summary>
    /// When the data updates after 20 sec refresh from networkservice
    /// </summary>
    /// <param name="data">the updated UIData</param>
    void OnUIDataUpdate(UIData data)
    {
        var appdatalist = data.Applications;
        //Clean up the previous children.
        // Can possibly be inefficient to keep destroying and creating new UI boxes but
        // But if less number of UI elements then might be cleaner to do it this way.

        // Destroy Existing UIElements
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }

        //Add all the new refreshed UIElements

        foreach (ApplicationData app in appdatalist)
        {
            var appObj = GameObject.Instantiate(ApplicationPrefab);
            appObj.transform.SetParent(Content.transform);
            var textField = appObj.GetComponentInChildren<TextMeshProUGUI>(true);
            textField.text = app.title;
            var image = appObj.GetComponentInChildren<RawImage>();
            if (app.iconUrl != null)
                StartCoroutine(DownloadIconImage(app.iconUrl, (Texture2D tex) => { image.texture = tex; }));
            else
                image.texture = DefaultIcon;
        }

        // Alternative approach, to cache a list of previous data, compare which ones need be destroyed and which ones to add 
        // and change UI only according to that.
    }

    // This will probably move to the network layer where a GET method exists where you can provide
    // a URI and get a texture. It can be used across the codebase. But in the interest of time. 

    /// <summary>
    /// A coroutine that downloads the image from iconURL in json if it exists otherwise uses the default icon
    /// </summary>
    /// <param name="iconURL"></param>
    /// <param name="OnRequestComplete"></param>
    /// <returns></returns>
    IEnumerator DownloadIconImage(string iconURL, Action<Texture2D> OnRequestComplete)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(iconURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            OnRequestComplete.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
    }

    private void OnDestroy()
    {
        NetworkService.OnUIDataUpdate -= OnUIDataUpdate;
    }
}

