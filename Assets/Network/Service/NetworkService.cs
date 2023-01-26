using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network.Data.Datamodels;
using System;
using Newtonsoft.Json;
namespace Network.Service
{
    public class NetworkService : MonoBehaviour
    {
        public Action<UIData> OnUIDataUpdate = (data) => { };

        private float frequency = 20f;

        private float currentTime;
        // Start is called before the first frame update
        void Start()
        {
            //Initial fetch of UIDATA
            var newUIData = GET("https://UIDATA/SOMETHING");
            OnUIDataUpdate.Invoke(newUIData);
        }

        // Update is called once per frame
        void Update()
        {
            if ((Time.time - currentTime) > frequency)
            {
                var newUIData = GET("https://UIDATA/SOMETHING");
                OnUIDataUpdate.Invoke(newUIData);
                currentTime = Time.time;
            }
        }


        public UIData GET(string URI)
        {
            try
            {
                // URI can be used in the future. For now we mock it by reading the json from a file
                TextAsset library = Resources.Load<TextAsset>("library");

                var libraryData = JsonConvert.DeserializeObject<Dictionary<string, ApplicationData>>(library.text);

                if (libraryData != null)
                    // Deserialize the Library File
                    return new UIData(libraryData);
            }

            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }


            return null;


        }
    }
}
