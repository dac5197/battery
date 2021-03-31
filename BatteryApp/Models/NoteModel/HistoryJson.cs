using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public class HistoryJson
    {
        [JsonConstructor]
        public HistoryJson(string fieldName, string oldValue, string newValue)
        {
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string FieldName { get; }
        public string OldValue { get; }
        public string NewValue { get; }
    }
}
