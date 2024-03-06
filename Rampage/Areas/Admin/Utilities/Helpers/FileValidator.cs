namespace Rampage.Areas.Admin.Utilities.Helpers;


public static class FileValidator
{
    public static bool ValidateType(this IFormFile file, string type = "image")
    {
        if (!file.ContentType.Contains(type)) return false;

        return true;
    }
    public static bool ValidateSize(this IFormFile file, int mb)
    {
        if (file.Length > mb * 1024 * 1024) return false;

        return true;
    }
    public static bool ValidateImage(this IFormFile file, int mb = 2)
    {
        if (!file.ValidateType())
        {
            return false;
        };
        if (!file.ValidateSize(mb))
        {
            return false;
        }

        return true;
    }
}
