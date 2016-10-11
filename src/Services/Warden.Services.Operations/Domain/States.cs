namespace Warden.Services.Operations.Domain
{
    public static class States
    {
        public static string Accepted => "accepted";
        public static string Processing => "processing";
        public static string Completed => "completed";
        public static string Rejected => "rejected";
    }
}