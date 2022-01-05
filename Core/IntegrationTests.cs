using Core.Helper;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;

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

            //Getting data from excel and converting to User DTO
            var listUsersDtoFromExcelFile = ExcelHelper.GetTableFromExcelFile(pathToExcelFile);

            //Assert => using Linq
            listUsersDtoFromExcelFile.First().Should().BeEquivalentTo(newUser);
            listUsersDtoFromExcelFile.Last().Should().BeEquivalentTo(newUser2);
            
            //or

            var firstUser = listUsersDtoFromExcelFile.Single(user => user.FirstName.Equals("Ricky"));
            firstUser.Should().BeEquivalentTo(newUser);

            var secondUser = listUsersDtoFromExcelFile.Single(user => user.LastName.Equals("Spears"));
            secondUser.Should().BeEquivalentTo(newUser2);
        }
    }
}