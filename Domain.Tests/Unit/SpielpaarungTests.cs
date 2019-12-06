using System.Linq;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Unit
{
    [TestClass]
    public class SpielpaarungTests
    {
        private Spielpaarung paarung;
        private Mannschaft   team1;
        private Mannschaft   team2;
        private int          toreTeam1;
        private int          toreTeam2;

        [TestInitialize]
        public void Init()
        {
            toreTeam1 = 3;
            toreTeam2 = 2;
            team1     = new Mannschaft("Die Bauern");
            team2     = new Mannschaft("Krasslords");
            paarung   = new Spielpaarung(team1, team2);
        }

        [TestMethod]
        public void ToreEintragen_valideWerte_DictionaryUndPropertiesKorrekt()
        {
            //Act
            paarung.ToreEintragen(toreTeam1, toreTeam2);
            var inDictGesetzteTore1 = paarung.MannschaftenMitToren.FirstOrDefault(m => m.Key == "Die Bauern");
            var inDictGesetzteTore2 = paarung.MannschaftenMitToren.FirstOrDefault(m => m.Key == "Krasslords");

            //Assert
            Assert.IsNotNull(inDictGesetzteTore1);
            Assert.IsNotNull(inDictGesetzteTore2);
            Assert.AreEqual(toreTeam1, inDictGesetzteTore1.Value);
            Assert.AreEqual(toreTeam2, inDictGesetzteTore2.Value);
        }

        [TestMethod]
        public void ToreEintragen_InvaliderWert_PassAufDummkopfException()
        {
            //Arrange
            var invaliderTorWert = -500;

            //Act
            Assert.ThrowsException<PassAufDummkopfException>(() => paarung.ToreEintragen(toreTeam1, invaliderTorWert));
        }

        [TestMethod]
        [DataRow(2, 1, 3, 0)]
        [DataRow(2, 3, 0, 3)]
        [DataRow(3, 3, 1, 1)]
        public void PunktzahlErmitteln_ValideWerte_KorrektePunktzahlEingetragen(int tore1, int tore2, int expected1,
            int                                                                     expected2)
        {
            //Act
            paarung.ToreEintragen(tore1, tore2);

            var punkteTeamEins = paarung.MannschaftEins.Punktzahl;
            var punkteTeamZwei = paarung.MannschaftZwei.Punktzahl;

            //Assert
            Assert.AreEqual(expected1, punkteTeamEins);
            Assert.AreEqual(expected2, punkteTeamZwei);
        }
    }
}