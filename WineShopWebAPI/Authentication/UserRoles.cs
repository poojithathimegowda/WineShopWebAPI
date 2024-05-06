namespace WineShopWebAPI.Authentication
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string PurchaseManager = "PurchaseManager";
        public const string StoreManager = "StoreManager";
        public const string Employee = "Employee";

        public static string[] roles = { UserRoles.Admin, UserRoles.StoreManager, UserRoles.Employee, UserRoles.User, UserRoles.PurchaseManager };
    }


}



