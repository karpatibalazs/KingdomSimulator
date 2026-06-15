using System;

namespace KingdomSim.Exceptions
{
    /// <summary>
    /// Kivétel, amely akkor dobódik, ha egy lovag érvénytelen vagy lehetetlen akciót próbál végrehajtani (pl. halottan próbál mozogni).
    /// </summary>
    public class InvalidKnightActionException : Exception
    {
        public InvalidKnightActionException(string message) : base(message) { }
        public InvalidKnightActionException(string message, Exception innerException) : base(message, innerException) { }
    }
}