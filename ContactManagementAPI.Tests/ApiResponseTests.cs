using ContactManagementAPI.Enums;
using ContactManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementAPI.Tests
{
    public class ApiResponseTests
    {
        [Fact]
        public void ApiResponse_Success_ShouldHaveDefaultValues()
        {
            // Act
            var response = new ApiResponse(new { Name = "lazgin aksoy" });

            // Assert
            Assert.Equal((int)ErrorCodes.Success, response.Code);
            Assert.Equal("Success", response.Message);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.NotNull(response.Result);
        }

      
        [Fact]
        public void ApiResponse_Error_ShouldSetCorrectErrorCode()
        {
            // Act
            var response = new ApiResponse(ErrorCodes.Invalid_ContactId);

            // Assert
            Assert.Equal((int)ErrorCodes.Invalid_ContactId, response.Code);          
        }

        
        [Fact]
        public void ApiResponse_Error_ShouldAllowCustomStatusCode()
        {
            // Act
            var response = new ApiResponse(ErrorCodes.Contact_NotFound, StatusCodes.Status404NotFound);

            // Assert
            Assert.Equal((int)ErrorCodes.Contact_NotFound, response.Code);          
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public void GetErrorMessage_ShouldFormatEnumToReadableText()
        {
            // Act
            var formattedMessage = ErrorCodes.Invalid_ContactId.GetErrorMessage();

            // Assert
            Assert.Equal("Invalid Contactid", formattedMessage);
        }

     
    }
}
