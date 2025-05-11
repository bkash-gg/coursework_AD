using System.Security.Claims;

namespace AD_Coursework.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                var allClaims = string.Join(", ", principal.Claims.Select(c => $"{c.Type}:{c.Value}"));
                throw new InvalidOperationException($"NameIdentifier claim not found. All claims: {allClaims}");
            }

            if (!Guid.TryParse(userId, out var parsedId))
            {
                throw new FormatException($"Invalid user ID format: {userId}");
            }

            return parsedId;
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }
}