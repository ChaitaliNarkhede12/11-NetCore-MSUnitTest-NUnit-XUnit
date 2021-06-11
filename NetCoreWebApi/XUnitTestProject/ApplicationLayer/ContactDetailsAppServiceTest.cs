using ApplicationLayer.Services;
using DataAccess.Interfaces;
using DataAccess.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject.ApplicationLayer
{
    public class ContactDetailsAppServiceTest
    {
        [Fact]
        public async Task Task_Get_Contact_List_Return_List()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<ContactDetail>>();
            mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync(getContactList());

            var service = new ContactDetailsAppService(mockRepo.Object);

            // Act
            var result = await service.GetContactList();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ContactDetail>>(result);

        }

        [Fact]
        public async Task Task_Get_Contact_List_Return_Exception_When_Contact_List()
        {
            IEnumerable<ContactDetail> contactList = null;
            // Arrange
            var mockRepo = new Mock<IRepository<ContactDetail>>();
            mockRepo.Setup(repo => repo.Get())
                .ReturnsAsync(contactList);

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                var result = await service.GetContactList();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("GetContactList - Contact details list is null", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Get_Contact_Details_By_Id_Return_Contact_Details()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IRepository<ContactDetail>>();
            mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(getConatctDetailsById(id));

            var service = new ContactDetailsAppService(mockRepo.Object);

            // Act
            var result = await service.GetContactDetailsById(id);

            // Assert
            Assert.IsAssignableFrom<ContactDetail>(result);
            Assert.Equal("Firstname", result.FirstName);
        }

        [Fact]
        public async Task Task_Get_Contact_Details_By_Id_Return_Exception_When_Id_Less_than_1()
        {
            // Arrange
            int id = 0;
            var mockRepo = new Mock<IRepository<ContactDetail>>();

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                var result = await service.GetContactDetailsById(id);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("GetContactDetailsById - Contact id is 0 or less than 0", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Get_Contact_Details_By_Id_Return_Exception_When_Contact_Details_Is_Null()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetail = null;

            var mockRepo = new Mock<IRepository<ContactDetail>>();
            mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(contactDetail);

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                var result = await service.GetContactDetailsById(id);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("GetContactDetailsById - Contact details is null", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Add_Contact_Details_Return_Result()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);
            var mockRepo = new Mock<IRepository<ContactDetail>>();

            mockRepo.Setup(repo => repo.Add(contactDetails));

            mockRepo.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(id);

            var service = new ContactDetailsAppService(mockRepo.Object);

            // Act
            int result = await service.AddContactDetails(contactDetails);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Task_Add_Contact_Details_Return_Exception_When_Contact_Model_Null()
        {
            // Arrange
            ContactDetail contactDetails = null;
            var mockRepo = new Mock<IRepository<ContactDetail>>();
           
            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                int result = await service.AddContactDetails(contactDetails);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("AddContactDetails - Contact details is null", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Update_Contact_Details_Return_Result()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);
            var mockRepo = new Mock<IRepository<ContactDetail>>();

            mockRepo.Setup(repo => repo.Update(contactDetails));

            mockRepo.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(id);

            var service = new ContactDetailsAppService(mockRepo.Object);

            // Act
            int result = await service.UpdateContactDetails(contactDetails);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Task_Update_Contact_Details_Return_Exception_When_Contact_Id_Is_Less_Than_0()
        {
            // Arrange
            ContactDetail contactDetails = new ContactDetail();
            contactDetails.ContactId = 0;

            var mockRepo = new Mock<IRepository<ContactDetail>>();

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                int result = await service.UpdateContactDetails(contactDetails);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("UpdateContactDetails - Contact id is 0 or less than 0", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Delete_Contact_Details_Return_Exception_When_Id_Is_Less_Than_0()
        {
            // Arrange
            int id =0;

            var mockRepo = new Mock<IRepository<ContactDetail>>();

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                int result = await service.DeleteContactDetails(id);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("DeleteContactDetails - Contact id is 0 or less than 0", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Delete_Contact_Details_Return_Exception_When_Contact_Model_Null()
        {
            // Arrange
            // Arrange
            int id = 1;
            ContactDetail contactDetail = null;

            var mockRepo = new Mock<IRepository<ContactDetail>>();

            mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(contactDetail);

            var service = new ContactDetailsAppService(mockRepo.Object);

            try
            {
                // Act
                var result = await service.DeleteContactDetails(id);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Equal("DeleteContactDetails - Contact details is null", ex.Message);
            }
        }

        [Fact]
        public async Task Task_Delete_Contact_Details_Return_Result()
        {
            // Arrange
            int id = 1;
            ContactDetail contactDetails = getConatctDetailsById(id);
            var mockRepo = new Mock<IRepository<ContactDetail>>();

            mockRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(contactDetails);

            mockRepo.Setup(repo => repo.Update(contactDetails));

            mockRepo.Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(id);

            var service = new ContactDetailsAppService(mockRepo.Object);

            // Act
            int result = await service.DeleteContactDetails(id);

            // Assert
            Assert.Equal(1, result);
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
