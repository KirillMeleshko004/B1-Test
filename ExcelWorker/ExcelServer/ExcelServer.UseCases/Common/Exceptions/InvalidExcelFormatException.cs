namespace ExcelServer.UseCases.Common.Exceptions
{
    public class InvalidExcelFormatException : Exception
    {
        public InvalidExcelFormatException(int rowInd) :
            base($"Failed to parse excel file near row #{rowInd + 1}.")
        {

        }
    }
}