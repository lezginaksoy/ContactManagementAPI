using System.ComponentModel;

namespace ContactManagementAPI.Enums
{
    public enum ErrorCodes
    {

        Success = 0,
        Invalid_ContactId = 1001,
        Contact_NotFound = 1002,
        ContactName_IsEmpty = 1003,
        Contact_Creation_Failed = 1004,
        Contact_Update_Failed = 1005,
        Contact_Deletion_Failed = 1006,
        Contact_IsAssigned_ToFund = 1007,
        Invalid_FundId = 1008,
        Contact_Already_Assigned_ThisFund = 1009,
        Assign_Fund_To_Contact_Failed = 1010,
        Contact_Not_Assigned_To_Fund = 1011,
        Remove_ContactFund_Failed = 1012,
        Invalid_PaginationParams=1013,
        Unknown_error = 7000,
        Database_execution_error = 7001,
    }
}
