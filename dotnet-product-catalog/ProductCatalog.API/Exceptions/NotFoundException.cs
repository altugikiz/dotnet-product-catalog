namespace ProductCatalog.API.Exceptions;

public class NotFoundException(string message) : Exception(message);