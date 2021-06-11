using ApplicationLayer.Interfaces;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCoreWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSUnitTestProject.Controller
{
    [TestClass]
    public class ContactControllerTest
    {
        [TestMethod]
        public async Task Task_Get_Contact_List_Return_OkResult()
        {
            // Arrange
            var mockRepo = new Mock<IContactDetailsApp>();
            mockRepo.Setup(repo => repo.GetContactList())
                .ReturnsAsync(getContactList());
            var controller = new ContactController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode;
            Assert.AreEqual(200, statusCode);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));
        }

        [TestMethod]
        public async Task Task_Get_Contact_Details_By_Id_Return_OkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IContactDetailsApp>();
            mockRepo.Setup(repo => repo.GetContactDetailsById(id))
                .ReturnsAsync(getConatctDetailsById(id));
            var controller = new ContactController(mockRepo.Object);

            // Act
            var result = await controller.GetContactById(id);

            // Assert
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode;
            Assert.AreEqual(200, statusCode);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));
        }

        [TestMethod]
        public async Task Task_Save_Contact_Details_Return_OkResult()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);

            var mockRepo = new Mock<IContactDetailsApp>();
            mockRepo.Setup(repo => repo.AddContactDetails(contactDetails))
                .ReturnsAsync(id);
            var controller = new ContactController(mockRepo.Object);

            // Act
            var result = await controller.SaveContactDetails(contactDetails);

            // Assert
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode;
            Assert.AreEqual(200, statusCode);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));
        }

        [TestMethod]
        public async Task Task_Update_Contact_Details_Return_OkResult()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);

            var mockRepo = new Mock<IContactDetailsApp>();
            mockRepo.Setup(repo => repo.UpdateContactDetails(contactDetails))
                .ReturnsAsync(id);
            var controller = new ContactController(mockRepo.Object);

            // Act
            var result = await controller.UpdateContactDetails(contactDetails);

            // Assert
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode;
            Assert.AreEqual(200, statusCode);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));
        }

        [TestMethod]
        public async Task Task_Delete_Contact_Details_Return_OkResult()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);

            var mockRepo = new Mock<IContactDetailsApp>();
            mockRepo.Setup(repo => repo.DeleteContactDetails(id))
                .ReturnsAsync(id);
            var controller = new ContactController(mockRepo.Object);

            // Act
            var result = await controller.DeleteContactDetails(id);

            // Assert
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode;
            Assert.AreEqual(200, statusCode);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ObjectResult));
        }


        #region private section
        private IEnumerable<ContactDetail> getContactList()
        {
            List<ContactDetail> contactList = new List<ContactDetail>()
            {
                new ContactDetail{ContactId=1,FirstName="Firstname",LastName="Lastname",Email="test@gmail.com",PhoneNumber="9867586778",Status="Active" },
                new ContactDetail{ContactId=1,FirstName="Firstname",LastName="Lastname",Email="test@gmail.com",PhoneNumber="9867586778",Status="Active" }
            };

            return contactList;
        }

        private ContactDetail getConatctDetailsById(int id)
        {
            ContactDetail contactDetails = new ContactDetail();
            contactDetails.ContactId = 1;
            contactDetails.FirstName = "Firstname";
            contactDetails.LastName = "Lastname";
            contactDetails.Email = "test@gmail.com";
            contactDetails.PhoneNumber = "9867586778";
            contactDetails.Status = "Active";

            return contactDetails;
        }

        #endregion
    }
}
