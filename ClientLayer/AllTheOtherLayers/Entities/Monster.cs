using System;
using System.Collections.Generic;
using System.Linq;

namespace BOS.ClientLayer.AllTheOtherLayers.Entities
{
    public class Monster : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }

        public (bool IsValid, List<string> Messages) Validate()
        {
            var messages = new List<string>();

            if(string.IsNullOrEmpty(Name))
                messages.Add("Name is required.");

            if(Power <= 0)
                messages.Add("Power must be greater than 0.");

            return (IsValid: !messages.Any(), Messages: messages);
        }
    }
}