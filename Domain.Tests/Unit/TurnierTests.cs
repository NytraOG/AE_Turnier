using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.Unit
{
    [TestClass]
    public class TurnierTests
    {
        private Mannschaft team1;
        private Mannschaft team2;
        private Mannschaft team3;
        private Mannschaft team4;
        private Turnier    turnier;

        [TestInitialize]
        public void Init()
        {
            team1   = new Mannschaft("Die Bauern");
            team2   = new Mannschaft("Krasslords");
            team3   = new Mannschaft("Eintracht BOB");
            team4   = new Mannschaft("Bagaluden");
            turnier = new Turnier();
        }

        [TestMethod]
        public void PaarungAnlegen_BeideTeamsExistieren_EineNeuePaarungMitBeidenTeamsDrinnen()
        {
            //Arrange
            turnier.TeamHinzufügen(team1.Name);
            turnier.TeamHinzufügen(team2.Name);

            //Act
            turnier.PaarungAnlegen(team1.Name, team2.Name);

            var actualPaarung = turnier.Mannschaften.FirstOrDefault()?.Spiele.FirstOrDefault();

            //Assert
            Assert.IsNotNull(actualPaarung);
            Assert.AreEqual(team1.Name, actualPaarung.MannschaftEins.Name);
            Assert.AreEqual(team2.Name, actualPaarung.MannschaftZwei.Name);
            Assert.AreEqual(2, turnier.Mannschaften.Count);
            Assert.AreEqual(1, turnier.Mannschaften[0].Spiele.Count);
        }

        [TestMethod]
        public void PaarungAnlegen_TeamsExistierenNicht_MannschaftExistiertNichtException()
        {
            //Act & Assert
            Assert.ThrowsException<MannschaftExistiertNichtException>(() => turnier.PaarungAnlegen("asd", "das"));
        }

        [TestMethod]
        public void TeamHinzufügen_ValideWerte_NeueMannschaftinTurnierliste()
        {
            //Arrange
            var expectedMannschaft = team1;

            //Act
            turnier.TeamHinzufügen(team1.Name);
            var actualTeamname = turnier.Mannschaften.FirstOrDefault(tm => tm.Name == team1.Name)?.Name;

            //Assert
            Assert.IsNotNull(turnier.Mannschaften);
            Assert.AreEqual(expectedMannschaft.Name, actualTeamname);
        }

        [TestMethod]
        public void TeamHinzufügen_MehrereTeamsEintragen_KorrekteAnzahlAnMannschaften()
        {
            //Arrange
            var expectedMannschaften = new List<Mannschaft>
                                       {
                                           team1, team2, new Mannschaft("Baller"), new Mannschaft("Eintracht Vogel")
                                       };

            //Act
            foreach (var mannschaft in expectedMannschaften) turnier.TeamHinzufügen(mannschaft.Name);

            //Assert
            Assert.IsNotNull(turnier.Mannschaften);
            Assert.AreEqual(expectedMannschaften.Count, turnier.Mannschaften.Count);

            foreach (var mannschaft in turnier.Mannschaften)
            {
                var mannschaftExistiert = expectedMannschaften.Any(ex => ex.Name == mannschaft.Name);
                Assert.IsTrue(mannschaftExistiert);
            }
        }

        [TestMethod]
        public void TeamHinzufügen_TeamDoppeltEintragen_DummkopfException()
        {
            //Arrange
            turnier.TeamHinzufügen(team1.Name);

            //Act & Assert
            Assert.ThrowsException<PassAufDummkopfException>(() => turnier.TeamHinzufügen(team1.Name));
        }


        [TestMethod]
        public void TabelleAnzeigen_MannschaftenMitEinzigartigenPunktzahlen_NachPunktenSortierteListe()
        {
            //Arrange
            team1.Punktzahl = 4;
            team2.Punktzahl = 0;
            team3.Punktzahl = 9;
            team4.Punktzahl = 6;

            turnier.Mannschaften.AddRange(new[] {team1, team2, team3, team4});

            //Act
            var punkteTabelle = turnier.TabelleAnzeigen();

            //Assert
            Assert.IsNotNull(punkteTabelle);
            Assert.AreEqual(4, punkteTabelle.Count);
            Assert.AreEqual(team3.Name, punkteTabelle[0].Name);
            Assert.AreEqual(team4.Name, punkteTabelle[1].Name);
            Assert.AreEqual(team1.Name, punkteTabelle[2].Name);
            Assert.AreEqual(team2.Name, punkteTabelle[3].Name);
        }

        [TestMethod]
        public void TabelleAnzeigen_MannschaftenMitGleichenPunktenUnterschiedlichenToren_NachPunktenSortierteListe()
        {
            //Arrange
            team1.Punktzahl       = 0;
            team1.GeschosseneTore = 8;

            team2.Punktzahl       = 0;
            team1.GeschosseneTore = 5;

            team3.Punktzahl       = 6;
            team3.GeschosseneTore = 5;

            team4.Punktzahl       = 6;
            team4.GeschosseneTore = 3;

            turnier.Mannschaften.AddRange(new[] {team1, team2, team3, team4});

            //Act
            var punkteTabelle = turnier.TabelleAnzeigen();

            //Assert
            Assert.IsNotNull(punkteTabelle);
            Assert.AreEqual(4, punkteTabelle.Count);
            Assert.AreEqual(team3.Name, punkteTabelle[0].Name);
            Assert.AreEqual(team4.Name, punkteTabelle[1].Name);
            Assert.AreEqual(team1.Name, punkteTabelle[2].Name);
            Assert.AreEqual(team2.Name, punkteTabelle[3].Name);
        }

        [TestMethod]
        public void TabelleAnzeigen_KeineMannschaftenVorhanden_PassAufDummkopfException()
        {
            //Act & Assert
            Assert.ThrowsException<PassAufDummkopfException>(() => turnier.TabelleAnzeigen());
        }
    }
}