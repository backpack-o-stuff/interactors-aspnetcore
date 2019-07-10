using System.Collections.Generic;

namespace BOS.ClientLayer.AllTheOtherLayers.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        (bool IsValid, List<string> Messages) Validate();
    }
}