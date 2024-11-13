namespace ExcelServer.UseCases.Common.Exceptions
{
    public class TurnoverDocumentNotFoundException : Exception
    {
        public TurnoverDocumentNotFoundException(Guid id) :
            base($"Turnover document with id ${id} was not found.")
        {

        }
    }
}