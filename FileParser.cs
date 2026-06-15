using System;
using System.Collections.Generic;
using System.IO;
using KingdomSim.Entities;
using KingdomSim.Models;
using KingdomSim.States;

namespace KingdomSim
{
    /// <summary>
    /// A bemeneti szövegfájl feldolgozásáért felelős osztály.
    /// </summary>
    public static class FileParser
    {
        public static Kingdom ParseFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            if (lines.Length == 0) throw new Exception("A fájl üres!");

            var provData = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var provinces = new List<Province>();

            for (int i = 0; i < provData.Length; i += 2)
            {
                string pName = provData[i];
                char pType = provData[i + 1][0];

                IProvinceState state = pType switch
                {
                    'K' => new RoyalState(),
                    'S' => new NeutralState(),
                    'E' => new HostileState(50),
                    _ => throw new Exception($"Ismeretlen provincia típus: {pType}")
                };

                provinces.Add(new Province(pName, state));
            }

            Province startProvince = provinces[0];

            var knights = new List<Knight>();
            for (int i = 1; i < lines.Length; i++)
            {
                var kData = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (kData.Length < 2) continue;

                string kName = kData[0];
                char kType = kData[1][0];

                Knight k = kType switch
                {
                    'v' => new RecklessKnight(kName, 100, startProvince),
                    'b' => new BraveKnight(kName, 100, startProvince),
                    'o' => new CautiousKnight(kName, 100, startProvince),
                    _ => throw new Exception($"Ismeretlen lovag fajta: {kType}")
                };

                knights.Add(k);
            }

            return new Kingdom(provinces, knights);
        }
    }
}