using Microsoft.VisualStudio.TestTools.UnitTesting;
using JourneyMangr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JourneyMangr.Tests
{
    [TestClass()]
    public class UnitTest1

    {
        DBase d = DBase.GetInstance();
        [TestMethod()]
        public void GetCarDataListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CalcFTperKMTest()
        {
            
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCarListTest()
        {
            Assert.AreEqual(1, 1);
        }

        [TestMethod()]
        public void GetAutoIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCarDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddCarTest()
        {
            List<string> carList = d.GetCarList();
            int i = carList.Count;
            d.AddCar("aswd",123,"benzin");
            int k = carList.Count;
            Assert.AreEqual(i,k-1);
        } //passed

        [TestMethod()]
        public void AddCarDataTest()
        {
            d.AddCar("asd", 123, "benzin");
            List<CarData> carList = d.GetCarDataList("asd");
            int i = carList.Count();
            d.AddCarData("asd",new CarData("asd",123123,32131,123123,"asderka",123132));
            int k = d.GetCarDataList("asd").Count;

            Assert.AreEqual(i,k-1);
        }//passed
    }
}