﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MvcMate.Web
{
    static class Extensions
    {
        public static string ToJsonpString(this JsonSerializer serializer, object data, string callbackName)
        {
            using(var writer = new StringWriter())
            {
                serializer.Serialize(writer, data);
                var json = writer.ToString();
                return string.Format("{0}({1})", callbackName, json);
            }    
        }
    }
}
