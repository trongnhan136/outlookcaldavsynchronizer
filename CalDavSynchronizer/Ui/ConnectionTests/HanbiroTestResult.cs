using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CalDavSynchronizer.Ui.ConnectionTests
{
    public class HanbiroTestResult
    {
       private readonly string _type;
       private readonly string _message;
        private readonly string _url;

        public HanbiroTestResult(String type, string message, string url)
        {
            this._type = type;
            this._message = message;
            this._url = url;
        }

        public string Type
        {
            get { return _type; }
        }

        public string Message
        {
            get { return _message; }
        }
        public string Url
        {
            get { return _url; }
        }
    }
}
