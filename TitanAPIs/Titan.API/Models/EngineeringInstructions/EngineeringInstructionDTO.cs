using System.Collections.Generic;

namespace Titan.API.Models.EngineeringInstructions
{
    public class EngineeringInstructionDTO
    {
        public string EIRef { get; set; }
        public bool IsTopLevel { get; set; }
        public List<EngineeringInstructionDTO> Children { get; set; } =
            new List<EngineeringInstructionDTO>();
    }
}
