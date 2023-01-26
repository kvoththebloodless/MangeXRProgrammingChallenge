
using System.Collections.Generic;

namespace Network.Data.Datamodels
{
    public class ApplicationData
    {
        public bool hidden;
        public int sortorder;
        public string title;
        public string icon;

        //Todo: If there's time, add the packagename to the Application model when making list.
        public string packagename;
    }

    public class ApplicationDataComparer : IComparer<ApplicationData>
    {
        public int Compare(ApplicationData x, ApplicationData y)
        {
            if (x == null)
            { return -1; }
            if (y == null)
            {
                return 1;
            }
            if (x.sortorder == y.sortorder)
            {
                return string.Compare(x.title, y.title);
            }

            return x.sortorder - y.sortorder;
        }
    }


}
