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
        public void GetCarDataListTest()//passed
        {
            d.AddCar("CarDataListTest", 1250, "gas");
            d.AddCarData("CarDataListTest", new CarData("CarDataListTest", 250000, 500, 50, "asd", 34000));
            d.AddCarData("CarDataListTest", new CarData("CarDataListTest", 250500, 500, 50, "asd", 34000));
            List<CarData> l = d.GetCarDataList("CarDataListTest");
            Assert.AreEqual(2,l.Count);
        }

        [TestMethod()]
        public void CalcFTperKMTest()//passed
        {
            d.AddCar("CalcTest",1250,"gas");
            d.AddCarData("CalcTest",new CarData("CalcTest",250000,500,50,"asd",34000));
            d.AddCarData("CalcTest", new CarData("CalcTest", 250500, 500, 50, "asd", 34000));
            double sum = (34000 + 34000) / (500 + 500);
           double c =  d.CalcFTperKM("CalcTest");
            Assert.AreEqual(sum,c);
        }

        [TestMethod()]
        public void DeleteCarTest()//passed
        {
            d.AddCar("DeleteTestCar", 123, "delete");
            int i = d.GetAutoID("DeleteTestCar");
            d.DeleteCar("DeleteTestCar");
            int k = d.GetAutoID("DeleteTestCar");
            Assert.AreNotEqual(i, k);
        }

        [TestMethod()]
        public void GetCarListTest() //passed
        {
            d.AddCar("asd",112,"benzin");
            List<string> l = d.GetCarList();
            Assert.IsTrue(l.Count >= 1);
        }

        [TestMethod()]
        public void GetAutoIDTest() //passed
        {
            d.AddCar("proba123",123123,"asdasd");
            int i = d.GetAutoID("proba123");
            Assert.AreEqual(i,(int)d.GetAutoID("proba123"));
        }

        [TestMethod()]
        public void GetCarDataTest()
        {
            d.AddCar("testcar", 123, "gas");
            d.AddCarData("testcar", new CarData("testcar",123123,123124,15,"nope",123));
          
            Assert.AreNotEqual(d.GetCarData("testcar").Rows.Count,0);
        } //passed

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