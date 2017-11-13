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
        public void GetCarListTest() //passed
        {
            d.AddCar("asd",112,"benzin");
            List<string> l = d.GetCarList();
            Assert.IsTrue(l.Count >= 1);
        }

        [TestMethod()]
        public void GetAutoIDTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCarDataTest()
        {
            d.AddCar("testcar", 123, "gas");
            d.AddCarData("testcar", new CarData("testcar",123123,123124,15,"nope",123));
          
            Assert.AreNotEqual(d.GetCarData("testcar").Rows.Count,0);
        }

        [TestMethod()]
        public void AddCarTest()
        {

            List<string> carList = d.GetCarList();
            int i = carList.Count;
            d.AddCar("aswd",123,"benzin");
            carList = d.GetCarList();
            Assert.IsTrue(carList.Count > i);
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