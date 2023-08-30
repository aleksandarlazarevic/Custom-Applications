using System;
using System.Collections.Generic;

namespace Utilities.OTPServices
{
    public class MailListModel
    {
        public int last { get; set; }
        public string total { get; set; }
        public List<Msg> msgs { get; set; }
    }

    public class Msg
    {
        public string uid { get; set; }
        public string ib { get; set; }
        public string f { get; set; }
        public string s { get; set; }
        public bool d { get; set; }
        public List<Object> at { get; set; }
        public int r { get; set; }
        public bool ru { get; set; }
        public string fe { get; set; }
        public string rf { get; set; }
        public string ii { get; set; }
        public string av { get; set; }
    }
}