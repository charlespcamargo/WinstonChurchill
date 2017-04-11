using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.BackEnd.Common.Conversores
{
    public class CustomCPFCNPJ : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && !String.IsNullOrEmpty(reader.Value.ToString()))
                return WinstonChurchill.API.Common.Util.Tools.Formatar.MascaraCnpjCpf(reader.Value.ToString());
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string valor = "";

            if (value != null)
                valor = WinstonChurchill.API.Common.Util.Tools.Formatar.MascaraCnpjCpf(value.ToString());

            writer.WriteValue(valor);
        }
    }
}
