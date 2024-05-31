using System.Diagnostics;
using JaratKezeloProject;
namespace TestJaratKezeloProject
{
    public class Tests
    {
        Jaratkezelo jaratKezelo;
        DateTime datum;
        

        [SetUp]
        public void Setup()
        {
            jaratKezelo = new Jaratkezelo();
            datum = DateTime.Now;
        }

        [Test]
        public void JaratSzamNull()
        {
            Assert.Throws<ArgumentNullException>(() => jaratKezelo.ujJarat(null, "New York", "Alabama", datum));
        }

        [Test]
        public void JaratSzamUres()
        {
            Assert.Throws<ArgumentException>(() => jaratKezelo.ujJarat("", "New York", "Alabama", datum));
        }

        [Test]
        public void RepterHonnanNull()
        {
            Assert.Throws<ArgumentNullException>(() => jaratKezelo.ujJarat("ABC", null, "Alabama", datum));
        }

        [Test]
        public void RepterHonnanUres()
        {
            Assert.Throws<ArgumentException>(() => jaratKezelo.ujJarat("ABC", "", "Alabama", datum));
        }

        [Test]
        public void RepterHovaNull()
        {
            Assert.Throws<ArgumentNullException>(() => jaratKezelo.ujJarat("ABC", "New York", null, datum));
        }

        [Test]
        public void RepterHovaUres()
        {
            Assert.Throws<ArgumentException>(() => jaratKezelo.ujJarat("ABC", "New York", "", datum));
        }

        [Test]

        public void LetezoJaratszam()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            Assert.Throws<ArgumentException>(() => jaratKezelo.ujJarat("124", "New York", "Alabama", datum));
        }

        [Test]
        public void UjJarat_ErvenyesAdatokkal_Keses0()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            Assert.That(jaratKezelo.Jaratok.First().Keses, Is.Zero);
        }

        [Test]
        public void UjJarat_KesesJo()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            jaratKezelo.keses("124", 35);
            Assert.That(jaratKezelo.Jaratok.First().Keses, Is.EqualTo(35));
        }

        [Test]
        public void KesesSumnegativ()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            Assert.Throws<NegativKesesException>(() => jaratKezelo.keses("124", -5));
        }

        [Test]
        public void UjJarat_TobbKeses()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            jaratKezelo.keses("124", 35);
            jaratKezelo.keses("124", 20);
            jaratKezelo.keses("124", -25);
            Assert.That(jaratKezelo.Jaratok.First().Keses, Is.EqualTo(30));
        }

        [Test]
        public void UjJarat_TobbKeses_TobbJarat()
        {
            jaratKezelo.ujJarat("124", "New York", "Alabama", datum);
            jaratKezelo.keses("124", 35);
            jaratKezelo.keses("124", 20);
            jaratKezelo.keses("124", -25);
            jaratKezelo.ujJarat("235B", "Alabama", "New York", datum);
            jaratKezelo.keses("235B", 24);
            Assert.That(jaratKezelo.Jaratok.Where(x => x.JaratSzam == "124").First().Keses, Is.EqualTo(30));
            Assert.That(jaratKezelo.Jaratok.Where(x => x.JaratSzam == "235B").First().Keses, Is.EqualTo(24));
        }

    }
}