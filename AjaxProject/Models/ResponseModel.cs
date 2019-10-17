using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxProject.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public string Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}