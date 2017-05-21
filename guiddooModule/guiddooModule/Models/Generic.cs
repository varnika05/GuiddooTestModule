using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace guiddooModule.Models
{
    public class Generic
    {


        public String createJSON(DataSet inputDataSet)
        {
            try
            {
                Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
                json.NullValueHandling = NullValueHandling.Ignore;
                json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
                json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                json.Converters.Add(new DataSetConverter());
                StringWriter sw = new StringWriter();
                Newtonsoft.Json.JsonTextWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.None;
                writer.QuoteChar = '"';
                json.Serialize(writer, inputDataSet);
                string output = sw.ToString();

              
                writer.Close();
                sw.Close();
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}