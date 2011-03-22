using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String location= "Terre Haute, IN";
            String anotherLocation = "Lexington, KY";

            using (mocks.Record())
            {
                // The mock will return "Whale Rider" when the call is made with 24
                mockDatabase.getCarLocation(50);
                LastCall.Return(location);
                // The mock will return "Raptor Wrangler" when the call is made with 1025
                mockDatabase.getCarLocation(1025);
                LastCall.Return(anotherLocation);

            }
            var target = new Car(11);
            target.Database = mockDatabase;
            String result;
            result = target.getCarLocation(50);
            Assert.AreEqual(result, location);
            result = target.getCarLocation(1025);
            Assert.AreEqual(result, anotherLocation);

        }

        [Test()]
        public void TestThatCarGetsMilesFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 10;
           


            mockDatabase.Miles = Miles;
            var target = new Car(11);
            target.Database = mockDatabase;
            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);
        }

        [Test()]
        public void TestObjectMother()
        {
            var target = ObjectMother.BMW();

            Assert.AreEqual("BMW", target.Name);

        }
	}
}
