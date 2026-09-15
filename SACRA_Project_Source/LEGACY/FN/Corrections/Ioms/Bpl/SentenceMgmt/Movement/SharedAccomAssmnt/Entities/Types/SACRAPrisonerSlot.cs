
using System;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities
{
    [Serializable]
    public class SACRAPrisonerSlot
    {
        public SACRAPrisonerSlot()
        {
            CellSharingPrisonerId = string.Empty;
            PrisonerId = string.Empty;
            PrisonerName = string.Empty;
            PrisonerPRN = string.Empty;
        }
        public short OriginalOrderNumber { get; set; }
        public string CellSharingPrisonerId { get; set; }
        public string PrisonerId { get; set; }
        public string PrisonerName { get; set; }
        public string PrisonerPRN { get; set; }
    }
}