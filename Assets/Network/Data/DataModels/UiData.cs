using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Network.Data.Datamodels
{

    public class UIData
    {
        private Dictionary<string, ApplicationData> applicationsDict;

        // In a hypothetical future, the UIData can hold more params for other types of data
        public UIData(Dictionary<string, ApplicationData> dictApp)
        {
            applicationsDict = dictApp;
        }


        public List<ApplicationData> Applications
        {
            get
            {
                //TODO: return a list of Application objects after sorting
                return getSortedList();
            }
        }

        private List<ApplicationData> getSortedList()
        {
            var listapps = new List<ApplicationData>();
            foreach (KeyValuePair<string, ApplicationData> kv in applicationsDict)
            {
                //add package name to data object
                if (kv.Value != null)
                {
                    kv.Value.packagename = kv.Key;
                }

                listapps.Add(kv.Value);
            }

            listapps.Sort(new ApplicationDataComparer());
            return listapps;
        }


    }

}
