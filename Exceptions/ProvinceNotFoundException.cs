using System;

namespace KingdomSim.Exceptions
{
    /// <summary>
    /// Kivétel, amely akkor dobódik, ha a királyságban nem található a keresett provincia (pl. menedék keresésekor).
    /// </summary>
    public class ProvinceNotFoundException : Exception
    {
        public ProvinceNotFoundException(string message) : base(message) { }
        public ProvinceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}