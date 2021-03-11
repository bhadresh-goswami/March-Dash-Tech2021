using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.SQLObject
{

    public class CommonHelperClass
    {

        public static string _serializeDatatable(DataTable dt)
        {
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            settings.Converters.Add(new DataSetConverter());
            return JsonConvert.SerializeObject(dt, settings);
        }
        public static string _serializeDataSet(DataSet ds)
        {
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            settings.Converters.Add(new DataSetConverter());
            return JsonConvert.SerializeObject(ds, settings);
        }
    }
}