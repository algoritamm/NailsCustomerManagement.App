namespace NailsCustomerManagement.Core.Statics
{
    public static class Permissions
    {
        public static class Home
        {
            public static string View = "HomeView";
            public static string DefaultUrl = "/";
            public static string AdministratorRolePermission = "ApplicationCanSeeAll";
        }

        public static class Appointment
        {
            public static string View = "AppointmentView";
            public static string DefaultUrl = "/";
        }

        public static class Scheduler
        {
            public static string View = "SchedulerView";
            public static string DefaultUrl = "/";
        }

    }
}
