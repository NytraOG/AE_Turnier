using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class Turnier
    {
        public Turnier()
        {
            Mannschaften = new List<Mannschaft>();
        }
        public DateTime         TurnierDatum { get; set; }
        public List<Mannschaft> Mannschaften { get; set; }

        public void TeamHinzufügen(string name)
        {
            var mannschaftExistiert = Mannschaften.Any(m => m.Name == name);

            if (mannschaftExistiert)
                throw new PassAufDummkopfException($"Mannschaft mit dem Namen {name} existiert bereits!");

            var mannschaft = new Mannschaft(name);

            Mannschaften.Add(mannschaft);
        }

        public void PaarungAnlegen(string mannschaft1, string mannschaft2)
        {
            var mannschaftEinsExistiert = Mannschaften.Any(m1 => m1.Name == mannschaft1);
            var mannschaftZweiExistiert = Mannschaften.Any(m2 => m2.Name == mannschaft2);

            if (!mannschaftEinsExistiert)
                throw new
                    MannschaftExistiertNichtException($"Eine Mannschaft mit dem Namen: [{mannschaft1}] existiert nicht!");
            if (!mannschaftZweiExistiert)
                throw new
                    MannschaftExistiertNichtException($"Eine Mannschaft mit dem Namen: [{mannschaft2}] existiert nicht!");

            var mannschaftEins = Mannschaften.First(m => m.Name == mannschaft1);
            var mannschaftZwei = Mannschaften.First(m => m.Name == mannschaft2);

            var paarung = new Spielpaarung(mannschaftEins, mannschaftZwei);

            mannschaftEins.Spiele.Add(paarung);
            mannschaftZwei.Spiele.Add(paarung);
        }

        public List<Mannschaft> TabelleAnzeigen()
        {
            if(Mannschaften.Count <= 0)
                throw new PassAufDummkopfException("Keine Mannschaften vorhanden, um Punkte berechnen zu können!");

            var mannschaftenNachPunktenSortiert = Mannschaften
                .OrderByDescending(m => m.Punktzahl)
                .ThenByDescending(m => m.GeschosseneTore)
                .ToList();

            return mannschaftenNachPunktenSortiert;
        }
    }
}