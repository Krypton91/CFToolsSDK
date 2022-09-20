using System;
using System.Collections.Generic;
using System.Text;

namespace CFToolsSDK.classes.models
{
    public class Creator
    {
        public string cftools_id { get; set; }
    }

    public class Entry
    {
        public DateTime created_at { get; set; }
        public Creator creator { get; set; }
        public List<Link> links { get; set; }
        public Meta meta { get; set; }
        public DateTime updated_at { get; set; }
        public User user { get; set; }
        public string uuid { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string method { get; set; }
        public string relationship { get; set; }
    }

    public class Meta
    {
        public string comment { get; set; }
        public object expiration { get; set; }
        public bool from_api { get; set; }
    }

    public class WhiteListResponse
    {
        public List<Entry> entries { get; set; }
        public bool status { get; set; }
    }

    public class User
    {
        public string cftools_id { get; set; }
    }
}
