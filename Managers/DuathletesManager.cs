using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ZealandDuathlon;

namespace RestDuathlon.Managers
{
    public class DuathletesManager
    {
        private static int _nextBib = 1;
        private static List<Duathlete> duathletes = new List<Duathlete>()
        {
            new Duathlete(){Bib = _nextBib++, Name="Casper", AgeGroup=1, Bike=4000, Run=2000},
            new Duathlete(){Bib = _nextBib++, Name="Heidi", AgeGroup=2, Bike=4500, Run=1000},
            new Duathlete(){Bib = _nextBib++, Name="Tine", AgeGroup=3, Bike=3100, Run=1800},
            new Duathlete(){Bib = _nextBib++, Name="Sebastian", AgeGroup=1, Bike=4400, Run=2800},
            new Duathlete(){Bib = _nextBib++, Name="Carsten", AgeGroup=4, Bike=2200, Run=900}
        };

        //Vi laver en ny/kopiere listen med objekterne i fra den liste ovenover, for at kunne sende en liste med objekter som 
        //controlleren kan rette i, uden at redigere i de oprindelige objekter og så de objekter går tilbage til oprindelig data. 
        public IEnumerable<Duathlete> GetAll()
        {
            return new List<Duathlete>(duathletes);
        }

        //Her tager vi fat i listen og iterere igennem listen efter Bib og sammenligner med alle Bibs i listen med den indtastede
        //bib som vi har givet i parameteren i metoden. 
        public Duathlete? GetByBib(int bib)
        {
            return duathletes.Find(duathlete => duathlete.Bib == bib);
        }

        //Først kalder vi validate på vores nye objekt i metoden, for at sikrer at det nye objekt overholder de constraints som er givet.
        //Så auto-generater den et bib til det nye objekt.
        //Næst kalder vi Add() metoden på listen og så indsætter det nye objekt givet i parameteren til metoden ind i add metoden Add(newDuathlete).
        //til sidst retunere vi det ny oprettede objekt, så kan man evt. vise det når man opretter det.
        public Duathlete Add(Duathlete newDuathlete)
        {
            newDuathlete.Validate();
            newDuathlete.Bib = _nextBib++;
            duathletes.Add(newDuathlete);
            return newDuathlete;
        }

        //Kalder igen validate for at se om de nye data overholder mulige constraints.
        //Så spørger vi efter om der er et objekt i listen som er ligmed det bib som er givet. ? betyder at den gerne må være nullable.
        //I if() statementede siger vi at hvis objektet ikke findes så skal den bare retunere null.
        //Så siger vi at de gamle properties på det specifikke objekt opdateres til de nye properties/data givet.
        // Til sidst retunere vi det ny opdaterede objekt
        public Duathlete? Update(int bib, Duathlete updates)
        {
            updates.Validate();
            Duathlete? oldDuathlete = GetByBib(bib);
            if (oldDuathlete == null) return null;
            oldDuathlete.Name = updates.Name;
            oldDuathlete.AgeGroup = updates.AgeGroup;
            oldDuathlete.Bike = updates.Bike;
            oldDuathlete.Run = updates.Run;
            return oldDuathlete;
        }

        //Vi spørger efter et objekt med det givne bib ligesom i update
        //Og så igen så hvis ikke findes så retunere vi bare null
        //Ellers kalder vi Remove på det specifikke objekt i listen og retunere det fjernede objekt Spørg Morten på Mandag! :D
        public Duathlete? Delete(int bib)
        {
            Duathlete? tobeDeleted = GetByBib(bib);
            if (tobeDeleted == null) return null;
            duathletes.Remove(tobeDeleted);
            return tobeDeleted;
        }

    }
}
