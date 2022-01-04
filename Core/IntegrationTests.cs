using Core.Helper;
using Core.Models;
using NUnit.Framework;

namespace Core
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Arrange => Create object for class User
            var newUser = new UserDto
            {
                FirstName = "Ricky",
                LastName = "Martin",
                Age = 50,
                Email = "rickymartin@gmail.com" 
            };

            var newUser2 = new UserDto
            {
                FirstName = "Britney",
                LastName = "Spears",
                Age = 40,
                Email = "britneyspears@gmail.com"
            };

            //Act => Get path and set object to excel file
            var pathToExcelFile = ExcelHelper.GetFilePath("ExcelFile.xlsx");
            ExcelHelper.SetDataToExcelFile(pathToExcelFile, newUser);
            ExcelHelper.SetDataToExcelFile(pathToExcelFile, newUser2, 3, false);

            var data = ExcelHelper.GetTableFromExcelFile(pathToExcelFile);
        }
    }
}