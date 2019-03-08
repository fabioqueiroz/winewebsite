using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Business.Interfaces;
using Wine.Commons.Business.Models;
using Wine.Commons.Validation;

namespace Wine.Test.Validations
{
    [TestClass]
    public class ValidationTests
    {
        private IValidationRule<WineModel> _validation;

        [TestInitialize]
        public void SetUp()
        {

        }

        [TestMethod]
        public void FigureValidation()
        {
            //Arrange
            _validation = new FiguresRule<WineModel>(x => x.Price);

            // Act
            var result = _validation.IsValid(new WineModel { ID = 1, Name = "Test", Price = 6.70M, Description = "nothing", RegionId = 1, Sparkling = false });


            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FluentValidation()
        {
            // Arrange
            var testWine = new WineModel { ID = 1, Name = "Test", Price = 6.70M, Description = "nothing", RegionId = 1, Sparkling = false };
            FluentFiguresValidator validator = new FluentFiguresValidator();

            // Act
            var result = validator.Validate(testWine);


            // Assert 
            Assert.IsTrue(result.IsValid);
        }
    }  
}
