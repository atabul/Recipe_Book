namespace InventoryBeginners.Models
{
    public static class Helper
    {

        public static string GetTypeName(string fullTypeName)
        {
            string retString = "";

            try
            {
                int lastIndex = fullTypeName.LastIndexOf('.') + 1;
                retString = fullTypeName.Substring(lastIndex, fullTypeName.Length - lastIndex);
            }
            catch 
            {
                retString = fullTypeName;
            }

            retString = retString.Replace("]","");

            return retString;        
        }
    }
}
