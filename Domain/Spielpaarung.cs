using System;
using System.Collections.Generic;
using Domain.Exceptions;

namespace Domain
{
    public class Spielpaarung
    {
        public Spielpaarung(Mannschaft mannschaftEins, Mannschaft mannschaftZwei)
        {
            MannschaftenMitToren = new Dictionary<string, int>();
            Spieldatum           = DateTime.Now;
            MannschaftEins       = mannschaftEins;
            MannschaftZwei       = mannschaftZwei;
        }

        public Mannschaft              MannschaftEins       { get; set; }
        public Mannschaft              MannschaftZwei       { get; set; }
        public DateTime                Spieldatum           { get; set; }
        public Dictionary<string, int> MannschaftenMitToren { get; set; }

        public void ToreEintragen(int tore1, int tore2)
        {
            if (tore1 < 0)
                throw new PassAufDummkopfException($"Angegebene Torzahl({tore1}) für Team: {MannschaftEins} ist ungültig!");
            if (tore2 < 0)
                throw new PassAufDummkopfException($"Angegebene Torzahl({tore2}) für Team: {MannschaftZwei} ist ungültig!");

            MannschaftEins.GeschosseneTore += tore1;
            MannschaftZwei.GeschosseneTore += tore2;

            MannschaftenMitToren.Add(MannschaftEins.Name, tore1);
            MannschaftenMitToren.Add(MannschaftZwei.Name, tore2);

            PunktzahlErmitteln(tore1, tore2);
        }

        private void PunktzahlErmitteln(int tore1, int tore2)
        {
            if (tore1 > tore2)
            {
                MannschaftEins.Punktzahl += 3;
            }
            else if (tore1 < tore2)
            {
                MannschaftZwei.Punktzahl += 3;
            }
            else
            {
                MannschaftEins.Punktzahl += 1;
                MannschaftZwei.Punktzahl += 1;
            }
        }
    }
}