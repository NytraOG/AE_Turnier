using System;
using System.Collections.Generic;

namespace Domain
{
    public class Mannschaft
    {
        public Mannschaft(string name)
        {
            Name   = name;
            Oid    = Guid.NewGuid();
            Spiele = new List<Spielpaarung>();
        }

        public Guid   Oid             { get; set; }
        public int    GeschosseneTore { get; set; }
        public int    Punktzahl       { get; set; }
        public string Name            { get; }

        public List<Spielpaarung> Spiele { get; set; }
    }
}