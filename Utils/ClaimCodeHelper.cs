namespace AD_Coursework.Utils
{
    public static class ClaimCodeHelper
    {
        public static string GenerateClaimCode()
        {
            string prefix = "ORD";
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"); 
            string uniqueGuid = Guid.NewGuid().ToString("N").Substring(0, 6);

            return $"{prefix}-{timestamp}-{uniqueGuid}";
        }
    }
}
