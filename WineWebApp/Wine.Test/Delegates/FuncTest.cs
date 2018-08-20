using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wine.Test.Delegates
{
    [TestClass]
    public class FuncTest
    {
        private NewExercise _newValue;

        private ExerciseDelegate _value;

        [TestInitialize]
        public void SetUp()
        {
            _value = new ExerciseDelegate();

            _newValue = new NewExercise();
        }


        [TestMethod] // a test method never gives or parameters back
        public void TestSum()
        {
            Func<int, int, int> Sum = _value.AddSomething;

            int result = Sum(10, 5);
            Assert.AreEqual(15, result);
        }
        
        [TestMethod]
        public void VerifySumTest()
        {
            var result = _value.VerifySum(_value.AddSomething);
            Assert.AreEqual(result, 15);
        }

        [TestMethod] 
        public void MyFavouriteDelegate()
        {
            Func<int, int, int> Sum = (x, y) => x + y;

            Assert.AreEqual(15, Sum(12, 3));
        }

        [TestMethod]
        public void TestArea()
        {
            Func<int, int, int, int> TheArea = _newValue.CircleArea;
            
            int result = TheArea(2, 3, 4);

            Assert.AreEqual(96, result);
            Debug.WriteLine (result);
        }

    }

    public class ExerciseDelegate
    {
        public int AddSomething(int k, int p)
        {
            return k + p;
        }

        public int VerifySum(Func<int, int, int> addCalculator)
        {
            return addCalculator(11, 4);
        }
    }

    public class NewExercise
    {
        public int CircleArea(int x, int pi, int r)
        {
            return x * pi * r * r;
        }
    }
    
}
